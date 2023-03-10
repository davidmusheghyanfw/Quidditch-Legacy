using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            //gameObject.GetComponent<PlayerControler>().StartAddForceRoutine();
            //CameraController.instance.StartForceEffectRoutine();
        }
        if (other.gameObject.tag == "Finish")
        {
            GameManager.instance.GameWin();
        }

        if (other.gameObject.tag == "Enemy")
        {
            
            other.gameObject.GetComponent<EnemyController>().OnPlayerTriggered(gameObject.transform.position);
        }
    }
}
