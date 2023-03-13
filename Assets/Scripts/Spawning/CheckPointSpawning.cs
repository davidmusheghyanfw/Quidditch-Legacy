using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckPointSpawning : MonoBehaviour
{
    public static CheckPointSpawning instance;

    [SerializeField] private float offset;
    [SerializeField] private float nextSegmentCreationOffset;
    [SerializeField] private int maxSegmentAmount;
    [SerializeField] private int visibleSegmentCount;
    [SerializeField] private Vector2 spawningPosX;
    [SerializeField] private Vector2 spawningPosY;

    [SerializeField] private GameObject checkPoint;
    [SerializeField] private Transform parent;

    [SerializeField] private Transform player;

    private List<GameObject> checkPoints = new List<GameObject>();

    GameObject currentCheckPoint;

    private int maxCheckpointCount = 0;

    float tmpOffset;
    private Vector3 spawnPos;

    private Vector3 pointOnRoad;
    private Vector3 prevPointOnRoad;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CheckPointsSpawningInit()
    {
        StopCheckPointSpawning();
        tmpOffset = 0;
        DestroyAll();
        checkPoints.Clear();
        spawnPos = Vector3.zero;
        prevPointOnRoad = Vector3.zero;
        StartCheckPointSpawning();
    }

    private Coroutine CheckPointSpawnRoutineC;
    private IEnumerator CheckPointSpawnRoutine()
    {
        while (true)
        {

            SpawnNewCheckPoint();
     

            yield return new WaitForEndOfFrame();
        }
    }
    private void SpawnNewCheckPoint()
    {
        tmpOffset += offset;
        if (tmpOffset >= RoadGenerator.instance.GetDistance())
        {
            StopCheckPointSpawning();
            return;
        }

        pointOnRoad = GetNearestPointOnRoad(tmpOffset);
        prevPointOnRoad = GetNearestPointOnRoad(tmpOffset - 20);
        spawnPos.Set(Random.Range(spawningPosX.x, spawningPosX.y)+pointOnRoad.x,
            Random.Range(spawningPosY.x, spawningPosY.y) + pointOnRoad.y, pointOnRoad.z);

        currentCheckPoint = Instantiate(checkPoint, spawnPos, transform.rotation, parent);
        currentCheckPoint.transform.LookAt(prevPointOnRoad - pointOnRoad);
        
        checkPoints.Add(currentCheckPoint);
        maxCheckpointCount++;

        

    }

    private Vector3 GetNearestPointOnRoad(float offset)
    {
        //Debug.Log(RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetDistance()).position);
        return RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetDistance()).position;
    }

    private void DestroyCheckPoint()
    {

        Destroy(checkPoints[0].gameObject);

        checkPoints.RemoveAt(0);
        maxCheckpointCount--;

    }

    private void DestroyAll()
    {
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }
    }

    private void StopCheckPointSpawning()
    {
        if (CheckPointSpawnRoutineC != null) StopCoroutine(CheckPointSpawnRoutineC);
    }

    private void StartCheckPointSpawning()
    {
        if (CheckPointSpawnRoutineC != null) StopCoroutine(CheckPointSpawnRoutineC);
        CheckPointSpawnRoutineC = StartCoroutine(CheckPointSpawnRoutine());
    }

    public Vector3 GetEnemyGoalCheckPoint(int index)
    {
        if (index >= checkPoints.Count) return checkPoints[checkPoints.Count - 1].transform.position;
        return checkPoints[index].transform.position;
    }



    public int GetCheckPointCount()
    {
        return maxCheckpointCount;
    }
}
