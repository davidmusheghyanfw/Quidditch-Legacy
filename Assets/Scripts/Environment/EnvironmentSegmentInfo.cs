using Dreamteck.Forever;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSegmentInfo : MonoBehaviour
{
    [SerializeField] Transform segmnetStart;
    [SerializeField] Transform segmnetEnd;
    [SerializeField] LevelSegment levelSegment;
    [SerializeField] SegmentDefinition segmentDefinition;
    [SerializeField] Transform endPos;

    SplineSample sample = new SplineSample();
    private void Start()
    {

        LevelManager.instance.SetLevelEndPos(GetEndPos());
    }
    public float GetSegmentLength()
    { 
        float pos = Mathf.Abs(segmnetStart.position.z) + Mathf.Abs(segmnetEnd.position.z);
        pos = pos == 0 ? Mathf.Abs(segmnetStart.position.x) + Mathf.Abs(segmnetEnd.position.x) : pos;
        return pos; 
    }
    public SegmentDefinition GetSegmentDefinition() 
    {
        return segmentDefinition;
    }
    public LevelSegment GetLevelSegment()
    {
        return levelSegment;
    }

    public float GetEndPos()
    {
        if (!endPos) return 0.9f; 
        RoadGenerator.instance.GetLevelGenerator().Project(endPos.position, ref sample);
        return (float)sample.percent;
    }
}
