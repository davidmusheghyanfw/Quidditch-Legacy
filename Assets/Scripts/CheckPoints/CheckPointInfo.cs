using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointInfo : MonoBehaviour
{
    [SerializeField] private float triggerRadius;
    [SerializeField] private float ringScale;
    [SerializeField] private float posInSpline;
    [SerializeField] private Vector3 posInScreen;
    [SerializeField] private Vector3 overallPos;
    public void SetPosInSpline(float value)
    {
        posInSpline = value;
    }
    public float GetPosInSpline()
    {
        return posInSpline;
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

}
