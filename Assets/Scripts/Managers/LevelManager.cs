using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private List<SplineDefinition> levelDefinitionsList;
    [SerializeField] private List<CoinDefinition> coinDefinitionsList;
    [SerializeField] private List<int> placeReward;
    [SerializeField] private int defaultReward;
    [SerializeField] private float roadPointOffset;


    private int finishPlace = 0;
    private float levelCompleteDistance;
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

    public float GetLevelDistance()
    {
        return levelCompleteDistance;
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

    public int GetCoinDefinitionsCount()
    { 
        return coinDefinitionsList.Count;
    }
    public CoinDefinition GetCoinDefinition(int index)
    {
        return coinDefinitionsList[index];
    }

    public void Finished()
    {
        finishPlace++;
    }

    public int GetFinishPlace()
    {
       
        return finishPlace;
    }

    public void GetReward(int finishedPlace)
    {
        if(placeReward.Count < finishPlace)
        {
            DataManager.instance.SetCoinsAmount(defaultReward);
        }
        else
        {
            DataManager.instance.SetCoinsAmount(placeReward[finishedPlace]);
        }
    }
}
