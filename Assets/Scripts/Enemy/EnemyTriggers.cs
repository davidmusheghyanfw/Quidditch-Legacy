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

            enemyController.StopGettingCursor();
            enemyController.StopSpeedControllRountine();
            enemyController.StartCharacterStoppingRoutin();
            LevelManager.instance.Finished();
            enemyController.FinishPlace = LevelManager.instance.GetFinishPlace();
        }
        else if (other.gameObject.CompareTag("Environment"))
        {
    
        }
      
    }
}
