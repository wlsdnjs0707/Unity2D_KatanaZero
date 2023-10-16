using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayController : MonoBehaviour
{
    private PlatformEffector2D effector;
    private PlayerController pc;
    private Animator animator;

    private float pushTime = 0;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (pc != null)
        {
            if (pc.fallThrough && effector.rotationalOffset == 0f)
            {
                if (pushTime >= 0.75f)
                {
                    pushTime = 0;
                    ToggleRotationalOffset();
                    animator.SetTrigger("Crouch");
                }
                else
                {
                    pushTime += Time.deltaTime;
                }
            }
            else if (!pc.fallThrough)
            {
                pushTime = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            pc = collision.gameObject.GetComponent<PlayerController>();
            animator = collision.gameObject.GetComponent<Animator>();
        }
    }

    private void ToggleRotationalOffset()
    {
        effector.rotationalOffset = 180.0f;
        pc = null;
        StartCoroutine(ResetOffset_co());
    }

    private IEnumerator ResetOffset_co()
    {
        yield return new WaitForSeconds(1.0f);
        effector.rotationalOffset = 0f;
    }
}
