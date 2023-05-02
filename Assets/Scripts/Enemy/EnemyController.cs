using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyController : CharacterController
{
    private CharacterMovemant characterMovemant;

    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AnimationCurve curve;

    [Header("Speed")]
    [SerializeField] private float smoothSpeedChanging;
    [Header("Control")]
    [SerializeField] private float smoothnesControl;
    [Header("Checkpoints")]
    [SerializeField] private float getCheckPointRate;
    [SerializeField] private float randomRangeInRing;

    private Vector3 targetCursor;
    
   
    Vector3 pushedPos;
    private int nextCheckPointIndex;

    private void Start()
    {

    }

    public override void CharacterInit()
    {
       
        nextCheckPointIndex = 0;
       
        characterMovemant = GetCharacterMovemant();
      
        base.CharacterInit();
    }

    Coroutine GettingCursorPositionRoutineC;
    IEnumerator GettingCursorPositionRoutine()
    {
        bool getNewCheckpoint = NewCheckPointRate();
        bool isPosGetted = false;
       
        while (true)
        {
          
            //if(nextCheckPointIndex > 0 && CheckPointSpawning.instance.GetPervRingSample(nextCheckPointIndex).position.z > GetSplineSample().position.z)
            //{
            //    nextCheckPointIndex--;
            //}

            //if (isDied || getNewCheckpoint && !isPosGetted)
            //{

            //    targetCursor = CheckPointSpawning.instance.GetNextCheckPointOnScreen(nextCheckPointIndex);
            //    Vector2 randomPointInsideUnitCircle = Random.insideUnitCircle * randomRangeInRing;
            //    targetCursor.Set(targetCursor.x + randomPointInsideUnitCircle.x, targetCursor.y + randomPointInsideUnitCircle.y, 0);
            //    isPosGetted = true;
            //}
            //else if (isDied || !getNewCheckpoint && !isPosGetted)
            //{
            //    targetCursor = CheckPointSpawning.instance.GetOtherWay(nextCheckPointIndex);
            //    //Vector2 randomPointInsideUnitCircle = Random.insideUnitCircle;
            //    //targetCursor.Set(targetCursor.x + randomPointInsideUnitCircle.x, targetCursor.y + randomPointInsideUnitCircle.y, 0);
            //    targetCursor.Set(targetCursor.x, targetCursor.y, 0);
            //    isPosGetted = true;
            //}

            //cursor = Vector3.Lerp(cursor, targetCursor, smoothnesControl * Time.deltaTime);
            //cursor.x = Mathf.Clamp(cursor.x, horizontalBorderMin, horizontalBorderMax);
            //cursor.y = Mathf.Clamp(cursor.y, verticalBorderMin, verticalBorderMax);


            //Debug.Log(GetSplineSample().position.z);
            //if (CheckPointSpawning.instance.GetCurrentRingSample(nextCheckPointIndex).position.z < GetSplineSample().position.z && nextCheckPointIndex < CheckPointSpawning.instance.MaxCheckpointCount - 1)
            //{
            //    nextCheckPointIndex++;
            //    getNewCheckpoint = NewCheckPointRate();
            //    isPosGetted = false;
            //}
                yield return new WaitForEndOfFrame();
        }

    }
   
   
    public void StartGettingCursor()
    {
  
        if (GettingCursorPositionRoutineC != null) StopCoroutine(GettingCursorPositionRoutineC);
        GettingCursorPositionRoutineC = StartCoroutine(GettingCursorPositionRoutine());

    }

    public void StopGettingCursor()
    {
        if (GettingCursorPositionRoutineC != null) StopCoroutine(GettingCursorPositionRoutineC);
        StopCursorFollowing();
    }

    private bool NewCheckPointRate()
    {
        if (Random.value < getCheckPointRate / 100) return true;
        return false;

    }

    public void OnPlayerTriggered(Vector3 playerPos)
    {
        int dir = transform.position.x - playerPos.x > 0 ? 1 : -1;
       
        pushedPos = transform.position;
        pushedPos.x += 10 * dir;
        StartCoroutine(PushRoutine());
    }

    private IEnumerator PushRoutine()
    {
        float t = 0.0f;
        float startTime = Time.fixedTime;

        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 0.5f;
            transform.position = Vector3.Lerp(transform.position, pushedPos, t);
            yield return new WaitForEndOfFrame();
        }

    }

    public AnimationCurve GetCurve()
    {
        return curve;
    }
    public void SetCurve(AnimationCurve animationCurve)
    {
        curve = animationCurve;
    }

    public void SetNextCheckPointIndex(int index)
    {

        nextCheckPointIndex = index;
    }

    public float GetNextCheckPointIndex()
    {
        return nextCheckPointIndex;
    }
}
