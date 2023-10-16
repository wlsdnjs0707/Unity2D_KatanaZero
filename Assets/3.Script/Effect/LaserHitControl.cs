using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitControl : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject effect;

    public void Fade()
    {
        StartCoroutine(Fade_co());
    }

    private IEnumerator Fade_co()
    {
        Color color1 = bg.GetComponent<SpriteRenderer>().color;
        Color color2 = color1;
        color2.a = color1.a * 0.5f;

        while(color1.a > 0)
        {
            color1.a -= Time.deltaTime * 0.25f;
            color2.a = color1.a * 0.5f;

            bg.GetComponent<SpriteRenderer>().color = color1;
            effect.GetComponent<SpriteRenderer>().color = color2;

            yield return null;
        }

        yield break;
    }
}
