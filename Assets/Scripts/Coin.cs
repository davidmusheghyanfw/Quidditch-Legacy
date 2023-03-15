using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject visualContainer;
    [SerializeField] private Vector3 coinScale;
    [SerializeField] private float rotateSpeed;

    private void Start()
    {
        coinScale = visualContainer.GetComponentInChildren<Transform>().localScale;      
        StartCoinRotateRoutine();
    }

    Coroutine CoinRotateRoutineC;
    private IEnumerator CoinRotateRoutine()
    {
        while (true)
        {
            transform.Rotate(transform.up, 360 * rotateSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
    }


    private void StopCoinRotateRoutine()
    {
        if (CoinRotateRoutineC != null) StopCoroutine(CoinRotateRoutineC);
    }

    private void StartCoinRotateRoutine()
    {
        if (CoinRotateRoutineC != null) StopCoroutine(CoinRotateRoutineC);
        CoinRotateRoutineC = StartCoroutine(CoinRotateRoutine());
    }
}
