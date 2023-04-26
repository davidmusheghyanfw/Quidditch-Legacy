using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;
    [SerializeField] private int rocketCount;
    [SerializeField] private GameObject rocket;
    [SerializeField] private List<RocketController> rocketControllers = new List<RocketController>();

    private void Awake()
    {
        instance = this;
    }

    public void LauncherInit()
    {
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

     
    public void OnLunch()
    {
        rocketControllers[0].gameObject.SetActive(true);
        rocketControllers[0].CharacterInit();
        rocketControllers[0].StartCursorFollowing();
        rocketControllers[0].StartForceRoutine();
    }

    public RocketController GetRocketController()
    {
        return rocketControllers[0];
    }

}
