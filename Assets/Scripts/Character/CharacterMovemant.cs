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
    public float CurrentFlySpeed { get { return currentFlySpeed; } set { currentFlySpeed = value;} }

    [Header("Rotation")]
    [SerializeField] private float rotationSmoothness;
    [SerializeField] private float rotationSensetivity;
 
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

        Vector3 cursorPos = characterController.GetCursor();

        Rigidbody objBody = gameObject.GetComponent<Rigidbody>();

        while (true)
        {

            cursor = characterController.GetCursor();

            cursorPos = cursor;// Vector3.Lerp(cursorPos, cursor, Time.deltaTime * touchControl);




            //objBody.transform.Rotate(new Vector3(-cursorPos.y * rotationSensetivity, cursorPos.x * rotationSensetivity, 0f) * rotationSmoothness  * Time.deltaTime, Space.Self);

            //Vector3 euler = objBody.transform.localEulerAngles;
            //euler.y = Mathf.Clamp(euler.y, -35, 35);

            //objBody.transform.localEulerAngles = euler;

            //objBody.transform.Rotate(new Vector3(-cursorPos.y * rotationSensetivity, cursorPos.x * rotationSensetivity));
            Quaternion deltaRotation =  Quaternion.Euler(-cursorPos.y * rotationSensetivity, cursorPos.x * rotationSensetivity, 0f);

            Vector3 newForward = deltaRotation * objBody.transform.forward;
            
            if (Vector3.Angle(Vector3.up,newForward) < 60f || Vector3.Angle(-Vector3.up, newForward) < 60f)
            {
                newForward = objBody.transform.forward;
            }

           
            objBody.transform.forward = newForward;
            //objBody.transform.Rotate(heading * rotationSmoothness * Time.fixedDeltaTime);


            objBody.velocity = objBody.transform.forward * currentFlySpeed;


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
