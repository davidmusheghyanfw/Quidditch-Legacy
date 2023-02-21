using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected float flySpeed;
    [SerializeField] protected float varticalBorderMax;
    [SerializeField] protected float varticalBorderMin;
    [SerializeField] protected float horizontalBorderMax;
    [SerializeField] protected float horizontalBorderMin;

    [SerializeField] private float sensetivity;
    [SerializeField] protected float touchControll;
   
    //private float pitch;
    //private float roll;

    //private float verticalAxis;
    //private float horizontalAxis;
    private Vector3 pos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public Vector3 CharacterNewPos(Vector3 deltaPos)// float verticalDirection = 0, float horizontalDirection = 0)
    {
        
        pos = new  Vector3(deltaPos.x , deltaPos.y, 0)*sensetivity;


        return pos;
        //pitch = Mathf.Lerp(varticalPitchMin, varticalPitchMax, Mathf.Abs(verticalDirection)) * Mathf.Sign(verticalDirection);
        //roll = Mathf.Lerp(horizontalRollMin, horizontalRollMax, Mathf.Abs(horizontalDirection)) * -Mathf.Sign(horizontalDirection);

        //transform.localRotation = Quaternion.Euler(Vector3.right * pitch + Vector3.forward * roll);
        //transform.Translate(horizontalDirection * Time.deltaTime * flySpeed, 0, 0);

    }


   
}
