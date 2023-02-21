using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovemant : CharacterController
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 cursor;

    private Vector3 tmpV3 = Vector3.zero;

    [SerializeField] private Transform playerPrefab;
   
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

        //Debug.Log(cursor);

        cursor =  CharacterNewPos(deltaPosition);
    }

    void OnTouchUp(Vector3 lastPos)
    {
       
    }


    Coroutine PlayerCoursorFollowRoutineC;

    IEnumerator PlayerCoursorFollowRoutine()
    {
       

        while (true)
        {
            
            cursor.x = Mathf.Clamp(cursor.x, horizontalBorderMin, horizontalBorderMax);
            cursor.y = Mathf.Clamp(cursor.y, varticalBorderMin, varticalBorderMax);

            
            tmpV3 = transform.position;
            transform.position = Vector3.Lerp(transform.position, cursor, touchControll);

            tmpV3.Set(transform.position.x, transform.position.y, tmpV3.z + flySpeed * Time.deltaTime);
            
            //tmpV3.Set(Mathf.Lerp(transform.position.x, cursor.x, Time.deltaTime), Mathf.Lerp(transform.position.y, cursor.y, Time.deltaTime), transform.position.z + flySpeed * Time.deltaTime);
            
            transform.position = tmpV3;
            PlayerVisualRotating();
            CameraController.instance.PlayerPosUpdate(transform.position);


            yield return new WaitForEndOfFrame();
        }
    }


    private void PlayerVisualRotating()
    {
        int dirX = 0;
        int dirY = 0;

        dirX = cursor.x > 0 ? -1 : cursor.x < 0 ? 1 : 0;
        dirY = cursor.y > 0 ? -1 : cursor.y < 0 ? 1 : 0;

        
       
        tmpV3.Set(Mathf.Lerp(playerPrefab.rotation.z, 30 * dirX, Time.deltaTime* touchControll), Mathf.Lerp(playerPrefab.rotation.x, 30 * dirY, Time.deltaTime*touchControll), 0);

        playerPrefab.rotation = Quaternion.Euler(tmpV3.z, 0, tmpV3.x);
    }
}
