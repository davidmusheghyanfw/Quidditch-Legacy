using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovemant : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float flySpeed;
  
    [SerializeField] private float rotationDiff;
    [SerializeField] private float rotationZAxisSensitivity;
    [SerializeField] private float rotationDelay;

    [SerializeField] private float touchControl;

    private Vector3 cursor;
    private Transform visual;

    float tmpFlySpeed;
    SplineSample dot;

    public void SetCharacterController( CharacterController controller)
    {
        characterController = controller;
    }

    Coroutine CharacterCoursorFollowRoutineC;
    IEnumerator CharacterCoursorFollowRoutine()
    {
        visual = characterController.GetCharacterVisual();
        SplineSample prevDot = RoadGenerator.instance.GetSplineComputer().Evaluate(characterController.GetCurrentDistancePercent());
        Vector3 newPos = Vector3.zero;

        while (true)
        {
            if (characterController.GetCurrentDistancePercent() <= 1f)
            {
                characterController.SetCurrentDistancePercent(characterController.GetCurrentDistancePercent()
                + (tmpFlySpeed / RoadGenerator.instance.GetDistance()) / 100);

            }
            else
                characterController.SetCurrentDistancePercent(1f);

            dot = RoadGenerator.instance.GetSplineComputer().Evaluate(characterController.GetCurrentDistancePercent());
            
            cursor = characterController.GetCursor();

            cursor.z = 0;
            newPos = dot.position;
            newPos += cursor.x * dot.right + cursor.y * dot.up;
            
            
            
            //dot.position.x += cursor.x;
            //dot.position.y += cursor.y;
            
            characterController.GetCharacter().position = newPos;
            characterController.GetCharacterVisual().rotation = dot.rotation;

            Vector3 diff = (cursor - prevDot.position).normalized;
            characterController.GetAnimator().SetFloat("DirY", diff.y);


        //    // rb.velocity = Vector3.forward * flySpeed *

        

        //    characterController.GetCharacter().position += Vector3.forward * tmpFlySpeed * Time.deltaTime;

        //    cursor.z = characterController.GetCharacter().position.z;

        //    characterController.GetCharacter().position = Vector3.Lerp(characterController.GetCharacter().position, cursor, touchControl * Time.deltaTime);




            //diff = diff.normalized + Vector3.forward * rotationDelay;



          //  Vector3 upwards = Vector3.up + (Vector3.right * diff.x * rotationZAxisSensitivity);

         //   visual.rotation = Quaternion.Lerp(visual.rotation, Quaternion.LookRotation(diff, upwards), rotationDiff);

            CameraController.instance.PlayerPosUpdate(PlayerControler.instance.gameObject.transform.position,PlayerControler.instance.GetCharacterVisual());
            prevDot = dot;

        //    GameView.instance.UpdateScore();
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartCursorFollowing()
    {
        tmpFlySpeed = flySpeed;
        if (CharacterCoursorFollowRoutineC != null) StopCoroutine(CharacterCoursorFollowRoutineC);
        CharacterCoursorFollowRoutineC = StartCoroutine(CharacterCoursorFollowRoutine());

    }

    public void StopCursorFollowing()
    {
        if (CharacterCoursorFollowRoutineC != null) StopCoroutine(CharacterCoursorFollowRoutineC);
    }
    public void StartCharacterStoppingRoutin()
    {
        if (CharacterStoppingRoutinC != null) StopCoroutine(CharacterStoppingRoutinC);
        CharacterStoppingRoutinC = StartCoroutine(CharacterStoppingRoutin());
        characterController.SetStopState(true);

    }

    public void StopCharacterStoppingRoutin()
    {
        if (CharacterStoppingRoutinC != null) StopCoroutine(CharacterStoppingRoutinC);
        characterController.SetStopState(false);
    }



    private Coroutine CharacterStoppingRoutinC;
    private IEnumerator CharacterStoppingRoutin()
    {
        float t = 0.0f;
        float startTime = Time.fixedTime;
        cursor = characterController.GetCharacter().position;
        visual.rotation = Quaternion.Euler(0, 0, 0);


        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 1;
            tmpFlySpeed = Mathf.Lerp(tmpFlySpeed, 0, t);
            yield return new WaitForEndOfFrame();
        }
        if (characterController.IsStopping()) StopCursorFollowing();
        else tmpFlySpeed = flySpeed;
    }

    public float GetDefaultSpeed()
    {
        return flySpeed;
    }
    public float GetCurrentSpeed()
    {
        return tmpFlySpeed;
    }
    public void SetCurrentSpeed(float value)
    {
        tmpFlySpeed = value;
    }
}
