using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
    //private CharacterController characterController;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private Transform parent;
    [SerializeField] private int enemyCount = 5;

    private Vector3 spawnPosition;

    private List<EnemyController> enemies = new List<EnemyController>();

    //private void Awake()
    //{
    //    instance = this;
    //}

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
        while (enemies.Count < enemyCount)
        {

            // spawnPosition.Set(Random.Range(characterController.HorizontalBorderMin(), characterController.HorizontalBorderMax()),
            // Random.Range(characterController.VerticalBorderMin(), characterController.VerticalBorderMax()), 0);
            var currentEnemy = Instantiate(enemy, Vector3.zero, transform.rotation, parent);
            enemies.Add(currentEnemy);
        }

        //while (true)
        //{
           
        //    else StopCharacterStoppingRoutin();
        //    yield return new WaitForEndOfFrame();
        //}
    }

    //public void StartCharacterStoppingRoutin()
    //{
    //    if (EnemySpawningRoutineC != null) StopCoroutine(EnemySpawningRoutineC);
    //    EnemySpawningRoutineC = StartCoroutine(EnemySpawningRoutine());

    //}

    //public void StopCharacterStoppingRoutin()
    //{
    //    if (EnemySpawningRoutineC != null) StopCoroutine(EnemySpawningRoutineC);
     
    //}

    private void DestroyAll()
    {
        foreach (var item in enemies)
        {
            Destroy(item.gameObject);
        }
    }
}
