using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameInited = false;
    public bool isGameStopped = false;


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        GameInit();
    }

   
    public void GameInit()
    {
        isGameInited = false;
        LevelManager.instance.InitLevel();
        GameView.instance.GameViewInit();
        LevelEndView.instance.gameObject.SetActive(false);
        DebugCanvas.instance.DebugInit();
        this.Timer(1f, () => 
        { 
            Launcher.instance.LauncherInit();
            MenuView.instance.gameObject.SetActive(true);
        });
        

    }
    public void GameStart()
    {
        CameraController.instance.StopTrackedDollAnimRoutine();
        Launcher.instance.LauncherInGame();
        GameView.instance.LaunchBtnSetActive(true);
    }

    public void LevelComplete()
    {

        LevelEndView.instance.gameObject.SetActive(true);
        LevelManager.instance.levelWin();
       // Launcher.instance.GetRocketController().OnGameWin();
     
    }
   
    public void GameStopped()
    {
        //isGameStopped = true;
        Launcher.instance.GetRocketController().GameStopped();

    }

    public void GameResume()
    {
        //isGameStopped = false;
        Launcher.instance.GetRocketController().GameResume();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Break();
        }
        //else if (Input.GetKeyDown("r"))
        //{
        //    RestartGame();
        //}
        //else if (Input.GetKeyDown("w"))
        //{
        //    LevelCompleted();
        //}
    }
#endif

}
