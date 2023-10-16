using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUI : MonoBehaviour
{
    public void StartAnimation()
    {
        StartCoroutine(StartAnimation_co());
    }

    private IEnumerator StartAnimation_co()
    {
        Vector2 originalPosition = transform.position;

        while (true)
        {
            while (transform.position.x < originalPosition.x + 49.0f)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, transform.position.x + 50.0f, Time.deltaTime * 1.0f), transform.position.y);

                if (!gameObject.activeSelf)
                {
                    yield break;
                }

                yield return null;
            }

            transform.position = originalPosition;

            yield return null;
        }
    }
}
