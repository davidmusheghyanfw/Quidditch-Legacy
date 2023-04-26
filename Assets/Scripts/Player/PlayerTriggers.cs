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
            Destroy(Launcher.instance.GetRocketController().gameObject);
            // LevelManager.instance.Finished();
            // Launcher.instance.GetRocketController().FinishPlace = LevelManager.instance.GetFinishPlace();
            // LevelManager.instance.GetReward(Launcher.instance.GetRocketController().FinishPlace);
            // GameManager.instance.LevelComplete();

        }

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Environment>().IsDamaged(true);
            Destroy(Launcher.instance.GetRocketController().gameObject);
        }

        if (other.gameObject.tag == "Coin")
        {
            DataManager.instance.SetCoinsAmount(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Environment")
        {
            //Launcher.instance.GetRocketController().Die();
            //this.Timer(1f, () =>
            //{
            //    Launcher.instance.GetRocketController().Reborn();
            //});
            Destroy(Launcher.instance.GetRocketController().gameObject);
        }
    }
}
