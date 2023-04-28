using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private List<SplineDefinition> levelDefinitionsList;
    [SerializeField] private int rocketCount;

    [SerializeField] private float roadPointOffset;


    private int finishPlace = 0;
    private float levelEndPosPercent;
    private int level = 1;
    private int levelDefinition = 0;


    private void Awake()
    {
        instance = this;
    }

    public void InitLevel()
    {

        RoadGenerator.instance.RoadGeneratorInit();

        
        CheckPointSpawning.instance.CheckPointsSpawningInit();
        CoinSpawner.instance.CoinSpawnerInit();
    }

    public int GetLevel()
    {
        return level;
    }
    
    public void levelWin()
    {
        levelDefinition++; ;
        DataManager.instance.IncreaseLevelNumber();
    }

    public float GetRoadPointOffset()
    {
        return roadPointOffset;
    }

    public SplineDefinition GetLevelDefinition()
    {
        
        if (levelDefinition > levelDefinitionsList.Count-1) levelDefinition = 0;
        
        return levelDefinitionsList[levelDefinition];
    }

   
    public void Finished()
    {
        finishPlace++;
    }

    public int GetFinishPlace()
    {
       
        return finishPlace;
    }

    public int GetRocketCount()
    {
        return rocketCount;
    }

    public void SetLevelEndPos(float value)
    {
 
        levelEndPosPercent = value;
    }

    public float GetLevelEndPosPercent()
    {
        return levelEndPosPercent;
    }
}
