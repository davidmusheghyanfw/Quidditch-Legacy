using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            //gameObject.GetComponent<PlayerControler>().StartAddForceRoutine();
            //CameraController.instance.StartForceEffectRoutine();
        }
        if (other.gameObject.tag == "Finish")
        {

            // LevelManager.instance.Finished();
            // Launcher.instance.GetRocketController().FinishPlace = LevelManager.instance.GetFinishPlace();
            // LevelManager.instance.GetReward(Launcher.instance.GetRocketController().FinishPlace);
            // GameManager.instance.LevelComplete();

        }

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Destroyable>().IsDamaged(true);
            CameraController.instance.SetFollowTarget(CameraState.Rocket, other.gameObject.transform);
            Launcher.instance.GetRocketController().DestroyObject();
        }

        if (other.gameObject.tag == "Coin")
        {
            DataManager.instance.SetCoinsAmount(1);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {

            Launcher.instance.GetRocketController().DestroyObject();
        }
    }
  
}
