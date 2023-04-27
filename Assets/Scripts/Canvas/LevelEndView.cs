using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndView : MonoBehaviour
{
    public static LevelEndView instance;

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
