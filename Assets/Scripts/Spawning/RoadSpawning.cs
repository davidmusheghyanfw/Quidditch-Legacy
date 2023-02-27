using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawning : MonoBehaviour
{
    public static RoadSpawning instance;

    [SerializeField] private GameObject road;
    [SerializeField] private GameObject Finish;
    [SerializeField] private Transform player;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform firstRoad;

    [SerializeField] private float nextSegmentCreationOffset;
    [SerializeField] private float roadLength;
    [SerializeField] private float finishLength;
    [SerializeField] private int visibleSegmentCount;

    private bool isDestroying = false;
    private Vector3 tmpV3;

    private List<Transform> roads = new List<Transform> ();

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
       // roads.Add(firstRoad);
       //tmpV3 = firstRoad.position;
    }

    public void RoadSpawningInit()
    {
        StopRoadSpawning();
        DestroyAll();
        roads.Clear();
        tmpV3 = Vector3.zero;
        roadLength = road.GetComponent<RoadInfo>().GetRoadScale().z;
        SpawnFinish();
        StartRoadSpawning();
    }

    Coroutine SpawnNewRoadRoutineC;
    private IEnumerator SpawnNewRoadRoutine()
    {
        while (true)
        {

            if (roads.Count == 0) SpawnNewRoad();
            
            if (player.position.z >= roads[roads.Count - 1].position.z / 2 && !isDestroying)
            {
                DestroyBackRoads();
                SpawnNewRoad();
            }


            yield return new WaitForEndOfFrame();
        }
     
    }

    private void SpawnNewRoad()
    {
        for (int i = 0; i < visibleSegmentCount; i++)
        {
            tmpV3.z += roadLength;
            if (tmpV3.z > LevelManager.instance.GetLevelDistance())
            {

               
                StopRoadSpawning();
                break;
            }
      
            if (roads.Count == 0) tmpV3.z = 0;
          
            GameObject currentRoad = Instantiate(road, tmpV3, transform.rotation, parent);
            roads.Add(currentRoad.transform);
        }

        isDestroying = false;    
    }
    private void DestroyBackRoads()
    {
        isDestroying = true;
       
        for (int i = 0; i < visibleSegmentCount - 3; i++)
        {
            
            if (roads[i].transform.position.z + roadLength <player.position.z)
            Destroy(roads[i].gameObject);
         
        }

        for (int i = visibleSegmentCount - 4; i >= 0; i--)
        {
            
            roads.RemoveAt(i);
        }
    }

    private void DestroyAll()
    {
        foreach (Transform item in parent)
        {
            Destroy(item.gameObject);
        }
    }


    private void StopRoadSpawning()
    {
        if (SpawnNewRoadRoutineC != null) StopCoroutine(SpawnNewRoadRoutineC);
        
    }   
    
    private void StartRoadSpawning()
    {
        if (SpawnNewRoadRoutineC != null) StopCoroutine(SpawnNewRoadRoutineC);
        SpawnNewRoadRoutineC = StartCoroutine(SpawnNewRoadRoutine());

    }

    public float GetRoadLength()
    {
        return roadLength;
    }

    private void SpawnFinish()
    {
        
        Finish.transform.position = new Vector3(0, 0,LevelManager.instance.GetLevelDistance() + roadLength);
    }

    public Transform GetRoad()
    {
        return road.transform;
    }
}
