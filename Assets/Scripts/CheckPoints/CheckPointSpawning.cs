using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSpawning : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private Vector2 spawningPosX;
    [SerializeField] private Vector2 spawningPosY;
    [SerializeField] private GameObject checkPoint;
    [SerializeField] private Transform parent;

    private List<GameObject> checkPoints = new List<GameObject>();

    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPoints.Count < 12)
        {
            spawnPos.Set(Random.Range(spawningPosX.x, spawningPosX.y),
                Random.Range(spawningPosY.x, spawningPosY.y), offset + (checkPoints.Count == 0 ? 0 : checkPoints[checkPoints.Count-1].transform.position.z));

            var currentCheckPoint = Instantiate(checkPoint, spawnPos, transform.rotation, parent);

            checkPoints.Add(currentCheckPoint);
        }

    }
}
