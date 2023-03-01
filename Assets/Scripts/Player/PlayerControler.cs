using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : CharacterController
{
    public static PlayerControler instance;

    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TouchManager.instance.OnTouchDown += OnTouchDown;
        TouchManager.instance.OnTouchDrag += OnTouchDrag;
        TouchManager.instance.OnTouchUp += OnTouchUp;
    }


    public override void CharacterInit()
    {
        cursor.Set(cursor.x, verticalBorderMin, 0);
        CameraController.instance.PlayerPosUpdate(cursor);
        base.CharacterInit();
    }

    void OnTouchDown(Vector3 startPos)
    {


    }
    //public void StartCameraFollow()
    //{
    //    if (CameraFollowRoutineC != null) StopCoroutine(CameraFollowRoutineC);
    //    CameraFollowRoutineC = StartCoroutine(CameraFollowRoutine());

    //}

    //public void StopCameraFollow()
    //{
    //    if (CameraFollowRoutineC != null) StopCoroutine(CameraFollowRoutineC);
    //}

    //Coroutine CameraFollowRoutineC;
    //public IEnumerator CameraFollowRoutine()
    //{
    //    while (true)
    //    {
    //    CameraController.instance.PlayerPosUpdate(transform.position);
    //    yield return new WaitForEndOfFrame();

    //    }      
    //}
    void OnTouchDrag(Vector3 currentPos, Vector3 deltaPosition)
    {

        if (!isStopping)
        {
            Vector3 newCursorPosition = cursor + CharacterNewPos(deltaPosition);

            if (newCursorPosition.y < verticalBorderMin) newCursorPosition.y = verticalBorderMin;
            if (newCursorPosition.y > verticalBorderMax) newCursorPosition.y = verticalBorderMax;

            if (newCursorPosition.x < horizontalBorderMin) newCursorPosition.x = horizontalBorderMin;
            if (newCursorPosition.x > horizontalBorderMax) newCursorPosition.x = horizontalBorderMax;

            cursor = newCursorPosition;
        }
        else
        {
            cursor = transform.position;
        }
    }

    void OnTouchUp(Vector3 lastPos)
    {

    }

}
