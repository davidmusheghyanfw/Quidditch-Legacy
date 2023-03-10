using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private List<SplineDefinition> levelDefinitionsList;
    [SerializeField] private float roadPointOffset;

    private float levelCompleteDistance;

    private int level = 1;
    private int levelDefinition = 0;


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
       
        RoadGenerator.instance.RoadGeneratorInit();

        CheckPointSpawning.instance.CheckPointsSpawningInit();
        CalculateLevelDistance();
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

    private void CalculateLevelDistance()
    {

        //levelCompleteDistance = DataManager.instance.GetLevelNumber() * (RoadSpawning.instance.GetRoad().GetComponent<RoadInfo>().GetRoadScale().z * 2);
        levelCompleteDistance = (float)RoadGenerator.instance.GetDistance();

        GameView.instance.SetFinishDistance(levelCompleteDistance);//+ RoadSpawning.instance.GetRoad().GetComponent<RoadInfo>().GetRoadScale().z/2);
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
}
