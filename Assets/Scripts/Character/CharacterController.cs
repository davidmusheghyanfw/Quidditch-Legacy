using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected float flySpeed;
    [SerializeField] protected float verticalBorderMax;
    [SerializeField] protected float verticalBorderMin;
    [SerializeField] protected float horizontalBorderMax;
    [SerializeField] protected float horizontalBorderMin;

    [SerializeField] protected float sensetivity;
    [SerializeField] protected float touchControl;

    [SerializeField] protected float horizontalRotationAmount;
    [SerializeField] protected float verticalRotationAmount;
   
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
       
        
        pos = new  Vector3(deltaPos.x , deltaPos.y, 0) * sensetivity;


        return pos;

    }


   
}
