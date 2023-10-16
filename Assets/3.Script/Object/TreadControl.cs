using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadControl : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private Sprite[] treadImages;
    [SerializeField] private GameObject[] linePrefabs;

    private ParticleSystem ps;
    private BoxCollider2D bc;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        bc = GetComponent<BoxCollider2D>();
    }

    public void Switch_On()
    {
        GetComponent<SpriteRenderer>().sprite = treadImages[1];
        ps.Play();
        bc.enabled = true;
    }

    public void Switch_Off()
    {
        GetComponent<SpriteRenderer>().sprite = treadImages[0];
        ps.Stop();
        ps.Clear();
        bc.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LaserEffect_co());

            if (!collision.GetComponent<PlayerController>().invincible && !collision.GetComponent<PlayerController>().isDead)
            {
                collision.GetComponent<PlayerController>().LaserDead();
            }
        }
    }

    private IEnumerator LaserEffect_co()
    {
        CameraControl.instance.ShakeCamera(0.125f);

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 7.5f, transform.position.z);

        GameObject line1 = Instantiate(linePrefabs[0], spawnPosition, Quaternion.identity);
        Destroy(line1, 0.05f);

        yield return new WaitForSeconds(0.05f);

        GameObject line2 = Instantiate(linePrefabs[1], spawnPosition, Quaternion.identity);
        Destroy(line2, 0.05f);
    }
}
