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
        CanvasSetActivs();
        RoadSpawning.instance.RoadSpawningInit();
        CheckPointSpawning.instance.CheckPointsSpawningInit();
        EnemySpawning.instance.EnemySpawningInit();
        GameView.instance.GameViewCanvasInit();
        LevelEndCanvas.instance.LevelEndCanvasInit();
        DebugCanvas.instance.DebugInit();
        PlayerControler.instance.CharacterInit();
        isGameInited = true;
    }

    public void GameWin()
    {
        LevelManager.instance.levelWin();
        PlayerControler.instance.OnGameWin();
        GameView.instance.GetGameObject().SetActive(false);
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
