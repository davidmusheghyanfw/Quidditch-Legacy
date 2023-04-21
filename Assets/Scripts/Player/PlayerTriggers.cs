using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            //gameObject.GetComponent<PlayerControler>().StartAddForceRoutine();
            //CameraController.instance.StartForceEffectRoutine();
        }
        if (other.gameObject.tag == "Finish")
        {
            LevelManager.instance.Finished();
            PlayerControler.instance.FinishPlace = LevelManager.instance.GetFinishPlace();
            LevelManager.instance.GetReward(PlayerControler.instance.FinishPlace);
            GameManager.instance.LevelComplete();
        }

        if (other.gameObject.tag == "Enemy")
        {

            other.gameObject.GetComponent<EnemyController>().OnPlayerTriggered(gameObject.transform.position);
        }

        if (other.gameObject.tag == "Coin")
        {
            DataManager.instance.SetCoinsAmount(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Environment")
        {
            PlayerControler.instance.Die();
            this.Timer(1f, () =>
            {
                PlayerControler.instance.Reborn();
            });
        }
    }
}
