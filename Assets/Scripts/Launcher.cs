using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;
    [SerializeField] private int rocketCount;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private List<RocketController> rocketControllers;

    private void Awake()
    {
        instance = this;
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
            var go = Instantiate(rocket, Vector3.zero, Quaternion.identity, this.transform);
            go.SetActive(false);
            rocketControllers.Add(go.GetComponent<RocketController>());
        }
    }

     
    public void OnLaunch()
    {
        CameraController.instance.SetFollower(rocketControllers[0].transform);
        rocketControllers[0].gameObject.SetActive(true);
        rocketControllers[0].CharacterInit();
        rocketControllers[0].StartCursorFollowing();
        rocketControllers[0].StartForceRoutine();
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
        OnLaunch();
        });
    }
}
