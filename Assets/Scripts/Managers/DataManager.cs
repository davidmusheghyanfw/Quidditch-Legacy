using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;


    private void Awake()
    {
        instance = this;        
    }

    
    void Start()
    {
        
    }
    public int GetLevelNumber()
    {
        int currentLevelNumber = 1;

        if (!PlayerPrefs.HasKey("levelNumber"))
        {
            PlayerPrefs.SetInt("levelNumber", currentLevelNumber);
        }

        currentLevelNumber = PlayerPrefs.GetInt("levelNumber", currentLevelNumber);

        return currentLevelNumber;

    }

    public void IncreaseLevelNumber()
    {
        PlayerPrefs.SetInt("levelNumber", GetLevelNumber() + 1);
    }
}
