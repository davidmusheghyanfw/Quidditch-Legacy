using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInfo : MonoBehaviour
{
    [SerializeField] private float triggerRadius;
    [SerializeField] private float ringScale;
    [SerializeField] private float posInSpline;
    [SerializeField] private Vector3 posInScreen;
    [SerializeField] private Vector3 overallPos;
    [SerializeField] private SplineSample sample;
    [SerializeField] private Vector3 pos;
    [SerializeField] private List<Transform> otherWayList; 
    private void Start()
    {
        UpdateSpineSample();
        posInScreen = transform.position - sample.position;
        CheckPointSpawning.instance.AddCheckPointToList(this);
    }
  
    public float GetPosInSpline()
    {
        return (float)sample.percent;
    }

    public Vector3 GetPosInScreen(Vector3? pos = null)
    {
        if(pos is null) return posInScreen;
        return (Vector3)(pos - sample.position);

    }
    public Vector3 GetOverallPos() { return overallPos; }

   
    public void SetOverallPos(Vector3 pos)
    {
        overallPos = pos;
    }
    public void UpdateSpineSample()
    {
        RoadGenerator.instance.GetLevelGenerator().Project(transform.position, ref sample);
        pos = sample.position;
    }
    public SplineSample GetSplineSample()
    {
        return sample;
    }

    public Vector3 GetOtherWay()
    {
        return otherWayList[Random.Range(0, otherWayList.Count)].position;
    }
}
