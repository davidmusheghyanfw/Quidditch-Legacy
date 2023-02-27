using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{

    private void Start()
    {
        StartCursorFollowing();
    }

    Coroutine GettingCursorPositionRoutineC;
    IEnumerator GettingCursorPositionRoutine()
    {
        while (true)
        {
            cursor.Set(Random.Range(horizontalBorderMin, horizontalBorderMax), Random.Range(verticalBorderMin, verticalBorderMax), 0);
            yield return new WaitForSeconds(4f);
        }
        
    }

    public void StartCursorFollowing()
    {
        if (GettingCursorPositionRoutineC != null) StopCoroutine(GettingCursorPositionRoutineC);
        GettingCursorPositionRoutineC = StartCoroutine(GettingCursorPositionRoutine());

    }

    public void StopCursorFollowing()
    {
        if (GettingCursorPositionRoutineC != null) StopCoroutine(GettingCursorPositionRoutineC);
    }
}
