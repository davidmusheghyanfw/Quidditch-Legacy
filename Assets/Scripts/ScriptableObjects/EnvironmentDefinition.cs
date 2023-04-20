using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnvironmentSegmentDefinition", menuName = "EnvironmentSegmentDefinition", order = 2)]
public class EnvironmentDefinition : ScriptableObject 
{
    public EnvironmentSegmentInfo finish;
    public EnvironmentSegmentInfo start;
    public List<EnvironmentSegmentInfo> EnvironmentSegments = new List<EnvironmentSegmentInfo>();

}