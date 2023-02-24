using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndCanvas : MonoBehaviour
{
    public static LevelEndCanvas instance;

    [SerializeField] public GameObject LevelWin;
    [SerializeField] public GameObject LevelLoose;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public void LevelEndCanvasInit()
    {
        LevelWin.SetActive(false);
        //LevelLoose.SetActive(false);
    }
    public void LevelWinCanvasActive()
    {
        LevelWin.SetActive(true);
    }

    public void OnNextLevel()
    {
        GameManager.instance.GameInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
