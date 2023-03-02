using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    public static DebugCanvas instance;
    [SerializeField] private GameObject debugMenu;

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
}
