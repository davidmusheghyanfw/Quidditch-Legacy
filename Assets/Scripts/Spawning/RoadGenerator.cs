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
    private float offset;
    private int pointCount;

    private Vector3 newPointDir;
    private Vector3 newPointPos;
    private Vector3 prevPointDir;
    private Vector3 prevPointPos;
    private Vector3 dir;

   

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    
    }

    public void RoadGeneratorInit()
    {

        pointCount = 0;
        dir = Vector3.forward;
        offset = LevelManager.instance.GetRoadPointOffset();
        CreateRoadSplinePoints();
    }

    private void CreateRoadSplinePoints()
    {
        splineComputer.Rebuild();
        splineComputer.SetPoint(pointCount, new SplinePoint(Vector3.zero));

        for (int i = 0; i < splineDefinition.SplineSegments.Count; i++)
        {
            NewPoint(splineDefinition.SplineSegments[i].length, splineDefinition.SplineSegments[i].rotation);
        }
        distance = splineComputer.CalculateLength();
        GenerateFinish();
    }

    private void NewPoint(float length, Vector3 rot)
    {
   
        rot /= length / offset;

        prevPointDir = dir;

        for (int j = 0; j < (int)MathF.Round(length / offset); j++)
        {
            newPointDir = (Quaternion.Euler(Vector3.up * rot.x + Vector3.right * rot.y)*prevPointDir).normalized * offset;

            if (newPointDir.x != 0 || newPointDir.y != 0)
                dir = newPointDir;
            else
                dir.Set(dir.x, dir.y, newPointDir.z);


            newPointPos = prevPointPos + dir;

            prevPointDir = newPointDir;
            prevPointPos = newPointPos;


            pointCount++;
            splineComputer.SetPoint(pointCount, new SplinePoint(newPointPos));
           
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

    public float GetFirstAndLastPointDistance()
    {
        return Vector3.Distance(Vector3.zero, splineComputer.GetPoint(splineComputer.pointCount-1).position);
    }
}
