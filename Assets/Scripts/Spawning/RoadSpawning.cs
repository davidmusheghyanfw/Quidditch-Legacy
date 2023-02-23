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
    [SerializeField] private int visibleSegmentCount;
    private Vector3 tmpV3;

    private List<Transform> roads = new List<Transform> ();
    void Start()
    {
        roads.Add(firstRoad);
        tmpV3 = firstRoad.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.position.z - roads[roads.Count-1].position.z + nextSegmentCreationOffset >= roads[roads.Count - 1].position.z - roads[roads.Count - 1].position.z)
        {
            

            for (int i = 0; i < visibleSegmentCount; i++)
            {
                tmpV3.z += roads[roads.Count - 1].localScale.z;
                GameObject currentRoad = Instantiate(road, tmpV3, transform.rotation, parent);
                roads.Add(currentRoad.transform);
            }
            
           

        }
    }
}
