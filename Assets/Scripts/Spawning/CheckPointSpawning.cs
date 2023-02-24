using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSpawning : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float nextSegmentCreationOffset;
    [SerializeField] private int maxSegmentAmount;
    [SerializeField] private int visibleSegmentCount;
    [SerializeField] private Vector2 spawningPosX;
    [SerializeField] private Vector2 spawningPosY;

    [SerializeField] private GameObject checkPoint;
    [SerializeField] private Transform parent;

    [SerializeField] private Transform player;

    private List<GameObject> checkPoints = new List<GameObject>();

    GameObject currentCheckPoint;

    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckPointSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private IEnumerator CheckPointSpawnRoutine()
    {
        while (true)
        {
            if (checkPoints.Count == 0) SpawnNewCheckPoint();

            if (player.position.z > checkPoints[visibleSegmentCount - 1].transform.position.z + nextSegmentCreationOffset) DestroyCheckPoint();
            
            if (player.position.z < checkPoints[checkPoints.Count - 1].transform.position.z + nextSegmentCreationOffset
                && checkPoints.Count - 1 < maxSegmentAmount * visibleSegmentCount)
            {
                SpawnNewCheckPoint();
            }


            yield return new WaitForEndOfFrame();
        }
    }
    private void SpawnNewCheckPoint()
    {
        for (int i = 0; i < visibleSegmentCount; i++)
        {
            spawnPos.Set(Random.Range(spawningPosX.x, spawningPosX.y),
             Random.Range(spawningPosY.x, spawningPosY.y), offset + (checkPoints.Count == 0 ? 0 : checkPoints[checkPoints.Count - 1].transform.position.z));

            currentCheckPoint = Instantiate(checkPoint, spawnPos, transform.rotation, parent);

            checkPoints.Add(currentCheckPoint);
        }
       
    }

    private void DestroyCheckPoint()
    {
        for (int i = 0; i < visibleSegmentCount; i++)
        {
            Destroy(checkPoints[i].gameObject);
        }

        for (int i = 0; i < visibleSegmentCount; i++)
        {
            checkPoints.RemoveAt(i);
        }

       
    }
}
