using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;
using System;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;
    [SerializeField] SplineComputer splineComputer;
    [SerializeField] SplineMesh splineMesh;
    [SerializeField] GameObject firstRoad;
    [SerializeField] GameObject secondRoad;
    [SerializeField] SplineDefinition splineDefinition;

    
    private double distance;
    private int pointCount;

    private Vector3 newPointDir;
    private Vector3 newPointPos;
    private Vector3 prevPointDir;
    private Vector3 prevPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
        pointCount = 0;
        CreateRoadSplinePoints();
    }

    private void CreateRoadSplinePoints()
    {
        splineComputer.Rebuild();
       
        for (int i = 0; i < splineDefinition.SplineSegments.Count; i++)
        {
            NewPoint(splineDefinition.SplineSegments[i].length, splineDefinition.SplineSegments[i].rotation);
        }
        distance = splineComputer.CalculateLength();
        GenerateFinish();
    }

    private void NewPoint(float length, Vector3 rot)
    {
        rot /= 2;
        rot /= length / LevelManager.instance.GetRoadPointOffset();

        for (int j = 0; j < length / LevelManager.instance.GetRoadPointOffset(); j++)
        {
            splineComputer.SetPoint(pointCount, new SplinePoint(newPointPos));
        
            newPointDir = (Quaternion.Euler(Vector3.up * rot.x + Vector3.right * rot.y) * prevPointDir) * LevelManager.instance.GetRoadPointOffset();

            newPointPos = prevPoint + newPointDir;
            pointCount++;

            prevPointDir = newPointDir;
            prevPoint = newPointPos;
        }
    }

    public SplineComputer GetSplineComputer() 
    {
        return splineComputer;
    } 

    public double GetDistance()
    {
        return distance;
    }

    private void GenerateFinish()
    {

    }
}
