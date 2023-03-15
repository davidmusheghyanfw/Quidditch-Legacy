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

        if (other.gameObject.tag == "Coin")
        {
            DataManager.instance.SetCoinsAmount(DataManager.instance.GetCoinsAmount()+1);
            Destroy(other.gameObject);
        }
    }
}
