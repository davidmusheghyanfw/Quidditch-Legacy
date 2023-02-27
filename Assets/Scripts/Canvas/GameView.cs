using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView instance;

    [SerializeField] private Slider scoreUpdateSlider;
    [SerializeField] private float levelCompleteScore;

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
}
