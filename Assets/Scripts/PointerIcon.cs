using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerIcon : MonoBehaviour
{
    [SerializeField] Image pointImage;
    [SerializeField] Image targetImage;
    bool isPointShown = true;

    float minDistance = 0.5f;
    float maxDistance = 300f;

    float minScale = 1f;
    float maxScale = 4f;

   
    private void Awake()
    {
        pointImage.enabled = false;
        isPointShown = false;
    }

    public void SetIconPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Show()
    {
        if (isPointShown) return;
        isPointShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowProcess());
    }

    public void Hide()
    {
        if (!isPointShown) return;
        isPointShown = false;

        StopAllCoroutines();
        StartCoroutine(HideProcess());
    }

    IEnumerator ShowProcess()
    {
        targetImage.enabled = false;
        pointImage.enabled = true;
        transform.localScale = Vector3.zero;
        for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
        {
            pointImage.transform.localScale = Vector3.one * t;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    public void CorrectTargetScale(float distance)
    {
        var scale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(maxDistance, minDistance, distance));
        targetImage.transform.localScale = new Vector3(scale, scale, scale);
    }

    IEnumerator HideProcess()
    {

        for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
        {
            pointImage.transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }
        pointImage.enabled = false;
        targetImage.enabled = true;
    }

}
