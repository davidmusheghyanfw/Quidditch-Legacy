using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
  
    [SerializeField] private EnemyController enemy;
    [SerializeField] private Transform parent;
    [SerializeField,Range(1,10)] private int SegmentCount;
    [SerializeField] private int enemyCountInSegment;
    [SerializeField] private float offset;
    [SerializeField] private float rangeOffset;

    private float tmpOffset;

    private List<EnemyController> enemies = new List<EnemyController>();

    private Vector3 spawnPosOnRoad;
    private Vector3 spawnPosOnScreen;
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
        tmpOffset = 0;
        for (int i = 0; i < SegmentCount; i++)
        {
            int enemyCount = Random.Range(0, enemyCountInSegment);
            tmpOffset += offset;
            for (int j = 0; j < enemyCount; j++)
            {
               
                if (tmpOffset >= RoadGenerator.instance.GetDistance())
                {
                        break;
                }
                float tmpRange = Random.Range(-rangeOffset, rangeOffset);
                spawnPosOnRoad = GetNearestPointOnRoad(tmpRange+tmpOffset);

                spawnPosOnScreen.Set(Random.Range(enemy.HorizontalBorderMin(), enemy.HorizontalBorderMax()),
                  Random.Range(enemy.VerticalBorderMin(), enemy.VerticalBorderMax()), spawnPosOnRoad.z);

                var currentEnemy = Instantiate(enemy, spawnPosOnRoad + spawnPosOnScreen, transform.rotation, parent);
                enemies.Add(currentEnemy);
                enemies[enemies.Count - 1].SetPosInSpline((tmpRange+tmpOffset) / RoadGenerator.instance.GetDistance());
                enemies[enemies.Count - 1].SetCursor(spawnPosOnScreen);
                enemies[enemies.Count - 1].SetSpawnPosPersent((float)((tmpRange + tmpOffset) / RoadGenerator.instance.GetDistance()));
            }

        }
    }

    private Vector3 GetNearestPointOnRoad(float offset)
    {

        return RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetDistance()).position;
    }


    private void DestroyAll()
    {
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
    }
}
