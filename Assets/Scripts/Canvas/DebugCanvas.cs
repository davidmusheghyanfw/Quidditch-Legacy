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
        slider.value = PlayerControler.instance.GetSensetivity();
        sliderTxt.text = PlayerControler.instance.GetSensetivity().ToString();
    }

    public void Exit()
    {
        debugMenu.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.instance.GameInit();
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
        PlayerControler.instance.SetSensetivity(slider.value);
        SetSensetivityTxt(slider.value);
    }
}
