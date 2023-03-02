using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [SerializeField] private float smoothnesControl;
    [SerializeField] private float GetCheckPointRate;
    [SerializeField] private Rigidbody rb;
    private Vector3 targetCursor;
    private Vector3 randomPos;
    int index;
    int prevCheckPointCount;
    int CheckPointCount;
    Vector3 pushedPos;

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
        cursor = targetCursor = transform.position;
        StartGettingCursor();
        base.CharacterInit();
    }

    Coroutine GettingCursorPositionRoutineC;
    IEnumerator GettingCursorPositionRoutine()
    {
        bool getNewCheckpoint = NewCheckPointRate();

        while (true)
        {
            CheckPointCount = CheckPointSpawning.instance.GetCheckPointCount();

            if (getNewCheckpoint)
            {
            }
                targetCursor = CheckPointSpawning.instance.GetEnemyGoalCheckPoint(index);
                //randomPos = Random.insideUnitCircle * 125;
                targetCursor.Set(targetCursor.x + randomPos.x, targetCursor.y + randomPos.y, 0);

            cursor = Vector3.Lerp(cursor, targetCursor, smoothnesControl * Time.deltaTime);
            cursor.x = Mathf.Clamp(cursor.x, horizontalBorderMin, horizontalBorderMax);
            cursor.y = Mathf.Clamp(cursor.y, verticalBorderMin, verticalBorderMax);


            if (CheckPointCount - prevCheckPointCount < 0) index -= CheckPointCount - prevCheckPointCount;

            if (CheckPointSpawning.instance.GetEnemyGoalCheckPoint(index).z <= transform.position.z
                && index < CheckPointCount)
            {
                getNewCheckpoint = NewCheckPointRate();
                index++;
            }
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
        //rb.AddForce(Vector3.right * 300 * dir);
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
}
