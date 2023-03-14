using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject visualContainer;
    [SerializeField] private Vector3 coinScale;

    private void Start()
    {
        coinScale = visualContainer.GetComponentInChildren<Transform>().localScale;      
    }

}
