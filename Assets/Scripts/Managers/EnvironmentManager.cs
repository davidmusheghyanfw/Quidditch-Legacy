using Dreamteck.Forever;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    
    [SerializeField] private EnvironmentDefinition environmentDefinition;
    [SerializeField] private ForeverLevel foreverLevel;
    [SerializeField] private List<SegmentDefinition> segmentsList;
    
    float overallEnvironmentDistance = 0;


    private void Awake()
    {
        instance = this; 
    }

    public void GenerateEnvironment()
    {
        overallEnvironmentDistance = 0;
        Debug.Log(RoadGenerator.instance.GetDistance() - 500);
        segmentsList = new List<SegmentDefinition>();
        while (overallEnvironmentDistance <= RoadGenerator.instance.GetDistance()-500)
        {
            Debug.Log(overallEnvironmentDistance);
            EnvironmentSegmentInfo environmentSegment = environmentDefinition.EnvironmentSegments[Random.Range(0, environmentDefinition.EnvironmentSegments.Count)];
           
            segmentsList.Add(environmentSegment.GetSegmentDefinition());
            overallEnvironmentDistance += environmentSegment.GetSegmentLength();
        }

        foreverLevel.sequenceCollection.sequences[0].segments = segmentsList.ToArray();
        RoadGenerator.instance.GetLevelGenerator().StartGeneration();

    }
}
