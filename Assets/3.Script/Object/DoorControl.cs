using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool isOpen = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        animator.speed = 0.0f;
    }

    public void OpenDoor()
    {
        if (isOpen) { return; }

        boxCollider.enabled = false;
        animator.speed = 1;
    }
}
