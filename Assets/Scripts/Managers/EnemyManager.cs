using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private GameObject enemy;

    [SerializeField] private List<CharacterController> enemyList = new List<CharacterController>();
    private void Awake()
    {
        instance = this;
    }

    public void EnemyInit()
    {
        foreach (var item in enemyList)
        {
            item.CharacterInit();
        }
    }
   
}
