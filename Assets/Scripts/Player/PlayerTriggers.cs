using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            GameView.instance.UpdateScore();
            Debug.Log("triggered");
        }
    }
}
