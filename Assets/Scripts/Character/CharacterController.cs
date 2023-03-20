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

    [SerializeField, Range(0, 1)] private double posInSpline = 0f; 

    protected bool isStopping = false;

    private Vector3 pos;

    public virtual void CharacterInit()
    {
        
        StopCursorFollowing();
        characterMovemant.SetCurrentSpeed(characterMovemant.GetDefaultSpeed());
       
        transform.position = cursor;
        isStopping = false;
    }

    public float VerticalBorderMax()
    {
        return verticalBorderMax;
    }
    public float VerticalBorderMin()
    {
        return verticalBorderMin;
    }
     public float HorizontalBorderMax()
    {
        return horizontalBorderMax;
    }
    public float HorizontalBorderMin()
    {
        return horizontalBorderMin;
    }

    public Vector3 CharacterNewPos(Vector3 deltaPos)
    {
        pos = new Vector3(deltaPos.x, deltaPos.y, 0) * sensetivity;
        return pos;

    }
    public double GetPosInSpline()
    {
        return posInSpline;
    }
    public void SetPosInSpline(double value)
    {
        posInSpline = value;
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
     public void SetCursor(Vector3 pos)
    {
        cursor = pos;
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

    public float GetSensetivity()
    {
        return sensetivity;
    }

    public void SetSensetivity( float value)
    {
        sensetivity = value;
    }
    public CharacterMovemant GetCharacterMovemant()
    {
        return characterMovemant;
    }

    public void OnGameWin()
    {
        StopCursorFollowing();
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
    public void StopCursorFollowing()
    {
        characterMovemant.StopCursorFollowing();
    }

    Coroutine AddForceRoutineC;
    private IEnumerator AddForceRoutine()
    {
        
        float t = 0.0f;
        float startTime = Time.fixedTime;

        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 0.5f;
            characterMovemant.SetCurrentSpeed(Mathf.Lerp(characterMovemant.GetCurrentSpeed(), characterMovemant.GetDefaultSpeed()+(characterMovemant.GetDefaultSpeed()/2), t));
               // characterMovemant.GetSpeed() + (10 / characterMovemant.GetSpeed()), t));
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        t = 0.0f;
        startTime = Time.fixedTime;
        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 1.5f;
            characterMovemant.SetCurrentSpeed(Mathf.Lerp(characterMovemant.GetCurrentSpeed(), characterMovemant.GetDefaultSpeed(), t));
            yield return new WaitForEndOfFrame();
        }
    }


    public void StartAddForceRoutine()
    {
        if (AddForceRoutineC != null) StopCoroutine(AddForceRoutineC);
        AddForceRoutineC = StartCoroutine(AddForceRoutine());

    }

    public void StopAddForceRoutine()
    {
        if (AddForceRoutineC != null) StopCoroutine(AddForceRoutineC);
    }
}
