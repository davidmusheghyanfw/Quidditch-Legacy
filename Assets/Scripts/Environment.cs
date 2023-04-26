using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private GameObject burnParticle;

    public void IsDamaged(bool value)
    {
        burnParticle.SetActive(value);
    }
}
