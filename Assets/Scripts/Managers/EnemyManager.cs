using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public EnemySpawning spawner;

    [SerializeField] private List<EnemyController> enemyList = new List<EnemyController>();
    private void Awake()
    {
        instance = this;
    }

    public void EnemyInit()
    {
        enemyList = spawner.EnemySpawningInit();

        foreach (EnemyController character in enemyList)
        {
            character.CharacterInit();
        }
    }

    public void SpawnEnemies()
    {

    }

    public void EnemyStart()
    {
        
        foreach (EnemyController character in enemyList)
        {
           
            character.SetPosInSpline(character.GetSpawnPosPersent());
            character.SetNextCheckPointIndex(CheckPointSpawning.instance.CalculateEnemyNearestCheckPoint(character.GetSpawnPosPersent()));
            
            character.StartCursorFollowing();
            character.StartGettingCursor();
            character.StartSpeedControllRountine();
        }
    }




}
