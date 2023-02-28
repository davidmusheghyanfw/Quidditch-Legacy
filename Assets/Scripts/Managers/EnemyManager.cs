using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField] private GameObject enemy;

    [SerializeField] private List<Transform> enemyList = new List<Transform>();
    private void Awake()
    {
        instance = this;
    }

    public void EnemyInit()
    {
        foreach (Transform item in transform)
        {
            
            item.GetComponent<CharacterController>().CharacterInit();
            item.GetComponent<CharacterController>().StartCursorFollowing();

            enemyList.Add(item);
        }
    }
   
}
