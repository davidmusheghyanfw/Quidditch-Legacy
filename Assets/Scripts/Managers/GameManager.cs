using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   

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
        PlayerMovemant.instance.PlayerInit();
        CanvasSetActivs();
        LevelManager.instance.InitLevel();
        RoadSpawning.instance.RoadSpawningInit();
        CheckPointSpawning.instance.CheckPointsSpawningInit();
        GameView.instance.GameViewCanvasInit();
        LevelEndCanvas.instance.LevelEndCanvasInit();
        DebugCanvas.instance.DebugInit();
    }

    public void GameWin()
    {
        LevelManager.instance.levelWin();
        PlayerMovemant.instance.OnGameWin();
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
        PlayerMovemant.instance.GameStopped();
    }

    public void GameResume()
    {
        PlayerMovemant.instance.GameResume();
    }

   
}
