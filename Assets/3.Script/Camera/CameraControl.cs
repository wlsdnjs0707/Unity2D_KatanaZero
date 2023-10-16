using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Player")]
    [SerializeField] private GameObject player;

    // Shake Camera
    private float shakeTimer;
    private Vector3 currentPosition;

    private void Update()
    {
        if (GameManager.instance.currentStage == 1)
        {
            if (player.transform.position.x >= 0 && player.transform.position.x <= 12)
            {
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }
        }
        else if (GameManager.instance.currentStage == 2)
        {
            if (player.transform.position.x >= 0 && player.transform.position.x <= 27)
            {
                transform.position = new Vector3(player.transform.position.x, -32.0f, transform.position.z);
            }
        }
        else if (GameManager.instance.currentStage == 3)
        {
            if (player.transform.position.x >= 0 && player.transform.position.x <= 18)
            {
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }

            if (player.transform.position.y >= -70 && player.transform.position.y <= -54.5)
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            }
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    public void ShakeCamera(float shakeTime)
    {
        shakeTimer = shakeTime;
        currentPosition = transform.position;
        StartCoroutine(ShakeCamera_co());
    }

    private IEnumerator ShakeCamera_co()
    {
        while (shakeTimer > 0)
        {
            transform.position = Random.insideUnitSphere * 2.0f + currentPosition;
            shakeTimer -= Time.deltaTime;

            yield return null;
        }

        shakeTimer = 0;
        transform.position = currentPosition;

        yield break;
    }
}
