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

    [SerializeField] private Button LaunchBtn;
    [SerializeField] private TMP_Text rocketCountTxt;


    private void Awake()
    {
        instance = this;
    }

    public void GameViewInit()
    {
        SetActive(true);
        sliderTxt.text = "Level " + DataManager.instance.GetLevelNumber();
        SetRocketCount(LevelManager.instance.GetRocketCount());
        SetPlayerCurrentPos(0);
        LaunchBtnSetActive(false);
    }
    
    public void OnLaunch()
    {
        Launcher.instance.OnLaunch();
        LaunchBtnSetActive(false);
    }
 
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetRocketCount(int count)
    {
        rocketCountTxt.text = "X" + count;
    }

    public void LaunchBtnSetActive(bool value)
    {
        LaunchBtn.gameObject.SetActive(value);
    }
    
    public void SetDistance(float value)
    {
        
        scoreUpdateSlider.maxValue = value;
    }

    public void SetPlayerStartPos(float value)
    {
        scoreUpdateSlider.minValue = value; 
    }

    public void SetPlayerCurrentPos(float value)
    {
        scoreUpdateSlider.value = value;
    }
}
