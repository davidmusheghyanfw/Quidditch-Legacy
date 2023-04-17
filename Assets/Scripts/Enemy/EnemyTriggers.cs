using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            //gameObject.GetComponent<EnemyController>().StartAddForceRoutine();
        }
        else if (other.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            enemyController.StopGettingCursor();
            enemyController.StopSpeedControllRountine();
            enemyController.StartCharacterStoppingRoutin();

        }
        else if (other.gameObject.CompareTag("Environment"))
        {
            enemyController.Die();
            this.Timer(1f,() => 
            {
                enemyController.Reborn();
            });
            //Debug.Log("Ara kpav!!!", transform);
        }
      
    }
}
