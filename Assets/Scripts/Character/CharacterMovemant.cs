using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CharacterMovemant : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [Header("Speed")]
    [SerializeField] private float minFlySpeed;
    [SerializeField] private float maxFlySpeed;
    [SerializeField] private float currentFlySpeed;
    public float MinFlySpeed { get { return minFlySpeed; } }
    public float MaxFlySpeed { get { return maxFlySpeed; } }
    public float CurrentFlySpeed { get { return currentFlySpeed; } set { currentFlySpeed = value; characterController.GetLaneRunner().followSpeed = value; } }

    [Header("Rotation")]
    [SerializeField] private float rotationSmoothness;
    [SerializeField] private float movementTiltAngle;
    [SerializeField] private float rotationSensetivity;
    [SerializeField] private Vector2 horizontalRotantionBorder;
    [SerializeField] private Vector2 verticalRotantionBorder;
    [Header("Smoothnes")]
    [SerializeField] private float touchControl;

    private Vector3 cursor;


    //float tmpFlySpeed;

    public void SetCharacterController(CharacterController controller)
    {
        characterController = controller;
    }

    Coroutine CharacterCoursorFollowRoutineC;
    IEnumerator CharacterCoursorFollowRoutine()
    {
        Vector3 newPos = Vector3.zero;
        Vector3 prevMousPos = Vector3.zero;
        Vector3 cursorPos = characterController.GetCursor();
        SplineSample sample = new SplineSample();
        while (true)
        {

            cursor = characterController.GetCursor();

            cursor.z = 0;

            RoadGenerator.instance.GetLevelGenerator().Project(characterController.GetCharacter().transform.position, ref sample);

            characterController.SetSplineSample(sample);


            cursorPos = Vector3.Lerp(cursorPos, cursor, Time.deltaTime * touchControl);

            if (cursorPos.y < characterController.VerticalBorderRange.x) cursorPos.y = characterController.VerticalBorderRange.x;
            if (cursorPos.y > characterController.VerticalBorderRange.y) cursorPos.y = characterController.VerticalBorderRange.y;

            if (cursorPos.x < characterController.HorizontalBorderRange.x) cursorPos.x = characterController.HorizontalBorderRange.x;
            if (cursorPos.x > characterController.HorizontalBorderRange.y) cursorPos.x = characterController.HorizontalBorderRange.y;
            characterController.GetLaneRunner().motion.offset = new Vector2(cursorPos.x, cursorPos.y);

            //characterController.GetCharacter().position =  Vector3.Lerp(characterController.GetCharacter().position, newPos, Time.deltaTime * smoothnes);

            
            Vector3 tiltDirection = new Vector3(-cursorPos.y + rotationSensetivity, cursorPos.x + rotationSensetivity, 0);



            Vector3 rot = Vector3.Slerp(
            characterController.GetCharacterVisual().localEulerAngles,
            tiltDirection.normalized * movementTiltAngle,
            Time.deltaTime * rotationSmoothness);

            if (characterController is PlayerControler)
                Debug.Log(rot);

            Vector3 euler = rot;

            if (euler.x < horizontalRotantionBorder.x) euler.x = horizontalRotantionBorder.x;
            if (euler.x > horizontalRotantionBorder.y) euler.x = horizontalRotantionBorder.y;

            if (euler.y < verticalRotantionBorder.x) euler.y = verticalRotantionBorder.x;
            if (euler.y > verticalRotantionBorder.y) euler.y = verticalRotantionBorder.y;
            euler.z = 0;

            
            characterController.GetCharacterVisual().localRotation = Quaternion.Euler(euler);

            if (characterController is PlayerControler)
                CameraController.instance.PlayerPosUpdate(PlayerControler.instance.gameObject.transform.position, PlayerControler.instance.GetCharacterVisual());

            prevMousPos = cursorPos;
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
        float tmp = CurrentFlySpeed;
        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 5f;
            CurrentFlySpeed = Mathf.Lerp(tmp, 0, t);
            yield return new WaitForEndOfFrame();
        }
        if (characterController.IsStopping()) StopCursorFollowing();
        //else characterController.StartForceRoutine();
    }



}
