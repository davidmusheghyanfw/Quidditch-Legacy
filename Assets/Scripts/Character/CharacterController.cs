using Dreamteck.Forever;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private CharacterMovemant characterMovemant;
    [Header("Mouse")]
    [SerializeField] protected float sensetivity;
    [SerializeField] protected Vector3 cursor;
    [Header("Borders")]
    [SerializeField] private Vector2 verticalBorderRange;
    [SerializeField] private Vector2 horizontalBorderRange;
    public Vector2 VerticalBorderRange { get { return verticalBorderRange; } }
    public Vector2 HorizontalBorderRange { get { return horizontalBorderRange; }}

    [Header("Spline")]
    private SplineSample sample;
    [SerializeField, Range(0, 1)] private double rebornPosition = 0f; 
    
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private LaneRunner laneRunner;
    
    [Header("Character")]
    [SerializeField] private Transform visualContainer;
    [SerializeField] private GameObject CharacterCenter;
    [SerializeField] private List<Rigidbody> ragdollList;

    protected bool isStopping = false;
    protected bool isDied = false;
    protected bool isReborned = true;
    private Vector3 pos;

    public virtual void CharacterInit()
    {

        StopCursorFollowing();
        characterMovemant.CurrentFlySpeed = characterMovemant.MinFlySpeed;
        transform.position = cursor;
        
        isStopping = false;
    }

   

    public Vector3 CharacterNewPos(Vector3 deltaPos)
    {
        pos = new Vector3(deltaPos.x, deltaPos.y, 0) * sensetivity;
        return pos;

    }
    public double GetPosInSpline()
    {
        
        return sample.percent;
    }
   
    public void SetSplineSample(SplineSample splineSample)
    {
        sample = splineSample;
    }
    public SplineSample GetSplineSample()
    {
        return sample;
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
    public LaneRunner GetLaneRunner()
    {
        return laneRunner;
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

    public void Reborn()
    {
        if (!isReborned)
        {
            animator.enabled = true;
            ChangeRagdollKinematicState(true);
            laneRunner.SetPercent(GetPosInSpline() - rebornPosition);
            laneRunner.follow = true;
            StartForceRoutine();
            isReborned = true;
            isDied = false;
        }
    }

    public void Die()
    {
        if (!isDied)
        {
            StopForceRoutine();
            characterMovemant.CurrentFlySpeed = characterMovemant.MinFlySpeed;
            ChangeRagdollKinematicState(false);
            laneRunner.follow = false;
            animator.enabled = false;
            CharacterCenter.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10);
            isDied = true;
            isReborned = false;
        }
    }

    Coroutine ForceRoutineC;
    public IEnumerator ForceRoutine()
    {

        float t = 0.0f;
        float startTime = Time.fixedTime;
        float minSpeed = characterMovemant.MinFlySpeed;

        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 3f;

            characterMovemant.CurrentFlySpeed = Mathf.Lerp(minSpeed,characterMovemant.MaxFlySpeed,t);

            yield return new WaitForEndOfFrame();
        }


    }
    public void StartForceRoutine()
    {
       
        if (ForceRoutineC != null) StopCoroutine(ForceRoutineC);
        ForceRoutineC = StartCoroutine(ForceRoutine());
    }

    public void StopForceRoutine()
    {
        if (ForceRoutineC != null) StopCoroutine(ForceRoutineC);
    }
    private void ChangeRagdollKinematicState(bool isKinematic)
    {
        for (int i = 0; i < ragdollList.Count; i++)
        {
            ragdollList[i].isKinematic = isKinematic;
        }
    }

    public void StartCharacterStoppingRoutin()
    {
        laneRunner.follow = false;

        characterMovemant.StartCharacterStoppingRoutin();
    }
     public void StopCharacterStoppingRoutin()
    {
        characterMovemant.StopCharacterStoppingRoutin();
    }

    public void StartCursorFollowing()
    {
        laneRunner.follow = true;
        characterMovemant.StartCursorFollowing();
    }
    public void StopCursorFollowing()
    {
        characterMovemant.StopCursorFollowing();
    }

    //Coroutine BoostRoutineC;
    //private IEnumerator BoostRoutine()
    //{
        
    //    float t = 0.0f;
    //    float startTime = Time.fixedTime;

    //    while (t < 1)
    //    {
    //        t = (Time.fixedTime - startTime) / 0.5f;
    //        characterMovemant.SetCurrentSpeed(Mathf.Lerp(characterMovemant.GetCurrentSpeed(), characterMovemant.GetDefaultSpeed()+(characterMovemant.GetDefaultSpeed()/2), t));
    //           // characterMovemant.GetSpeed() + (10 / characterMovemant.GetSpeed()), t));
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return new WaitForSeconds(1);
    //    t = 0.0f;
    //    startTime = Time.fixedTime;
    //    while (t < 1)
    //    {
    //        t = (Time.fixedTime - startTime) / 1.5f;
    //        characterMovemant.SetCurrentSpeed(Mathf.Lerp(characterMovemant.GetCurrentSpeed(), characterMovemant.GetDefaultSpeed(), t));
    //        yield return new WaitForEndOfFrame();
    //    }
    //}


    //public void StartBoostRoutine()
    //{
    //    if (BoostRoutineC != null) StopCoroutine(BoostRoutineC);
    //    BoostRoutineC = StartCoroutine(BoostRoutine());

    //}

    //public void StopBoostRoutine()
    //{
    //    if (BoostRoutineC != null) StopCoroutine(BoostRoutineC);
    //}
}
