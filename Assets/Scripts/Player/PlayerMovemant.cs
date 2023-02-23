using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovemant : CharacterController
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 cursor;

    [SerializeField] private Transform visualContainer;
    [SerializeField] private float rotationDiff;
    [SerializeField] private float rotationZAxisSensitivity;

    [SerializeField] private Animator animator;

    void Start()
    {
        cursor.Set(cursor.x, verticalBorderMin, cursor.z);
        PlayerCoursorFollowRoutineC = StartCoroutine(PlayerCoursorFollowRoutine());
        TouchManager.instance.OnTouchDown += OnTouchDown;
        TouchManager.instance.OnTouchDrag += OnTouchDrag;
        TouchManager.instance.OnTouchUp += OnTouchUp;
    }

    void OnTouchDown(Vector3 startPos)
    {
      
            
    }

    void OnTouchDrag(Vector3 currentPos, Vector3 deltaPosition)
    {
   
        Vector3 newCursorPosition = cursor + CharacterNewPos(deltaPosition);

        if (newCursorPosition.y < verticalBorderMin) newCursorPosition.y = verticalBorderMin;
        if (newCursorPosition.y > verticalBorderMax) newCursorPosition.y = verticalBorderMax;

        if (newCursorPosition.x < horizontalBorderMin) newCursorPosition.x = horizontalBorderMin;
        if (newCursorPosition.x > horizontalBorderMax) newCursorPosition.x = horizontalBorderMax;

        cursor = newCursorPosition;

      
    }

    void OnTouchUp(Vector3 lastPos)
    {
       
    }


    Coroutine PlayerCoursorFollowRoutineC;

    IEnumerator PlayerCoursorFollowRoutine()
    {
        Vector3 prevPos = transform.position;

        while (true)
        {

            transform.position += Vector3.forward * flySpeed * Time.deltaTime;

            cursor.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, cursor, touchControl * Time.deltaTime);


            Vector3 diff = transform.position - prevPos;

          
            diff = diff.normalized;

            animator.SetFloat("DirY", diff.y);


            Vector3 upwards = Vector3.up +(Vector3.right  * diff.x * rotationZAxisSensitivity);
          
            visualContainer.rotation = Quaternion.Lerp(visualContainer.rotation, Quaternion.LookRotation(diff, upwards), rotationDiff);

            CameraController.instance.PlayerPosUpdate(transform.position);
            prevPos = transform.position;

            yield return new WaitForEndOfFrame();
        }
    }

}
