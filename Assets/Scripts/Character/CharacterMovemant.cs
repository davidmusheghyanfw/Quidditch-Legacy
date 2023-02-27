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
    

    public void SetCharacterController( CharacterController controller)
    {
        characterController = controller;
    }

    Coroutine CharacterCoursorFollowRoutineC;
    IEnumerator CharacterCoursorFollowRoutine()
    {
        visual = characterController.GetCharacterVisual();
        Vector3 prevPos = transform.position;
        Debug.Log("s");
        while (true)
        {
            // rb.velocity = Vector3.forward * flySpeed *

            cursor = characterController.GetCursor();

            characterController.GetCharacter().position += Vector3.forward * tmpFlySpeed * Time.deltaTime;

            cursor.z = characterController.GetCharacter().position.z;

            characterController.GetCharacter().position = Vector3.Lerp(transform.position, cursor, touchControl * Time.deltaTime);


            Vector3 diff = transform.position - prevPos;


            diff = diff.normalized + Vector3.forward * rotationDelay;

            characterController.GetAnimator().SetFloat("DirY", diff.y);


            Vector3 upwards = Vector3.up + (Vector3.right * diff.x * rotationZAxisSensitivity);

            visual.rotation = Quaternion.Lerp(visual.rotation, Quaternion.LookRotation(diff, upwards), rotationDiff);

            CameraController.instance.PlayerPosUpdate(transform.position);
            prevPos = transform.position;

            GameView.instance.UpdateScore();
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
        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 1;
            tmpFlySpeed = Mathf.Lerp(tmpFlySpeed, 0, t);
            yield return new WaitForEndOfFrame();
        }
        if (characterController.IsStopping()) StopCursorFollowing();
        else tmpFlySpeed = flySpeed;
    }

}
