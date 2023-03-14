using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSplineDefinition", menuName = "SplineDefinition", order = 1)]
public class SplineDefinition : ScriptableObject 
{
    public List<SplineParameter> SplineSegments = new List<SplineParameter>();

}

[System.Serializable]
public struct SplineParameter
{
    public float length;
    public Vector3 rotation;
}