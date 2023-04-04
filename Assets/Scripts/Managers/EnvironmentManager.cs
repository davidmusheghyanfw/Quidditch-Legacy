using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private EnvironmentDefinition environmentDefinition;
    [SerializeField] private ForeverLevel foreverLevel;
    [SerializeField] private List<SegmentDefinition> segmentsList= new List<SegmentDefinition>();
    float overallEnvironmentDistance = 0;

    private void Awake()
    {
        instance = this; 
    }


    public LevelGenerator GetLevelGenerator() 
    {
        return levelGenerator;
    }

    public void GenerateEnvironment()
    {
        while (overallEnvironmentDistance <= RoadGenerator.instance.GetDistance())
        {

            EnvironmentSegmentInfo environmentSegment = environmentDefinition.EnvironmentSegments[Random.Range(0, 1)];
            segmentsList.Add(environmentSegment.GetSegmentDefinition());
            overallEnvironmentDistance += environmentSegment.GetSegmentLength();
        }

        foreverLevel.sequenceCollection.sequences[0].segments = segmentsList.ToArray();
        levelGenerator.Restart();
    }

}
