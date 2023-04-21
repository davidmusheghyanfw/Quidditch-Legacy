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

    public void OnGameStart()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameStart();
    }
}
