using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
  
    [SerializeField] private EnemyController enemy;
    [SerializeField] private Transform parent;
    [SerializeField] private int enemyCount;
    [SerializeField] private float offset;
    [SerializeField] private float rangeOffset;

    private SplineSample sample;
    private float tmpOffset;

    private List<EnemyController> enemies = new List<EnemyController>();

    private Vector3 spawnPosOnRoad;
    private Vector2 spawnPosOnScreen;
    private void Awake()
    {
        instance = this;
    }

    public List<EnemyController> EnemySpawningInit()
    {
        DestroyAll();
        enemies.Clear();

        SpawnEnemies();

        return enemies;
        //characterController = enemy.GetComponent<EnemyController>();
    }

    //Coroutine EnemySpawningRoutineC;
    public void SpawnEnemies()
    {
        var start = EnvironmentManager.instance.GetStartSegment();

        int enemyCount = this.enemyCount; //Random.Range(0, enemyCountInSegment);
        RoadGenerator.instance.GetLevelGenerator().Project(Launcher.instance.transform.position, ref sample);
        for (int j = 0; j < enemyCount; j++)
        {
            spawnPosOnScreen.Set(Random.Range(enemy.HorizontalBorderRange.x, enemy.HorizontalBorderRange.y),
              Random.Range(enemy.VerticalBorderRange.x, enemy.VerticalBorderRange.y));


            var currentEnemy = Instantiate(enemy, spawnPosOnRoad + sample.position, transform.rotation, parent);
            enemies.Add(currentEnemy);
            //enemies[enemies.Count - 1].SetPosInSpline((tmpRange+tmpOffset) / RoadGenerator.instance.GetDistance());
            enemies[enemies.Count - 1].SetCursor(spawnPosOnScreen);
            enemies[enemies.Count - 1].SetSpawnPos(spawnPosOnScreen);
            enemies[enemies.Count - 1].SetSplineSample(sample);
            enemies[enemies.Count - 1].GetLaneRunner().SetPercent(sample.percent);
            //3enemies[enemies.Count - 1].SetSpawnPosPersent((float)((tmpRange + tmpOffset) / RoadGenerator.instance.GetDistance()));
        }
    }

    private void DestroyAll()
    {
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
    }
}
