using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{

    int index = 0;
    int prevCheckPointCount = 0;
    int CheckPointCount = 0;

    private void Start()
    {
        
    }

    public override void CharacterInit()
    {
        StartGettingCursor();
        base.CharacterInit();
    }

    Coroutine GettingCursorPositionRoutineC;
    IEnumerator GettingCursorPositionRoutine()
    {
        
        while (true)
        {
            CheckPointCount = CheckPointSpawning.instance.GetCheckPointCount();

            cursor = CheckPointSpawning.instance.GetEnemyGoalCheckPoint(index).position;
            cursor.z = 0;
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
