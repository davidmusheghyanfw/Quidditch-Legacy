using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager instance;
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private EnvironmentDefinition environmentDefinition;
    [SerializeField] private ForeverLevel foreverLevel;
    private void Awake()
    {
        instance = this; 
    }


    public LevelGenerator GetLevelGenerator() 
    {
        return levelGenerator;
    }

}
