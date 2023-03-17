using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
    private CharacterController characterController;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private Transform parent;
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private float offset;

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
        while (enemies.Count < enemyCount)
        {
            tmpOffset += offset;
            if (tmpOffset >= RoadGenerator.instance.GetDistance())
            {
                break;
            }
            spawnPosOnRoad = GetNearestPointOnRoad(tmpOffset);

            spawnPosOnScreen.Set(Random.Range(enemy.HorizontalBorderMin(), enemy.HorizontalBorderMax()),
              Random.Range(enemy.VerticalBorderMin(), enemy.VerticalBorderMax()),spawnPosOnRoad.z);

            var currentEnemy = Instantiate(enemy, spawnPosOnRoad + spawnPosOnScreen, transform.rotation, parent);
            enemies.Add(currentEnemy);
            enemies[enemies.Count - 1].SetCurrentDistancePercent(tmpOffset / RoadGenerator.instance.GetDistance());
            enemies[enemies.Count - 1].SetCursor(spawnPosOnScreen);
            enemies[enemies.Count - 1].SetSpawnPosPersent((float)(tmpOffset / RoadGenerator.instance.GetDistance()));
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
