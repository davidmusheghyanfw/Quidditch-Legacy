using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView instance;

    [SerializeField] private Slider scoreUpdateSlider;
    [SerializeField] private TMP_Text sliderTxt;

    [SerializeField] private float levelCompleteScore;

    [SerializeField] private GameObject GameStartCanvas;


    private void Awake()
    {
        instance = this;
    }

    public void GameViewInit()
    {

    }
    
    public void SetLevelSettings()
    {
        scoreUpdateSlider.maxValue = LevelManager.instance.GetLevelDistance();
        sliderTxt.text = "Level " + LevelManager.instance.GetLevel();
    }

    public void SetActive(bool state)
    {
        this.gameObject.SetActive(state);
    }
}
