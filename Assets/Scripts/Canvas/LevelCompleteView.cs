using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteView : MonoBehaviour
{
    public static LevelCompleteView instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void OnNextLevel()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameInit();
    }
}
