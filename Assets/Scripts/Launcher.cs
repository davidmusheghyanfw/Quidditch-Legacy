using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;
    [SerializeField] private CinemachinePath path;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private List<RocketController> rocketControllers;
    [SerializeField] private List<Transform> launchSlots;

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
        int rocketCount = LevelManager.instance.GetRocketCount();
        for (int i = 0; i < rocketCount; i++)
        {
            var go = Instantiate(rocket, Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            rocketControllers.Add(go.GetComponent<RocketController>());
        }
    }

     
    public void OnLaunch()
    {
        Transform launchSlot = launchSlots[Random.Range(0, launchSlots.Count)];
        RocketController firstRocketController = rocketControllers[0];
        firstRocketController.transform.position = launchSlot.position;
        
        firstRocketController.transform.rotation = launchSlot.rotation;
        firstRocketController.gameObject.SetActive(true);
        PointerManager.Instance.SetPlayerTransform(firstRocketController.transform);
        PointerManager.Instance.Show();
        firstRocketController.CharacterInit();
        firstRocketController.StartCursorFollowing();
        firstRocketController.StartForceRoutine();
        CameraController.instance.SwitchCamera(CameraState.Rocket);
        CameraController.instance.SetAimTarget(CameraState.Launcher, null);
        CameraController.instance.SetFollowTarget(CameraState.Rocket, firstRocketController.transform);
        CameraController.instance.SetAimTarget(CameraState.Rocket, firstRocketController.transform);
        
        //GameView.instance.SetPlayerStartPos((float)firstRocketController.GetPosInSpline());
    }

    public RocketController GetRocketController()
    {
        return rocketControllers[0];
    }

    public void DestroyAllRockets()
    {
        
        for (int i = 0; i < rocketControllers.Count; i++)
        {
           
            Destroy(rocketControllers[0].gameObject);
            rocketControllers.RemoveAt(0);
        }
    }
    public void RocketDestroyed()
    {
        destroyParticle.gameObject.transform.position = rocketControllers[0].transform.position;
        destroyParticle.SetActive(true);
        rocketControllers.RemoveAt(0);
        PointerManager.Instance.Hide();
        GameView.instance.SetPlayerCurrentPos(0f);
        if (rocketControllers.Count<=0)
        {
            GameManager.instance.LevelComplete();
            return;
        }
        GameView.instance.SetRocketCount(rocketControllers.Count);
        this.Timer(1f, () =>
        {
            GameManager.instance.InGame();
        });
    }


}
