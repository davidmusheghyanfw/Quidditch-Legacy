using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private float smoothnesControl;
    private Vector3 targetCursor;
    int index;
    int prevCheckPointCount;
    int CheckPointCount;

    private void Start()
    {
        
    }

    public override void CharacterInit()
    {
        index = 0;
        prevCheckPointCount = 0;
        CheckPointCount = 0;
        transform.position = new Vector3(Random.Range(horizontalBorderMin, horizontalBorderMax),
            Random.Range(verticalBorderMin, verticalBorderMax), 0);
        cursor = transform.position; 
        StartGettingCursor();
        base.CharacterInit();
    }

    Coroutine GettingCursorPositionRoutineC;
    IEnumerator GettingCursorPositionRoutine()
    {
        
        while (true)
        {
            CheckPointCount = CheckPointSpawning.instance.GetCheckPointCount();

            targetCursor = CheckPointSpawning.instance.GetEnemyGoalCheckPoint(index).position;
            targetCursor.z = 0;
            cursor = Vector3.Lerp(cursor, targetCursor, smoothnesControl);

            if (CheckPointCount - prevCheckPointCount < 0) index -= CheckPointCount - prevCheckPointCount;

            if (CheckPointSpawning.instance.GetEnemyGoalCheckPoint(index).transform.position.z <= transform.position.z
                && index < CheckPointCount) index++;

            prevCheckPointCount = CheckPointCount;
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
    }
}
