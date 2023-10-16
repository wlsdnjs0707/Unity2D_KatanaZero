using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDust : MonoBehaviour
{
    private float lifeTime = 0.5f;

    private Color originalColor;
    private Vector3 originalScale;

    private void Awake()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if(gameObject.activeSelf)
        {
            if (lifeTime > 0)
            {
                Color color = GetComponent<SpriteRenderer>().color;

                lifeTime -= Time.deltaTime;

                color.a -= 1.5f * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = color;

                transform.localScale += Vector3.one * Time.deltaTime * 1f;
                transform.position += Vector3.up * Time.deltaTime * 0.75f;
            }
            else
            {
                gameObject.SetActive(false);
                ResetTransform();
                EffectManager.instance.runDustQueue.Enqueue(gameObject);
            }
        }
    }

    private void ResetTransform()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        transform.localScale = originalScale;
        lifeTime = 0.5f;
    }
}
