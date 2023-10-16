using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeControl : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enemy.GetComponent<GruntController>().currentState != State.Dead && enemy.GetComponent<GruntController>().currentState != State.Attack)
            {
                enemy.GetComponent<GruntController>().PlayerInAttackRange();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (enemy.GetComponent<GruntController>().currentState != State.Dead && enemy.GetComponent<GruntController>().currentState == State.Attack)
            {
                enemy.GetComponent<GruntController>().PlayerOutAttackRange();
            }
        }
    }
}
