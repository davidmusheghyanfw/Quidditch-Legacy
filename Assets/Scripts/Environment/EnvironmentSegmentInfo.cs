using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSegmentInfo : MonoBehaviour
{
    [SerializeField] Transform segmnetStart;
    [SerializeField] Transform segmnetEnd;

    public float GetSegmentLength()
    {
        return Mathf.Abs(segmnetStart.position.z) + Mathf.Abs(segmnetEnd.position.z);
    }
}
