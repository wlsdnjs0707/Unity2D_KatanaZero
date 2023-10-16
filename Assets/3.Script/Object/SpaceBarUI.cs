using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBarUI : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Sprite[] images;
    private bool isPushed = false;
    private float timer = 0;

    void Update()
    {
        if (timer < 0.5f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;

            if (isPushed)
            {
                isPushed = false;
                GetComponent<SpriteRenderer>().sprite = images[0];
            }
            else
            {
                isPushed = true;
                GetComponent<SpriteRenderer>().sprite = images[1];
            }
        }

        
    }
}
