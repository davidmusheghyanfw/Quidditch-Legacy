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
        MenuView.instance.gameObject.SetActive(true);
        isGameInited = false;
        LevelManager.instance.InitLevel();
        GameView.instance.GameViewInit();
        LevelCompleteView.instance.gameObject.SetActive(false);
        DebugCanvas.instance.DebugInit();
        
        this.Timer(1f, () =>
        {
            PlayerControler.instance.CharacterInit();
            EnemyManager.instance.EnemyInit();
           

            isGameInited = true;
        });
    }
    public void GameStart()
    {
        GameView.instance.SetActive(true);
        PlayerControler.instance.CharacterInit();
        PlayerControler.instance.StartCursorFollowing();
        PlayerControler.instance.StartForceRoutine();
        EnemyManager.instance.EnemyStart();

    }

    public void LevelComplete()
    {
        GameView.instance.SetActive(false);
        LevelCompleteView.instance.gameObject.SetActive(true);
        LevelManager.instance.levelWin();
        PlayerControler.instance.OnGameWin();
     
    }
   
    public void GameStopped()
    {
        //isGameStopped = true;
        PlayerControler.instance.GameStopped();

    }

    public void GameResume()
    {
        //isGameStopped = false;
        PlayerControler.instance.GameResume();
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
