using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public static MenuView instance;

    private void Awake()
    {
        instance = this;
    }

    public void MenuViewInit()
    {
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnGameStart()
    {
        SetActive(false);
        GameManager.instance.GameStart();
    }
}
