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
        int index = 0;
        while (true)
        {

            Debug.Log(index);
            cursor = Vector3.Lerp(transform.position, CheckPointSpawning.instance.GetCheckPointByIndex(index).position, 0.2f);
            if(CheckPointSpawning.instance.GetCheckPointByIndex(index).position.z <= transform.position.z 
                && index < CheckPointSpawning.instance.GetCheckPointCount()) index++;
            //cursor.Set(Random.Range(horizontalBorderMin, horizontalBorderMax), Random.Range(verticalBorderMin, verticalBorderMax), 0);
            //yield return new WaitForSeconds(4f);
            yield return new WaitForEndOfFrame();
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
