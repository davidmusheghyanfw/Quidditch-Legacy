using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private CharacterMovemant characterMovemant;

    [SerializeField] protected float sensetivity;
    [SerializeField] protected float verticalBorderMax;
    [SerializeField] protected float verticalBorderMin;
    [SerializeField] protected float horizontalBorderMax;
    [SerializeField] protected float horizontalBorderMin;

    [SerializeField] protected Vector3 cursor;
    [SerializeField] private Transform visualContainer;
    [SerializeField] private Animator animator;

    protected bool isStopping = false;

    private Vector3 pos;

   
    public Vector3 CharacterNewPos(Vector3 deltaPos)
    {
        pos = new Vector3(deltaPos.x, deltaPos.y, 0) * sensetivity;
        return pos;

    }

    public virtual void CharacterInit()
    {

        characterMovemant.StopCursorFollowing();
        transform.position = cursor;
        isStopping = false;
        characterMovemant.StartCursorFollowing();
        
    }

   

    public Transform GetCharacter()
    {
        return gameObject.transform;
    }

    public Animator GetAnimator()
    {
        return animator;
    }
    public Vector3 GetCursor()
    {
        return cursor;
    }

    public Transform GetCharacterVisual()
    {
        return visualContainer;
    }

    public bool IsStopping()
    {
        return isStopping;
    }
    public void SetStopState(bool value)
    {
        isStopping = value;
    }

    public void OnGameWin()
    {
        StartCharacterStoppingRoutin();
    }

    public void GameStopped()
    {
        StopCharacterStoppingRoutin();
    }

    public void GameResume()
    {
        StopCharacterStoppingRoutin();
        StartCursorFollowing();
    }

    public void StartCharacterStoppingRoutin()
    {
        characterMovemant.StartCharacterStoppingRoutin();
    }
     public void StopCharacterStoppingRoutin()
    {
        characterMovemant.StopCharacterStoppingRoutin();
    }

     public void StartCursorFollowing()
    {
        characterMovemant.StartCursorFollowing();
    }


}
