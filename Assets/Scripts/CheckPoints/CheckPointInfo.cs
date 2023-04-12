using Dreamteck.Splines;
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
    private void Start()
    {
        UpdateSpineSample();
        SetPosInScreen(transform.position - sample.position);
        CheckPointSpawning.instance.AddCheckPointToList(this);
    }
  
    public float GetPosInSpline()
    {
        return (float)sample.percent;
    }

    public Vector3 GetPosInScreen() { return posInScreen; }
    public Vector3 GetOverallPos() { return overallPos; }

    public void SetPosInScreen(Vector3 pos)
    {
        posInScreen = pos;
    }
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
}
