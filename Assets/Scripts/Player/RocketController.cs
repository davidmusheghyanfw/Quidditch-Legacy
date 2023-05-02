using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : CharacterController
{
    
    private SplineSample sample;
    [SerializeField] private float borderSmoothness;

    void Start()
    {
        
        TouchManager.instance.OnTouchDown += OnTouchDown;
        TouchManager.instance.OnTouchDrag += OnTouchDrag;
        TouchManager.instance.OnTouchUp += OnTouchUp;
    }


    public override void CharacterInit()
    {
        cursor = Vector3.zero;
      
        base.CharacterInit();
        
    }
 

    void OnTouchDown(Vector3 startPos)
    {


    }
   
    void OnTouchDrag(Vector3 currentPos, Vector3 deltaPosition)
    {

        if (!isStopping)
        {
            //Vector3 newCursorPosition = cursor + CharacterNewPos(deltaPosition);

            Vector3 newCursorPosition = cursor + deltaPosition;
            //if (newCursorPosition.y < VerticalBorderRange.x - borderSmoothness) newCursorPosition.y = VerticalBorderRange.x - borderSmoothness;
            //if (newCursorPosition.y > VerticalBorderRange.y + borderSmoothness) newCursorPosition.y = VerticalBorderRange.y + borderSmoothness;

            //if (newCursorPosition.x < HorizontalBorderRange.x - borderSmoothness) newCursorPosition.x = HorizontalBorderRange.x - borderSmoothness;
            //if (newCursorPosition.x > HorizontalBorderRange.y + borderSmoothness) newCursorPosition.x = HorizontalBorderRange.y + borderSmoothness;

            cursor = newCursorPosition;
        }
        else
        {
            //cursor = transform.position;
        }

    }

    void OnTouchUp(Vector3 lastPos)
    {
        cursor = Vector3.zero;
    }

}
