using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            Debug.Log("s");
        }
        if (other.gameObject.tag == "Finish")
        {
            GameManager.instance.GameWin();
        }
    }
}
