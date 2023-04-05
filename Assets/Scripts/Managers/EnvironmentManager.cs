using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    [SerializeField] private LevelGenerator LeftlevelGenerator;
    [SerializeField] private LevelGenerator RightlevelGenerator;
    [SerializeField] private EnvironmentDefinition environmentDefinition;
    [SerializeField] private ForeverLevel foreverLevel;
    [SerializeField] private List<SegmentDefinition> segmentsList= new List<SegmentDefinition>();
    float overallEnvironmentDistance = 0;

    private void Awake()
    {
        instance = this; 
    }


    public LevelGenerator GetLeftSideLevelGenerator() 
    {
        return LeftlevelGenerator;
    } 
    public LevelGenerator GetRightSideLevelGenerator() 
    {
        return RightlevelGenerator;
    }
    public void SetPath(CustomPathGenerator path)
    {
        LeftlevelGenerator.pathGenerator= path;
        RightlevelGenerator.pathGenerator= path;
    }

    public void GenerateEnvironment()
    {
        while (overallEnvironmentDistance <= RoadGenerator.instance.GetDistance()*2)
        {

            EnvironmentSegmentInfo environmentSegment = environmentDefinition.EnvironmentSegments[Random.Range(0, 1)];
            segmentsList.Add(environmentSegment.GetSegmentDefinition());
            overallEnvironmentDistance += environmentSegment.GetSegmentLength();
        }

        foreverLevel.sequenceCollection.sequences[0].segments = segmentsList.ToArray();
        LeftlevelGenerator.Restart();
        RightlevelGenerator.Restart();
    }

}
