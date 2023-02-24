using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawning : MonoBehaviour
{
    [SerializeField] private GameObject road;
    [SerializeField] private Transform player;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform firstRoad;

    [SerializeField] private float nextSegmentCreationOffset;
    [SerializeField] private float roadLength;
    [SerializeField] private int visibleSegmentCount;

    private bool isDestroying = false;
    private Vector3 tmpV3;

    private List<Transform> roads = new List<Transform> ();
    void Start()
    {
        StartCoroutine(SpawnNewRoadRoutine());
       // roads.Add(firstRoad);
       //tmpV3 = firstRoad.position;
    }


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

            //if (player.position.z - roads[roads.Count - 1].position.z + nextSegmentCreationOffset
            //>= roads[roads.Count - 1].position.z - roads[roads.Count - 1].position.z)
            //{

              

            //}


            

            yield return new WaitForEndOfFrame();
        }
     
    }

    private void SpawnNewRoad()
    {
        for (int i = 0; i < visibleSegmentCount; i++)
        {
            tmpV3.z += roadLength;
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
          
            Destroy(roads[i].gameObject);
         
        }

        for (int i = visibleSegmentCount - 4; i >= 0; i--)
        {
            
            roads.RemoveAt(i);
        }
    }
}
