using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            //GameView.instance.UpdateScore();
        }
        if (other.gameObject.tag == "Finish")
        {
            gameObject.GetComponent<CharacterController>().StartCharacterStoppingRoutin();
        }
    }
}
