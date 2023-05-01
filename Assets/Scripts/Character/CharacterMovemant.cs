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
    [SerializeField] private float rotationDelay;
    [SerializeField] private float rotationSensetivity;
    [SerializeField] private float rotationZAxisSpeed;
 
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
        Vector3 prevPos = transform.position;
        Vector3 cursorPos = characterController.GetCursor();
        Vector3 prevCursorPos = Vector3.zero;

        var visual = characterController.GetMainVisualContainer();
        SplineSample sample = new SplineSample();
        var targetDirection = transform.forward;
        while (true)
        {

            cursor = characterController.GetCursor();
            cursorPos = Vector3.Lerp(cursorPos, cursor, Time.deltaTime * touchControl);
            //print(cursorPos);
            Rigidbody objBody = gameObject.GetComponent<Rigidbody>();

            //var temp = cursorPos.x;
            //cursorPos.x = cursorPos.y;
            //cursorPos.y = temp;
            
            //targetDirection = Quaternion.Euler(cursorPos * rotationSensetivity) * targetDirection;

            //var rotation = Quaternion.LookRotation(targetDirection);
            //objBody.MoveRotation(Quaternion.Lerp(objBody.transform.rotation, rotation, Time.deltaTime * rotationSmoothness));
            objBody.transform.Rotate(new Vector3(-cursorPos.y, cursorPos.x, 0f) * rotationSensetivity * Time.deltaTime, Space.Self);

            objBody.velocity = (objBody.transform.forward) * currentFlySpeed;



            prevCursorPos = cursor;

            prevPos = transform.position;



            //cursor.z = 0;

            ////RoadGenerator.instance.GetLevelGenerator().Project(characterController.GetCharacter().transform.position, ref sample);

            ////characterController.SetSplineSample(sample);
            ////GameView.instance.SetPlayerCurrentPos((float)sample.percent);

            //cursorPos = Vector3.Lerp(cursorPos, cursor, Time.deltaTime * touchControl);

            ////if (cursorPos.y < characterController.VerticalBorderRange.x) cursorPos.y = characterController.VerticalBorderRange.x;
            ////if (cursorPos.y > characterController.VerticalBorderRange.y) cursorPos.y = characterController.VerticalBorderRange.y;

            ////if (cursorPos.x < characterController.HorizontalBorderRange.x) cursorPos.x = characterController.HorizontalBorderRange.x;
            ////if (cursorPos.x > characterController.HorizontalBorderRange.y) cursorPos.x = characterController.HorizontalBorderRange.y;
            ////characterController.GetLaneRunner().motion.offset = new Vector2(cursorPos.x, cursorPos.y);

            ////characterController.GetCharacter().position =  Vector3.Lerp(characterController.GetCharacter().position, newPos, Time.deltaTime * smoothnes);



            //gameObject.GetComponent<Rigidbody>().velocity = (transform.forward + ((cursorPos-prevCursorPos).normalized * rotationSensetivity )) * currentFlySpeed;

            //Vector3 diff = transform.position - prevPos;

            //diff = diff.normalized + Vector3.forward * rotationDelay;

            //Vector3 upwards = Vector3.up + (Vector3.up * diff.x * rotationSensetivity);

            ////gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(diff, upwards), Time.deltaTime * rotationSmoothness));

            ////gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(diff, upwards), Time.deltaTime * rotationSmoothness));


            //var heading = cursorPos - transform.position;
            //heading.z = transform.position.z;
            //var rotation = Quaternion.LookRotation(heading * rotationSensetivity);
            //gameObject.GetComponent<Rigidbody>().MoveRotation(rotation);
            ////StartCharachterRotatingRoutine();

            ////if (characterController is RocketController)
            ////    CameraController.instance.PlayerPosUpdate(Launcher.instance.GetRocketController().gameObject.transform.position, Launcher.instance.GetRocketController().GetMainVisualContainer());


            //prevPos = transform.position;
            //prevCursorPos = cursorPos;
            yield return new WaitForFixedUpdate();
        }
    }


    

    Coroutine CharachterRotatingRoutineC;
    IEnumerator CharachterRotatingRoutine()
    {
        var visual = characterController.GetSecondVisualContainer();
        while (true)
        {
            visual.Rotate(transform.forward, 360 * rotationZAxisSpeed* Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }


    public void StartCharachterRotatingRoutine()
    {

        if (CharachterRotatingRoutineC != null) StopCoroutine(CharachterRotatingRoutineC);
        CharachterRotatingRoutineC = StartCoroutine(CharachterRotatingRoutine());

    }

    public void StopCharachterRotatingRoutine()
    {
        if (CharachterRotatingRoutineC != null) StopCoroutine(CharachterRotatingRoutineC);
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
