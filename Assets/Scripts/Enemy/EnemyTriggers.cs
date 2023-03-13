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
            gameObject.GetComponent<EnemyController>().StopGettingCursor();
            gameObject.GetComponent<CharacterController>().StartCharacterStoppingRoutin();
        }
    }
}
