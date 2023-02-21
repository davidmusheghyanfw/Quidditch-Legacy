using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Camera main;


    private void Awake()
    {
        instance = this;
    }
   
    public void PlayerPosUpdate(Vector3 playerPos)
    {
        transform.position = playerPos;
    }
}
