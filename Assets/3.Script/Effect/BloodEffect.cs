using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        int num = Random.Range(0, 3);
        animator = GetComponent<Animator>();
        animator.SetTrigger(num.ToString());
    }
}
