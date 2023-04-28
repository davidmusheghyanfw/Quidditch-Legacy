using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public static DebugCanvas instance;
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text sliderTxt;

    private void Awake()
    {
        instance = this;
    }
    public void DebugInit()
    {
        debugMenu.SetActive(false);
    }
    public void OpenDebugMenu()
    {
        debugMenu.SetActive(true);
        slider.value = Launcher.instance.GetRocketController().GetSensetivity();
        sliderTxt.text = Launcher.instance.GetRocketController().GetSensetivity().ToString();
    }

    public void Exit()
    {
        debugMenu.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.instance.GameRestart();
    }

    public void ClearStats()
    {
        DataManager.instance.ClearStats();
        RestartGame();
    }

    public void SetSensetivityTxt(float value)
    {
        sliderTxt.text = value.ToString();
    }

    public void OnSensetivityChange()
    {
        Launcher.instance.GetRocketController().SetSensetivity(slider.value);
        SetSensetivityTxt(slider.value);
    }
}
