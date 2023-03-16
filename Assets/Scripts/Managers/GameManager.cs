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

    public void GameDefaultConfigs()
    {
       
    }
    public void GameInit()
    {
       
        isGameInited = false;
        LevelManager.instance.InitLevel();
        PlayerControler.instance.CharacterInit();
     
        GameView.instance.SetActiveGameStartCanvas(true);
        
        CanvasSetActivs();
        EnemyManager.instance.EnemyInit();

        DebugCanvas.instance.DebugInit();
        LevelEndCanvas.instance.LevelEndCanvasInit();
        isGameInited = true;
    }
    public void GameStart()
    {
       
        PlayerControler.instance.CharacterInit();
        LevelEndCanvas.instance.LevelEndCanvasInit();
        PlayerControler.instance.StartCursorFollowing();
        EnemyManager.instance.EnemyStart();

        GameView.instance.GameViewCanvasInit();
    }

    public void GameWin()
    {
        GameView.instance.GetGameObject().SetActive(false);
        LevelManager.instance.levelWin();
        PlayerControler.instance.OnGameWin();
        LevelEndCanvas.instance.LevelWinCanvasActive();
    }

    public void CanvasSetActivs()
    {
        
        DebugCanvas.instance.gameObject.SetActive(true);
        GameView.instance.gameObject.SetActive(true);
        LevelEndCanvas.instance.gameObject.SetActive(true);
    }

    public void GameOver()
    {

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

   
}
