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
        GameView.instance.gameObject.SetActive(true);
        GameView.instance.GameViewCanvasInit();
        CheckPointSpawning.instance.CheckPointsSpawningInit();
        RoadSpawning.instance.RoadSpawningInit();
        LevelEndCanvas.instance.LevelEndCanvasInit();
    }

    public void GameWin()
    {
        PlayerMovemant.instance.OnGameWin();
        GameView.instance.GetGameObject().SetActive(false);
        LevelEndCanvas.instance.LevelWinCanvasActive();
    }

    public void GameOver()
    {

    }

    public void GameStopped()
    {

    }
}
