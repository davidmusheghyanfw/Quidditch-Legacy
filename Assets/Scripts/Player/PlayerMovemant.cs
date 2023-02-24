using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovemant : CharacterController
{
    public static PlayerMovemant instance;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 cursor;

    [SerializeField] private Transform visualContainer;
    [SerializeField] private float rotationDiff;
    [SerializeField] private float rotationZAxisSensitivity;
    [SerializeField] private float rotationDelay;

    [SerializeField] private Animator animator;

    float tmpFlySpeed;
    bool isStopping = false;

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

    public void PlayerInit()
    {
        tmpFlySpeed = flySpeed;
        StopCursorFollowing();
        cursor.Set(cursor.x, verticalBorderMin, 0);
        transform.position = cursor;
        isStopping = false;
        StartCursorFollowing();
    }

    void OnTouchDown(Vector3 startPos)
    {
      
            
    }

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
           // rb.velocity = Vector3.forward * flySpeed *

            transform.position += Vector3.forward * tmpFlySpeed * Time.deltaTime;

            cursor.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, cursor, touchControl * Time.deltaTime);


            Vector3 diff = transform.position - prevPos;


            diff = diff.normalized + Vector3.forward* rotationDelay;

            animator.SetFloat("DirY", diff.y);


            Vector3 upwards = Vector3.up +(Vector3.right  * diff.x * rotationZAxisSensitivity);
          
            visualContainer.rotation = Quaternion.Lerp(visualContainer.rotation, Quaternion.LookRotation(diff, upwards), rotationDiff);

            CameraController.instance.PlayerPosUpdate(transform.position);
            prevPos = transform.position;

            GameView.instance.UpdateScore();
            yield return new WaitForEndOfFrame();
        }
    }

    private void StartCursorFollowing()
    {
        if (PlayerCoursorFollowRoutineC != null) StopCoroutine(PlayerCoursorFollowRoutineC);
        PlayerCoursorFollowRoutineC = StartCoroutine(PlayerCoursorFollowRoutine());

    }

    private void StopCursorFollowing()
    {
        if (PlayerCoursorFollowRoutineC != null) StopCoroutine(PlayerCoursorFollowRoutineC);
    }

    private Coroutine PlayerStoppingRoutinC;
    private IEnumerator PlayerStoppingRoutin()
    {
        isStopping = true;
        float t = 0.0f;
        float startTime = Time.fixedTime;
        cursor = transform.position ;
        while(t<1)
        {
            t = (Time.fixedTime - startTime)/1;
            tmpFlySpeed = Mathf.Lerp(tmpFlySpeed, 0, t);
            yield return new WaitForEndOfFrame();
        }
        if (isStopping) StopCursorFollowing();
        else tmpFlySpeed = flySpeed;
    }

    public Transform GetPlayer()
    {
        return gameObject.transform;
    }

    public void OnGameWin()
    {
        if (PlayerStoppingRoutinC != null) StopCoroutine(PlayerStoppingRoutinC);
        PlayerStoppingRoutinC = StartCoroutine(PlayerStoppingRoutin());
    }
}
