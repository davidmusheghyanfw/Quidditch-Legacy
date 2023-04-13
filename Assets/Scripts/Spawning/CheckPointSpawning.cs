using Dreamteck.Splines;
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

    [SerializeField] private CheckPointInfo checkPoint;
    [SerializeField] private Transform parent;

    [SerializeField] private Transform player;

    [SerializeField] private List<CheckPointInfo> checkPoints = new List<CheckPointInfo>();

    //GameObject currentCheckPoint;

    public int MaxCheckpointCount
    {
        get
        {
            return checkPoints.Count;
        }
    }

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
        //StopCheckPointSpawning();
        tmpOffset = 0;
        DestroyAll();
        checkPoints.Clear();
        spawnPos = Vector3.zero;
        prevPointOnRoad = Vector3.zero;
        //StartCheckPointSpawning();
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
        spawnPos.Set(Random.Range(spawningPosX.x, spawningPosX.y),
            Random.Range(spawningPosY.x, spawningPosY.y), 0);

        var currentCheckPoint = Instantiate(checkPoint, spawnPos + pointOnRoad, transform.rotation, parent);
        currentCheckPoint.transform.LookAt(prevPointOnRoad - pointOnRoad);
        currentCheckPoint.SetOverallPos(spawnPos + pointOnRoad);
        currentCheckPoint.SetPosInScreen(spawnPos);
        checkPoints.Add(currentCheckPoint);

        

    }

    public void AddCheckPointToList(CheckPointInfo checkPointInfo)
    {
        checkPoints.Add(checkPointInfo);
    }

    private Vector3 GetNearestPointOnRoad(float offset)
    {

        return Vector3.zero;//RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetDistance()).position;
    }

    public int CalculateEnemyNearestCheckPoint(float enemyPos)
    {
        
        for (int i = 0; i < checkPoints.Count; i++)
        {
            if(enemyPos< checkPoints[i].GetPosInSpline()) return i;
        }
        return 0;
    }

    public Vector3 GetNextCheckPointOnScreen(int index)
    {
        if (index > checkPoints.Count - 1) return checkPoints[checkPoints.Count - 1].GetPosInScreen();
        return checkPoints[index].GetPosInScreen();
    }
    public SplineSample GetCurrentRingSample(int index)
    {
        if (index >= MaxCheckpointCount - 1)
        {
            checkPoints[checkPoints.Count - 1].UpdateSpineSample();
            return checkPoints[checkPoints.Count - 1].GetSplineSample();
        }
        checkPoints[index].UpdateSpineSample();
        return checkPoints[index].GetSplineSample();
    }

    private void DestroyCheckPoint()
    {

        Destroy(checkPoints[0].gameObject);

        checkPoints.RemoveAt(0);
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


}
