using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

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

        SetEffects();
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject runDustPrefab;
    [SerializeField] private GameObject jumpDustPrefab;
    [SerializeField] private GameObject landDustPrefab;

    // Effects : 미리 생성하여 관리 Queue
    [Header("Queue")]
    public Queue<GameObject> runDustQueue = new Queue<GameObject>();
    public Queue<GameObject> jumpDustQueue = new Queue<GameObject>();
    public Queue<GameObject> landDustQueue = new Queue<GameObject>();

    private void SetEffects()
    {
        for (int i = 0; i < 10; i++) // RunDust 10개
        {
            GameObject currentObject = Instantiate(runDustPrefab);
            currentObject.SetActive(false);
            runDustQueue.Enqueue(currentObject);
        }

        for (int i = 0; i < 3; i++) // JumpDust 3개
        {
            GameObject currentObject = Instantiate(jumpDustPrefab);
            currentObject.SetActive(false);
            jumpDustQueue.Enqueue(currentObject);
        }

        for (int i = 0; i < 3; i++) // LandDust 3개
        {
            GameObject currentObject = Instantiate(landDustPrefab);
            currentObject.SetActive(false);
            landDustQueue.Enqueue(currentObject);
        }
    }

    public void RunDust(int direction, Vector3 feetPosition)
    {
        if (runDustQueue.Count < 3)
        {
            return;
        }

        if (direction == 0)
        {
            Vector2[] dustPosition = new Vector2[3] { feetPosition, new Vector2(feetPosition.x + 0.2f, feetPosition.y - 0.1f), new Vector2(feetPosition.x + 0.1f, feetPosition.y + 0.1f) };
            for (int i = 0; i < 3; i++)
            {
                GameObject runDust = runDustQueue.Dequeue();
                runDust.SetActive(true);
                runDust.transform.position = dustPosition[i];
                runDust.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 4.0f, ForceMode2D.Impulse);
            }
        }
        else if (direction == 1)
        {
            Vector2[] dustPosition = new Vector2[3] { feetPosition, new Vector2(feetPosition.x - 0.2f, feetPosition.y - 0.1f), new Vector2(feetPosition.x - 0.1f, feetPosition.y + 0.1f) };
            for (int i = 0; i < 3; i++)
            {
                GameObject runDust = runDustQueue.Dequeue();
                runDust.SetActive(true);
                runDust.transform.position = dustPosition[i];
                runDust.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 4.0f, ForceMode2D.Impulse);
            }
        }
    }

    public void RollDust(Vector3 spawnPosition)
    {
        if (runDustQueue.Count < 1)
        {
            return;
        }

        GameObject runDust = runDustQueue.Dequeue();
        runDust.SetActive(true);
        runDust.transform.position = spawnPosition;
    }

    public void JumpDust(Vector3 spawnPosition)
    {
        if (jumpDustQueue.Count < 1)
        {
            return;
        }

        GameObject jumpDust = jumpDustQueue.Dequeue();
        jumpDust.SetActive(true);
        jumpDust.transform.position = spawnPosition;
        StartCoroutine(Enqueue_Delay(jumpDustQueue, jumpDust, 0.25f));
    }

    public void LandDust(Vector3 spawnPosition)
    {
        if (landDustQueue.Count < 1)
        {
            return;
        }

        GameObject landDust = landDustQueue.Dequeue();
        landDust.SetActive(true);
        landDust.transform.position = spawnPosition;
        StartCoroutine(Enqueue_Delay(landDustQueue, landDust, 0.2f));
    }

    private IEnumerator Enqueue_Delay(Queue<GameObject> queue, GameObject obj, float time) // 지연 비활성화
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        queue.Enqueue(obj);
    }
}
