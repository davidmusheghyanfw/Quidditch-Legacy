using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovemant : CharacterController
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 cursor;
   
    private Vector3 playerDistance;
    private Vector3 tmpV3 = Vector3.zero;
    private Vector3 tmpV3Rot = Vector3.zero;

    [SerializeField] private Transform visualContainer;

   

    void Start()
    {
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

       
        if(deltaPosition.x != 0 || deltaPosition.y != 0)
        cursor =  CharacterNewPos(deltaPosition - cursor);

        playerDistance = transform.position + cursor;
    }

    void OnTouchUp(Vector3 lastPos)
    {
       
    }


    Coroutine PlayerCoursorFollowRoutineC;

    IEnumerator PlayerCoursorFollowRoutine()
    {
       

        while (true)
        {
            

            tmpV3 = transform.position;
           

            tmpV3.Set(Mathf.Lerp(transform.position.x, playerDistance.x,  touchControl), Mathf.Lerp(transform.position.y, playerDistance.y, touchControl), transform.position.z + flySpeed * Time.deltaTime);

           
            tmpV3Rot = tmpV3;
            
            
            tmpV3.y = Mathf.Clamp(tmpV3.y, varticalBorderMin, varticalBorderMax);
            tmpV3.x = Mathf.Clamp(tmpV3.x, horizontalBorderMin, horizontalBorderMax);
            
            transform.position = tmpV3;

            tmpV3Rot.y = Mathf.Clamp(tmpV3Rot.y, -verticalRotationAmount, verticalRotationAmount);
            tmpV3Rot.x = Mathf.Clamp(tmpV3Rot.x, -horizontalRotationAmount, horizontalRotationAmount);


            visualContainer.rotation = Quaternion.Euler(-tmpV3Rot.y, 0, -tmpV3Rot.x);


            CameraController.instance.PlayerPosUpdate(transform.position);
            

            yield return new WaitForEndOfFrame();
        }
    }

}
