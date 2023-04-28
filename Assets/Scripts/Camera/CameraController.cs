using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera cam;
    private CinemachineTrackedDolly activeTrackedDollyCamera;

    [SerializeField] private float pathAnimSpeed;
    [SerializeField] CinemachineBrain cmBrain;
    [SerializeField] List<CameraProperties> cameraStates = new List<CameraProperties>();



    [System.Serializable]
    class CameraProperties
    {
        public CameraState state;
        public CinemachineVirtualCamera camera;
    }

    Vector3 targetPosition;

    void Awake()
    {
        instance = this;
    }

    public void InitCamera()
    {
        targetPosition = Vector3.zero;
        transform.position = targetPosition;
    }


    public void SetFollowTarget(CameraState cameraState, Transform target)
    {
        GetCamera(cameraState).Follow = target;
    }

    public void SetAimTarget(CameraState cameraState, Transform target)
    {
        GetCamera(cameraState).LookAt = target;
    }

    public void SetDistanceFromObject(CameraState cameraState, float distance)
    {
        GetCamera(cameraState).GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = distance;
    }

    public void SwitchCamera(CameraState state)
    {
        for (int i = 0; i < cameraStates.Count; i++)
        {
            cameraStates[i].camera.Priority = 0;

            if (cameraStates[i].state == state)
            {
                cameraStates[i].camera.Priority = 10;
                continue;
            }
        }
    }

    public void SetTrackedDollyPath(CameraState cameraState, CinemachinePath path)
    {
        if (!GetCamera(cameraState).GetCinemachineComponent<CinemachineTrackedDolly>()) return;

        GetCamera(cameraState).GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = path;
    }

    public void SetTrackedDollyCamera(CameraState cameraState)
    {
        if (!GetCamera(cameraState).GetCinemachineComponent<CinemachineTrackedDolly>()) return;

        activeTrackedDollyCamera = GetCamera(cameraState).GetCinemachineComponent<CinemachineTrackedDolly>();
    }
    public void SetUpdateMethod(CinemachineBrain.UpdateMethod updateMethod)
    {
        cmBrain.m_UpdateMethod = updateMethod;
    }

    public CinemachineVirtualCamera GetCamera(CameraState state)
    {
        for (int i = 0; i < cameraStates.Count; i++)
        {
            if (cameraStates[i].state == state)
            {
                return cameraStates[i].camera;
            }
        }

        Debug.LogError("Such Camera State Doesn't Exist!");
        return null;
    }

    public void SetYawDamping(CameraState cameraState, float yaw)
    {
        GetCamera(cameraState).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_YawDamping = yaw;
    }

    Coroutine TrackedDollAnimRoutineC;
    IEnumerator TrackedDollAnimRoutine()
    {
        float t = 0;
        float startTime = Time.fixedTime;

        while(t < 1)
        {
            t = (Time.fixedTime - startTime) / pathAnimSpeed;
            activeTrackedDollyCamera.m_PathPosition = Mathf.Lerp(0,1,t);
            yield return new WaitForEndOfFrame();
        }
        StartTrackedDollAnimRoutine();
    }

    public void StartTrackedDollAnimRoutine()
    {
        if (TrackedDollAnimRoutineC != null) StopCoroutine(TrackedDollAnimRoutineC);
        TrackedDollAnimRoutineC = StartCoroutine(TrackedDollAnimRoutine());

    }

    public void StopTrackedDollAnimRoutine()
    {
        if (TrackedDollAnimRoutineC != null) StopCoroutine(TrackedDollAnimRoutineC);

    }
}


public enum CameraState { Rocket, Overlay, Launcher, Enemy };


