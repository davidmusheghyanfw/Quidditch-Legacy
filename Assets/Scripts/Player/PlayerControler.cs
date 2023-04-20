using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : CharacterController
{
    public static PlayerControler instance;
    private SplineSample sample;

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

        RoadGenerator.instance.GetLevelGenerator().Project(EnvironmentManager.instance.GetStartSegment().GetRaceStartPos(), ref sample);
        GetLaneRunner().SetPercent(sample.percent);
        GetLaneRunner().motion.offset = cursor = sample.position;
        CameraController.instance.PlayerPosUpdate(transform.position,GetCharacterVisual());
        base.CharacterInit();
        
    }
 

    void OnTouchDown(Vector3 startPos)
    {


    }
   
    void OnTouchDrag(Vector3 currentPos, Vector3 deltaPosition)
    {

        if (!isStopping)
        {
            Vector3 newCursorPosition = cursor + CharacterNewPos(deltaPosition);

            if (newCursorPosition.y < VerticalBorderRange.x - 10f) newCursorPosition.y = VerticalBorderRange.x - 10f;
            if (newCursorPosition.y > VerticalBorderRange.y + 10f) newCursorPosition.y = VerticalBorderRange.y + 10f;

            if (newCursorPosition.x < HorizontalBorderRange.x - 10f) newCursorPosition.x = HorizontalBorderRange.x - 10f;
            if (newCursorPosition.x > HorizontalBorderRange.y + 10f) newCursorPosition.x = HorizontalBorderRange.y + 10f;

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
