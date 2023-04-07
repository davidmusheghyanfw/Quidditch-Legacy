using Dreamteck.Forever;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    //[SerializeField] private LevelGenerator LeftlevelGenerator;
    //[SerializeField] private LevelGenerator RightlevelGenerator;
    [SerializeField] private EnvironmentDefinition environmentDefinition;
    [SerializeField] private ForeverLevel foreverLevel;
    [SerializeField] private List<SegmentDefinition> segmentsList= new List<SegmentDefinition>();
    float overallEnvironmentDistance = 0;

    private void Awake()
    {
        instance = this; 
    }

    public void GenerateEnvironment()
    {
        while (overallEnvironmentDistance <= RoadGenerator.instance.GetDistance())
        {

            EnvironmentSegmentInfo environmentSegment = environmentDefinition.EnvironmentSegments[Random.Range(0, environmentDefinition.EnvironmentSegments.Count)];
           
            segmentsList.Add(environmentSegment.GetSegmentDefinition());
            overallEnvironmentDistance += environmentSegment.GetSegmentLength();
        }

        foreverLevel.sequenceCollection.sequences[0].segments = segmentsList.ToArray();
        RoadGenerator.instance.GetLevelGenerator().StartGeneration();
        //RightlevelGenerator.Restart();
    }


}
