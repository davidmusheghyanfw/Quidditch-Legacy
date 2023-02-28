using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform parent;
    [SerializeField] private int enemyCount = 5;

    private List<GameObject> enemys = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void EnemySpawningInit()
    {
        DestroyAll();
        enemys.Clear();
        StartCharacterStoppingRoutin();
    }

    Coroutine EnemySpawningRoutineC;
    IEnumerator EnemySpawningRoutine()
    {
        while (true)
        {
            if (enemys.Count < enemyCount)
            {
                var currentEnemy = Instantiate(enemy, Vector3.zero, transform.rotation, parent);
                enemys.Add(currentEnemy);

            }
            else StopCharacterStoppingRoutin();
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartCharacterStoppingRoutin()
    {
        if (EnemySpawningRoutineC != null) StopCoroutine(EnemySpawningRoutineC);
        EnemySpawningRoutineC = StartCoroutine(EnemySpawningRoutine());

    }

    public void StopCharacterStoppingRoutin()
    {
        if (EnemySpawningRoutineC != null) StopCoroutine(EnemySpawningRoutineC);
        EnemyManager.instance.EnemyInit();
    }

    private void DestroyAll()
    {
        foreach (var item in enemys)
        {
            Destroy(item.gameObject);
        }
    }
}
