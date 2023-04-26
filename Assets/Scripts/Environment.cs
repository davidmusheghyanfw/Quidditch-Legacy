using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private GameObject burnParticle;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Color32 normalColor;
    [SerializeField] private Color32 burnColor;

    private void Start()
    {
        mesh.material.color = normalColor;
    }

    public void IsDamaged(bool value)
    {
        burnParticle.SetActive(value);
        mesh.material.color = value ? burnColor : normalColor;
    }
}
