using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;
    [SerializeField] SplineComputer splineComputer;
    [SerializeField] SplineMesh splineMesh;
    [SerializeField] GameObject firstRoad;
    [SerializeField] GameObject secondRoad;
    private float roadLength;
    private double distance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //roadLength = firstRoad.GetComponent<RoadInfo>().GetRoadScale().z;
        CreateRoadSplinePoints();
    }

    private void CreateRoadSplinePoints()
    {
        splineComputer.Rebuild(); 
        for (int i = 0; i < 10; i++)
        {
            splineComputer.SetPoint(i, new SplinePoint(new Vector3(Random.Range(-50, 50), Random.Range(-50,50), i * 300)));
           
            //splineComputer.SetPoint(i, new SplinePoint(Vector3.forward * i * roadLength));
            
        }
        distance = splineComputer.CalculateLength();
    }

    public SplineComputer GetSplineComputer() 
    {
        return splineComputer;
    } 

    public double GetDistance()
    {
        return distance;
    }
}
