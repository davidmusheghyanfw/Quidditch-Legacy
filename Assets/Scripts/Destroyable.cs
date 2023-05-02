using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private GameObject burnParticle;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Color32 normalColor;
    [SerializeField] private Color32 burnColor;
    [SerializeField] public bool isVisualDestroy;
    [SerializeField]private GameObject mainBody;
    [SerializeField]private List<GameObject> detachParts;

    private Vector3 dir;

    private void Start()
    {
        IsDamaged(false);
        PointerManager.Instance.AddToList(this);
    }

    public void IsDamaged(bool value)
    {
        burnParticle.SetActive(value);
        
        for (int i = 0; i < mesh.materials.Length; i++)
        {
            mesh.materials[0].color = value ? burnColor : normalColor;
        }
        if (value && isVisualDestroy)
        {
            VisualDamage();
            PointerManager.Instance.RemoveFromList(this);
        }
    }

    private void VisualDamage()
    {
        if (!mainBody && detachParts.Count == 0) return;
       
        if (mainBody)
        {
            dir.Set(Random.Range(-1, 2), 1, Random.Range(-1, 2));
            mainBody.GetComponent<Rigidbody>().isKinematic = false;
            mainBody.GetComponent<Rigidbody>().AddForce(dir * 50f);
            mainBody.GetComponent<BoxCollider>().enabled = true;
        }
        
        if (detachParts.Count != 0)
        {
            
            for (int i = 0; i < detachParts.Count; i++)
            {
                dir.Set(Random.Range(-1, 2), 1, Random.Range(-1, 2));
                detachParts[i].GetComponent<Rigidbody>().isKinematic = false;
                detachParts[i].GetComponent<Rigidbody>().AddForce(dir * 100f);
                detachParts[i].GetComponent<BoxCollider>().enabled = true;
            }
            
        }
    }

}