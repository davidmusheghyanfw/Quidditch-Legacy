using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyController : CharacterController
{
    [SerializeField] private float smoothnesControl;
    [SerializeField] private float GetCheckPointRate;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float spawnPosPersent;

    private Vector3 targetCursor;
    private Vector3 randomPos;

    int prevCheckPointCount;
    int CheckPointCount;
    Vector3 pushedPos;
    private int nextCheckPointIndex;

    private void Start()
    {

    }

    public override void CharacterInit()
    {
      
        prevCheckPointCount = 0;
        CheckPointCount = 0;
        SetPosInSpline(spawnPosPersent);
        base.CharacterInit();
    }

    Coroutine GettingCursorPositionRoutineC;
    IEnumerator GettingCursorPositionRoutine()
    {
        bool getNewCheckpoint = NewCheckPointRate();
        CheckPointCount = CheckPointSpawning.instance.GetCheckPointCount();
        while (true)
        {

          

            if (getNewCheckpoint)
            {
                
               targetCursor = CheckPointSpawning.instance.GetNextCheckPointOnScreen(nextCheckPointIndex);

              
               targetCursor.Set(targetCursor.x , targetCursor.y, 0);
            }

            cursor = Vector3.Lerp(cursor, targetCursor, smoothnesControl * Time.deltaTime);
            cursor.x = Mathf.Clamp(cursor.x, horizontalBorderMin, horizontalBorderMax);
            cursor.y = Mathf.Clamp(cursor.y, verticalBorderMin, verticalBorderMax);


          

            if(CheckPointSpawning.instance.GetNextCheckPointOnSpline(nextCheckPointIndex) < GetPosInSpline() && nextCheckPointIndex < CheckPointCount-1)
            {
               
                nextCheckPointIndex++;
                getNewCheckpoint = NewCheckPointRate();
            }
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
        if (Random.value < GetCheckPointRate / 100) return true;
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

    public void SetSpawnPosPersent(float persent)
    {
       
        spawnPosPersent = persent;
    }

    public float GetSpawnPosPersent()
    {
        return spawnPosPersent;
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
