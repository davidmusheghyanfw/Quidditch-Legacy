using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
    //private void OnDrawGizmos()
    //{
    //    if (characterController is not PlayerControler) return;
    //    foreach(var dot in splineSamples)
    //    {
    //        Gizmos.DrawSphere(dot.position + Vector3.up * 3, 1f);
    //    }
    //}
    //List<SplineSample> splineSamples = new List<SplineSample>();

    Coroutine CharacterCoursorFollowRoutineC;
    IEnumerator CharacterCoursorFollowRoutine()
    {
        visual = characterController.GetCharacterVisual();
        Vector3 newPos = Vector3.zero;

        while (true)
        {
          

            if(characterController.GetCurrentDistancePercent() <= 1f)
            dot = RoadGenerator.instance.GetSplineComputer().Evaluate(characterController.GetCurrentDistancePercent());
            //splineSamples.Add(dot);
            cursor = characterController.GetCursor();

            cursor.z = 0;
           
            newPos = dot.position;
            newPos += cursor.x * dot.right + cursor.y * dot.up;


            characterController.GetCharacter().position =  Vector3.Lerp(characterController.GetCharacter().position, newPos, Time.deltaTime * touchControl);
            characterController.GetCharacterVisual().rotation = dot.rotation;


            Vector3 diff = (cursor - dot.position).normalized;
            characterController.GetAnimator().SetFloat("DirY", diff.y);


       
            if(characterController is PlayerControler)
            CameraController.instance.PlayerPosUpdate(PlayerControler.instance.gameObject.transform.position,PlayerControler.instance.GetCharacterVisual());
      


            characterController.SetCurrentDistancePercent(characterController.GetCurrentDistancePercent()
            + tmpFlySpeed / RoadGenerator.instance.GetDistance());

            yield return new WaitForFixedUpdate();
        }
    }

    public void StartCursorFollowing()
    {
       
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
