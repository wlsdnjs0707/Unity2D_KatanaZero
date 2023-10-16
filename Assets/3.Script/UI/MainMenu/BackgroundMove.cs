using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [Header("Movement")]
    public float targetY = 0;
    public float moveSpeed = 0;

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();

        StartCoroutine(MoveUp_co());
    }

    private IEnumerator MoveUp_co()
    {
        while (rt.anchoredPosition.y < targetY - 1.0f)
        {
            rt.anchoredPosition = new Vector3(rt.anchoredPosition.x, Mathf.Lerp(rt.anchoredPosition.y, targetY, moveSpeed * Time.deltaTime), 0);

            yield return null;
        }

        rt.anchoredPosition = new Vector3(rt.anchoredPosition.x, targetY, 0);

        yield break;
    }
}
