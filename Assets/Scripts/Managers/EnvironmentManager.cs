using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    [SerializeField] private LevelGenerator levelGenerator;
    private void Awake()
    {
        instance = this; 
    }


    public LevelGenerator GetLevelGenerator() 
    {
        return levelGenerator;
    }
}
