using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView instance;

    [SerializeField] private Slider scoreUpdateSlider;
    [SerializeField] private Slider sensetivitySlider;
    [SerializeField] private Slider flySpeedSlider;
    [SerializeField] private TMP_Text sensetivityText;
    [SerializeField] private TMP_Text flySpeedText;

    [SerializeField] private float levelCompleteScore;

    [SerializeField] private GameObject GameStartCanvas;
    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
    }

    public void GameViewCanvasInit()
    {
        scoreUpdateSlider.value = 0;
        scoreUpdateSlider.maxValue = levelCompleteScore;
        sensetivitySlider.value = PlayerControler.instance.GetSensetivity();
        sensetivityText.text = PlayerControler.instance.GetSensetivity().ToString();
        flySpeedSlider.value = PlayerControler.instance.GetCharacterMovemant().GetCurrentSpeed();
        flySpeedText.text = PlayerControler.instance.GetCharacterMovemant().GetCurrentSpeed().ToString();
        SetActiveGameStartCanvas(false);

    }
    public void SetFinishDistance(float distance)
    {
        levelCompleteScore = distance;
    }
    public void UpdateScore()
    {
        scoreUpdateSlider.value = PlayerControler.instance.GetCharacter().transform.position.z;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public void SetActiveGameStartCanvas(bool State)
    {
        GameStartCanvas.SetActive(State);
    }

    public void OnStartGame()
    {
        GameManager.instance.GameStart();
        SetActiveGameStartCanvas(false);
    }

    public void OnSensetivityChanged()
    {
        sensetivityText.text = sensetivitySlider.value.ToString();
        PlayerControler.instance.SetSensetivity(sensetivitySlider.value);
    }
    public void OnFlyaSpeedChanged()
    {
        flySpeedText.text = flySpeedSlider.value.ToString();
        PlayerControler.instance.GetCharacterMovemant().SetCurrentSpeed(flySpeedSlider.value);
    }
}
