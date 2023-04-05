using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;
using Dreamteck.Forever;
using System;
using System.Linq;
using Unity.Mathematics;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance;
    [SerializeField] SplineComputer splineComputer;
    [SerializeField] SplineMesh splineMesh;
    [SerializeField] SplineDefinition splineDefinition;
    [SerializeField] GameObject finish;
    [SerializeField] CustomPathGenerator pathGenerator;
    
    private double distance;
    private float offset;

    private Vector3 newPointDir;
    private Vector3 newPointPos;
    private Vector3 prevPointDir;
    private Vector3 prevPointPos;
    private Vector3 dir;
    private List<SplinePoint> allSplinePoints;
   // private List<SplinePoint> LeftEnvironmentPoints = new List<SplinePoint>();    
   

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    
    }

    public void RoadGeneratorInit()
    {
        dir = Vector3.forward;
        newPointDir = Vector3.zero;
        newPointPos = Vector3.zero;
        prevPointPos = Vector3.zero;
        prevPointDir = Vector3.zero;
        
        pathGenerator.Clear();
        offset = LevelManager.instance.GetRoadPointOffset();
        
        CreateRoadSplinePoints();
    }

    private void CreateRoadSplinePoints()
    {
        
        splineComputer.RebuildImmediate();
        allSplinePoints = new List<SplinePoint>();
        allSplinePoints.Add(new SplinePoint(Vector3.zero));

        splineDefinition = LevelManager.instance.GetLevelDefinition();

        for (int i = 0; i < splineDefinition.SplineSegments.Count; i++)
        {
            NewPoint(splineDefinition.SplineSegments[i].length, splineDefinition.SplineSegments[i].rotation);
        }
        splineComputer.SetPoints(allSplinePoints.ToArray());
        pathGenerator.points =allSplinePoints.ToArray();
        pathGenerator.segmentCount = (int)Mathf.Round( allSplinePoints.Count / 2);
        distance = splineComputer.CalculateLength();
        EnvironmentManager.instance.SetPath(pathGenerator);
        EnvironmentManager.instance.GenerateEnvironment();
        

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

            allSplinePoints.Add(new SplinePoint(newPointPos));
            //newPointPos.Set(newPointPos.x + 50, newPointPos.y, newPointPos.z);
            //LeftEnvironmentPoints.Add(new SplinePoint(newPointPos));
          
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
        finish.transform.position = newPointPos;
    }

    public float GetFirstAndLastPointDistance()
    {
        return Vector3.Distance(Vector3.zero, splineComputer.GetPoint(splineComputer.pointCount-1).position);
    }
}

