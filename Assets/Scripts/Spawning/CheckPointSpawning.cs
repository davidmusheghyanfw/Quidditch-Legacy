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


    private Vector3 spawnPos;

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
        DestroyAll();
        checkPoints.Clear();
        spawnPos = Vector3.zero;
        StartCheckPointSpawning();
    }

    private Coroutine CheckPointSpawnRoutineC;
    private IEnumerator CheckPointSpawnRoutine()
    {
        while (true)
        {

            SpawnNewCheckPoint();
            //if (checkPoints.Count == 0) SpawnNewCheckPoint();

            //if (player.position.z > checkPoints[0].transform.position.z + nextSegmentCreationOffset
            //    && GameManager.instance.isGameInited) DestroyCheckPoint();

            //if (player.position.z < checkPoints[checkPoints.Count - 1].transform.position.z + nextSegmentCreationOffset
            //    && checkPoints.Count - 1 < maxSegmentAmount * visibleSegmentCount
            //    && GameManager.instance.isGameInited)
            //{
            //    SpawnNewCheckPoint();
            //}


            yield return new WaitForEndOfFrame();
        }
    }
    private void SpawnNewCheckPoint()
    {
        float tmpOffset = offset + (checkPoints.Count == 0 ? 0 : checkPoints[checkPoints.Count - 1].transform.position.z);
        if (tmpOffset >= RoadGenerator.instance.GetDistance())
        {
            StopCheckPointSpawning();
        }

        Vector3 pointOnRoad = GetNearestPointOnRoad(tmpOffset);
        spawnPos.Set(Random.Range(spawningPosX.x, spawningPosX.y)+pointOnRoad.x,
            Random.Range(spawningPosY.x, spawningPosY.y) + pointOnRoad.y, tmpOffset);

        currentCheckPoint = Instantiate(checkPoint, spawnPos, transform.rotation, parent);

        checkPoints.Add(currentCheckPoint);
        maxCheckpointCount++;
        //for (int i = 0; i < visibleSegmentCount; i++)
        //{
        //    float tmpOffset = offset + (checkPoints.Count == 0 ? 0 : checkPoints[checkPoints.Count - 1].transform.position.z);
        //    if (tmpOffset >= RoadGenerator.instance.GetDistance())
        //    {

        //        StopCheckPointSpawning();
        //        break;
        //    }
        //    spawnPos.Set(Random.Range(spawningPosX.x, spawningPosX.y),
        //     Random.Range(spawningPosY.x, spawningPosY.y),tmpOffset);

        //    currentCheckPoint = Instantiate(checkPoint, spawnPos, transform.rotation, parent);

        //    checkPoints.Add(currentCheckPoint);
        //    maxCheckpointCount++;
        //}

    }

    private Vector3 GetNearestPointOnRoad(float offset)
    {
        Debug.Log(offset / RoadGenerator.instance.GetFirstAndLastPointDistance() / 100);
        return RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetFirstAndLastPointDistance() / 100).position;
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
