using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public EnemySpawning spawner;
    [SerializeField] private EnemyCurveDefinition curveDefinition;

    [SerializeField] private List<Destroyable> enemyList = new List<Destroyable>();
    private void Awake()
    {
        instance = this;
    }

    public void AddToList(Destroyable destroyable)
    {
        enemyList.Add(destroyable);
    }

    public int GetEnemyCount()
    {
        return enemyList.Count-1;
    }
   public void Enemyinit()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            PointerManager.Instance.AddToList(enemyList[i]);
        }
    }

    //public void EnemyInit()
    //{
    //    enemyList = spawner.EnemySpawningInit();

    //    foreach (EnemyController character in enemyList)
    //    {
    //        character.CharacterInit();
    //        character.SetCurve(curveDefinition.curveList[Random.Range(0,curveDefinition.curveList.Count)]);
    //    }
    //}

    //public void SpawnEnemies()
    //{

    //}

    //public void EnemyStart()
    //{
        
    //    foreach (EnemyController character in enemyList)
    //    {
           
    //        //character.SetPosInSpline(character.GetSpawnPosPersent());
    //        //character.SetNextCheckPointIndex(CheckPointSpawning.instance.CalculateEnemyNearestCheckPoint(character.GetSpawnPosPersent()));
            
    //        character.StartCursorFollowing();
    //        character.StartForceRoutine();
    //        character.StartGettingCursor();
            
    //    }
    //}




}
