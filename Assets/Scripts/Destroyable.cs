using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private GameObject burnParticle;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Color32 normalColor;
    [SerializeField] private Color32 burnColor;

    private void Start()
    {
        IsDamaged(false);
    }

    public void IsDamaged(bool value)
    {
        burnParticle.SetActive(value);
        for (int i = 0; i < mesh.materials.Length; i++)
        {
            mesh.materials[0].color = value ? burnColor : normalColor;
        }
        
    }
}
