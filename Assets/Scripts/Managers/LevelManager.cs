using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private float firstLevelDistance;

    private float levelCompleteDistance;

    private int level = 1;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitLevel()
    {
        CalculateLevelDistance();
    }

    public int GetLevel()
    {
        return level;
    }
    
    public void levelWin()
    {
        level ++;
    }

    private void CalculateLevelDistance()
    {
          
        levelCompleteDistance = (level * RoadSpawning.instance.GetRoadLength())+firstLevelDistance;
        GameView.instance.SetFinishDistance(levelCompleteDistance);
    }

    public float GetLevelDistance()
    {
        return levelCompleteDistance;
    }
}
