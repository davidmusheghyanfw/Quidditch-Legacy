using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSegmentInfo : MonoBehaviour
{
    [SerializeField] Transform segmnetStart;
    [SerializeField] Transform segmnetEnd;
    [SerializeField] LevelSegment levelSegment;
    [SerializeField] SegmentDefinition segmentDefinition;

    public float GetSegmentLength()
    {
        return Mathf.Abs(segmnetStart.position.z) + Mathf.Abs(segmnetEnd.position.z);
    }
    public SegmentDefinition GetSegmentDefinition() 
    {
        return segmentDefinition;
    }
    public LevelSegment GetLevelSegment()
    {
        return levelSegment;
    }
}
