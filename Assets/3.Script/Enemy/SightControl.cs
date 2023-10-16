using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightControl : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemy;

    private bool isFound = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFound && enemy.GetComponent<GruntController>().currentState != State.Dead)
        {
            if (collision.CompareTag("Player"))
            {
                isFound = true;
                enemy.GetComponent<GruntController>().FindPlayer(collision.gameObject);
            }
        }
    }
}
