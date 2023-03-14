using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner instance;
    [SerializeField] private Coin coin;
    [SerializeField] private float offset;
    [SerializeField] private CoinDefinition coinDefinition;
    [SerializeField] private Transform parent;

    [SerializeField] private List<GameObject> coins= new List<GameObject>();

    private Vector3 pointOnRoad;
    private Vector3 spawnPos;

    private GameObject currentCoin;

    private void Awake()
    {
        instance = this;
    }

    public void CoinSpawnerInit()
    {
        StopCoinSpawnRoutine();
        DestroyAll();
        coins.Clear();
        spawnPos = Vector3.zero;
        StartCoinSpawnRoutine();
    }

    Coroutine CoinSpawnRoutineC;
    IEnumerator CoinSpawnRoutine()
    {
        float tmpOffset = 0f;
        for (int i = 0; i < coinDefinition.CoinSegments.Count; i++)
        {
            if (coinDefinition.CoinSegments[i].coinCount == 0) continue;
                tmpOffset += offset;

            for (int j = 0; j < coinDefinition.CoinSegments[i].coinCount; j++)
            {
                if (tmpOffset >= RoadGenerator.instance.GetDistance())
                {
                    StopCoinSpawnRoutine();
                    break;
                }
                tmpOffset += coinDefinition.CoinSegments[i].offset;
               pointOnRoad = GetNearestPointOnRoad(tmpOffset);

                spawnPos.Set(pointOnRoad.x + coinDefinition.CoinSegments[i].position.x, pointOnRoad.y + coinDefinition.CoinSegments[i].position.y,
                    pointOnRoad.z + coinDefinition.CoinSegments[i].position.z);

                currentCoin = Instantiate(coin.gameObject, spawnPos, transform.rotation, parent);
              

                coins.Add(currentCoin);
                yield return new WaitForEndOfFrame();
            }
        }
       
    }

    private void StopCoinSpawnRoutine()
    {
        if (CoinSpawnRoutineC != null) StopCoroutine(CoinSpawnRoutineC);
    }

    private void StartCoinSpawnRoutine()
    {
        if (CoinSpawnRoutineC != null) StopCoroutine(CoinSpawnRoutineC);
        CoinSpawnRoutineC = StartCoroutine(CoinSpawnRoutine());
    }



    private Vector3 GetNearestPointOnRoad(float offset)
    {

        return RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetDistance()).position;
    }



    private void DestroyAll()
    {
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }
    }
}
