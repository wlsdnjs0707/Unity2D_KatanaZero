using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangsterController : MonoBehaviour
{
    [Header("State")]
    public State currentState;

    // Components
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] bloodPrefabs;
    [SerializeField] private GameObject smallBloodPrefab;

    private void Awake()
    {
        currentState = State.Idle;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Dead(Vector3 playerPosition)
    {
        if (currentState == State.Dead)
        {
            return;
        }

        currentState = State.Dead;

        GameManager.instance.GetScore();

        rb.AddForce(((transform.position - playerPosition).normalized) * 15.0f, ForceMode2D.Impulse);

        for (int i = 0; i < 10; i++)
        {
            float power = Random.Range(7.5f, 15.0f);

            GameObject smallBlood = Instantiate(smallBloodPrefab, transform.position, Quaternion.identity);
            smallBlood.GetComponent<Rigidbody2D>().AddForce(((transform.position - playerPosition).normalized + Vector3.up) * power, ForceMode2D.Impulse);
            Destroy(smallBlood, 2.0f);
        }

        StartCoroutine(BloodEffect_co());
    }

    private IEnumerator BloodEffect_co()
    {
        int num = Random.Range(0, 4);

        if (!GameManager.instance.isSlow)
        {
            Time.timeScale = 0.25f;
        }

        currentState = State.Dead;

        animator.SetTrigger("Dead");

        GameObject blood = Instantiate(bloodPrefabs[num], new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity);

        if (rb.velocity.x < 0)
        {
            blood.GetComponent<SpriteRenderer>().flipX = true;
        }

        yield return new WaitForSeconds(0.05f);

        if (!GameManager.instance.isSlow)
        {
            Time.timeScale = 1.0f;
        }
    }

}
