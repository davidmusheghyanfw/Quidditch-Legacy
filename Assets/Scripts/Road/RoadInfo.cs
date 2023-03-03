using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadInfo : MonoBehaviour
{
    public static RoadInfo instance;

    [SerializeField] private GameObject roadGroundPrefab;
 


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
      
    }

    public Vector3 GetRoadScale()
    {
        return roadGroundPrefab.transform.localScale;
    }

    public GameObject GetRoadGround()
    { return roadGroundPrefab; }
}
