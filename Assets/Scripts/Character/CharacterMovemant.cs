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
    [SerializeField] private float rotationDiff;
    [SerializeField] private float rotationZAxisSensitivity;
    [SerializeField] private float rotationDelay;
    [Header("Smoothnes")]
    [SerializeField] private float touchControl;
    //[SerializeField] private float smoothnes;
    private Vector3 cursor;
    

    //float tmpFlySpeed;

    public void SetCharacterController( CharacterController controller)
    {
        characterController = controller;
    }
    
    Coroutine CharacterCoursorFollowRoutineC;
    IEnumerator CharacterCoursorFollowRoutine()
    {
        Vector3 newPos = Vector3.zero;
        Vector3 cursorPos = characterController.GetCursor();
        SplineSample sample = new SplineSample();
        while (true)
        {


            //if (characterController.GetPosInSpline() <= 1f)
            ////dot = RoadGenerator.instance.GetSplineComputer().Evaluate(characterController.GetPosInSpline());
            //EnvironmentManager.instance.GetLevelGenerator().Evaluate(characterController.GetPosInSpline(), ref dot);
            ////splineSamples.Add(dot);
            
            cursor = characterController.GetCursor();

            cursor.z = 0;

            RoadGenerator.instance.GetLevelGenerator().Project(characterController.GetCharacter().transform.position, ref sample);

            characterController.SetSplineSample(sample);


            cursorPos = Vector3.Lerp(cursorPos, cursor, Time.deltaTime * touchControl);
            //cursorPrevPos = Vector3.MoveTowards(cursorPrevPos, cursor, Time.deltaTime * touchControl);
            //newPos = sample.position;

            //transform.position += newPos;

            if (cursorPos.y < characterController.VerticalBorderRange.x) cursorPos.y = characterController.VerticalBorderRange.x;
            if (cursorPos.y > characterController.VerticalBorderRange.y) cursorPos.y = characterController.VerticalBorderRange.y;

            if (cursorPos.x < characterController.HorizontalBorderRange.x) cursorPos.x = characterController.HorizontalBorderRange.x;
            if (cursorPos.x > characterController.HorizontalBorderRange.y) cursorPos.x = characterController.HorizontalBorderRange.y;
            characterController.GetLaneRunner().motion.offset = new Vector2(cursorPos.x, cursorPos.y);

            //characterController.GetCharacter().position =  Vector3.Lerp(characterController.GetCharacter().position, newPos, Time.deltaTime * smoothnes);
            characterController.GetCharacterVisual().rotation = sample.rotation;


            ////Vector3 diff = (cursor - dot.position).normalized;
            ////characterController.GetAnimator().SetFloat("DirY", diff.y);

           

            if (characterController is PlayerControler)
            CameraController.instance.PlayerPosUpdate(PlayerControler.instance.gameObject.transform.position,PlayerControler.instance.GetCharacterVisual());
      


            //characterController.SetPosInSpline(characterController.GetPosInSpline()
            //+ tmpFlySpeed / RoadGenerator.instance.GetDistance());

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
