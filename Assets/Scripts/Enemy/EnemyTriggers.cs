using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            //gameObject.GetComponent<EnemyController>().StartAddForceRoutine();
        }
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            gameObject.GetComponent<EnemyController>().StopGettingCursor();
            gameObject.GetComponent<EnemyController>().StopSpeedControllRountine();
            gameObject.GetComponent<CharacterController>().StartCharacterStoppingRoutin();
            
        }
    }
}
