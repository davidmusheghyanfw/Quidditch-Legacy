using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;
    [SerializeField] private CinemachinePath path;
    [SerializeField] private int rocketCount;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private List<RocketController> rocketControllers;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        RotatingAroundLauncher();
    }

    public void RotatingAroundLauncher()
    {
        CameraController.instance.SwitchCamera(CameraState.Overlay);
        CameraController.instance.SetTrackedDollyCamera(CameraState.Overlay);
        CameraController.instance.SetTrackedDollyPath(CameraState.Overlay,path);
        CameraController.instance.SetAimTarget(CameraState.Overlay, gameObject.transform);
        CameraController.instance.SetFollowTarget(CameraState.Overlay, gameObject.transform);
        CameraController.instance.StartTrackedDollAnimRoutine();

    }

    public void LauncherInGame()
    {
        CameraController.instance.SwitchCamera(CameraState.Launcher);
        CameraController.instance.SetAimTarget(CameraState.Launcher, gameObject.transform);
        CameraController.instance.SetFollowTarget(CameraState.Launcher, gameObject.transform);
    }
    public void LauncherInit()
    {
        rocketControllers = new List<RocketController>();
        CreateRocketPull();
    }
    
    private void CreateRocketPull()
    {
        for (int i = 0; i < rocketCount; i++)
        {
            var go = Instantiate(rocket, Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            rocketControllers.Add(go.GetComponent<RocketController>());
        }
    }

     
    public void OnLaunch()
    {
        CameraController.instance.SwitchCamera(CameraState.Rocket);
        RocketController firstRocketController = rocketControllers[0];
        CameraController.instance.SetFollowTarget(CameraState.Rocket, firstRocketController.transform);
        CameraController.instance.SetAimTarget(CameraState.Rocket, firstRocketController.transform);

        firstRocketController.gameObject.SetActive(true);
        firstRocketController.CharacterInit();
        firstRocketController.StartCursorFollowing();
        firstRocketController.StartForceRoutine();
    }

    public RocketController GetRocketController()
    {
        return rocketControllers[0];
    }
    public void RocketDestroyed()
    {
        destroyParticle.gameObject.transform.position = rocketControllers[0].transform.position;
        destroyParticle.SetActive(true);
        rocketControllers.RemoveAt(0);
        if(rocketControllers.Count<=0)
        {
            GameManager.instance.LevelComplete();
            return;
        }
        this.Timer(1f, () =>
        {
            GameManager.instance.GameStart();
        });
    }


}
