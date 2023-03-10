using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera main;

    [SerializeField] private float smoothness;
    [SerializeField] private float maxFiledOfView;
    [SerializeField] private float minFiledOfView;

    private void Awake()
    {
        instance = this;
    }

    Vector3 camVelocity;
    public void PlayerPosUpdate(Vector3 playerPos, Transform playerRot)
    {
        //transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref camVelocity, smoothness);
        transform.position = Vector3.Lerp(transform.position, playerPos, Time.fixedDeltaTime * smoothness);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, playerRot.localRotation, Time.deltaTime * smoothness);
    }

    Coroutine ForceEffectRoutineC;
    private IEnumerator ForceEffectRoutine()
    {
      
        float t = 0.0f;
        float startTime = Time.fixedTime;

        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 0.5f;
            main.fieldOfView = Mathf.Lerp(main.fieldOfView, maxFiledOfView, t);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        t = 0.0f;
        startTime = Time.fixedTime;
        while (t < 1)
        {
            t = (Time.fixedTime - startTime) / 1.5f;
            main.fieldOfView = Mathf.Lerp(main.fieldOfView, minFiledOfView, t);
            yield return new WaitForEndOfFrame();
        }
    }


    public void StartForceEffectRoutine()
    {
        if (ForceEffectRoutineC != null) StopCoroutine(ForceEffectRoutineC);
        ForceEffectRoutineC = StartCoroutine(ForceEffectRoutine());

    }

    public void StopForceEffectRoutine()
    {
        if (ForceEffectRoutineC != null) StopCoroutine(ForceEffectRoutineC);
    }
}
