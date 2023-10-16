using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour
{
    private BoxCollider2D bc;
    private bool isOn = true;
    private Color red;
    private Color original;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();

        original = GetComponent<SpriteRenderer>().color;
        red = Color.red;
    }

    private void Update()
    {
        if (GameManager.instance.isSlow)
        {
            if (isOn)
            {
                GetComponent<SpriteRenderer>().color = red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = original;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = original;
        }
    }

    public void ToggleCollider()
    {
        if (isOn)
        {
            isOn = false;
            bc.enabled = false;
        }
        else
        {
            isOn = true;
            bc.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponent<PlayerController>().invincible && !collision.gameObject.GetComponent<PlayerController>().isDead)
            {
                Time.timeScale = 1.0f;
                collision.gameObject.GetComponent<PlayerController>().Dead();
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                if (collision.gameObject.transform.position.x <= transform.position.x)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 15.0f, ForceMode2D.Impulse);
                }
                else
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 15.0f, ForceMode2D.Impulse);
                }
            }
        }
    }
}
