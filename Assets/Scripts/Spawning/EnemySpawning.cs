using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
  
    [SerializeField] private EnemyController enemy;
    [SerializeField] private Transform parent;
    [SerializeField, Range(2,10)] private int SegmentCount;
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
        tmpOffset = 50;
        //for (int i = 0; i < SegmentCount; i++)
        //{
            int enemyCount = enemyCountInSegment; //Random.Range(0, enemyCountInSegment);
            for (int j = 0; j < enemyCount; j++)
            {
               
                if (tmpOffset >= RoadGenerator.instance.GetDistance())
                {
                        break;
                }
                float tmpRange = Random.Range(-rangeOffset, rangeOffset);
                spawnPosOnRoad = GetNearestPointOnRoad(tmpRange+tmpOffset);

                spawnPosOnScreen.Set(Random.Range(enemy.HorizontalBorderRange.x, enemy.HorizontalBorderRange.y),
                  Random.Range(enemy.VerticalBorderRange.x, enemy.VerticalBorderRange.y), spawnPosOnRoad.z);

                var currentEnemy = Instantiate(enemy, spawnPosOnRoad + spawnPosOnScreen, transform.rotation, parent);
                enemies.Add(currentEnemy);
                //enemies[enemies.Count - 1].SetPosInSpline((tmpRange+tmpOffset) / RoadGenerator.instance.GetDistance());
                enemies[enemies.Count - 1].SetCursor(spawnPosOnScreen);
                //3enemies[enemies.Count - 1].SetSpawnPosPersent((float)((tmpRange + tmpOffset) / RoadGenerator.instance.GetDistance()));
            }
            tmpOffset += offset;

        //}
    }

    private Vector3 GetNearestPointOnRoad(float offset)
    {

        return Vector3.zero;//RoadGenerator.instance.GetSplineComputer().Evaluate(offset / RoadGenerator.instance.GetDistance()).position;
    }


    private void DestroyAll()
    {
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
    }
}
