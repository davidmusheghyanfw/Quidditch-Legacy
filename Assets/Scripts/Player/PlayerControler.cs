using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : CharacterController
{
    public static PlayerControler instance;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private SplineProjector splineProjector;

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
       
        transform.position = cursor = Vector3.zero;
        CameraController.instance.PlayerPosUpdate(cursor,GetCharacterVisual());
        SetPosInSpline(0);
        //base.CharacterInit();
        
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
        else
        {
            cursor = transform.position;
        }

    }

    void OnTouchUp(Vector3 lastPos)
    {

    }

}
