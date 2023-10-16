using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer")]
    [SerializeField] private GameObject timerUI;
    private float maxTime = 60.0f;
    private float currentTime;

    [Header("SlowMotion")]
    public bool isSlow = false;
    [SerializeField] private GameObject darkImage;
    [SerializeField] private GameObject[] batteryUI;
    public float maxEnergy = 11;
    public float currentEnergy;

    [Header("Status")]
    public bool canClick = true;
    public int savedStage = 1;
    public int currentStage = 1;
    public int score = 0;
    public bool gameStart = false;
    public bool gameOver = false;

    [Header("UI")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject goUI;
    [SerializeField] private GameObject[] gameOverUI;
    private int gameOverNum;

    [Header("Object")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bgmPlayer;

    private IEnumerator slowMotionCoroutine;

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

        ResetStatus();
    }

    private void Update()
    {
        if (!isSlow && !gameOver)
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += Time.deltaTime;
            }

            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
        }

        UpdateEnergyUI();

        if (currentTime <= 0 && !gameOver)
        {
            GameOver(1);
        }

        if (canClick && gameOver && Input.GetMouseButtonDown(0))
        {
            gameOverUI[gameOverNum].SetActive(false);

            if (savedStage == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (savedStage == 2)
            {
                currentStage = 2;
                StageManager.instance.ChangeStage(player);
            }
            else if (savedStage == 3)
            {
                currentStage = 3;
                StageManager.instance.ChangeStage(player);
            }
        }
    }

    public void BGM_Play()
    {
        bgmPlayer.GetComponent<BGMControl>().BGM_Play();
    }

    public void BGM_Stop()
    {
        bgmPlayer.GetComponent<BGMControl>().BGM_Stop();
    }

    public void GameEnd()
    {
        canClick = false;
        gameStart = false;

        player.GetComponent<PlayerController>().Outro();
    }

    public void GameOver(int number)
    {
        Time.timeScale = 1.0f;

        gameOverNum = number;

        gameStart = false;
        gameOver = true;
        gameOverUI[gameOverNum].SetActive(true);
    }

    public void GetScore()
    {
        score += 1;

        if (currentStage == 1)
        {
            if (score == 3)
            {
                goUI.SetActive(true);
                goUI.GetComponent<GoUI>().StartAnimation();
            }
        }
        else if (currentStage == 2)
        {
            goUI.SetActive(true);
            goUI.GetComponent<GoUI>().StartAnimation();
        }
    }

    public void ResetStatus()
    {
        currentEnergy = 0;
        score = 0;
        currentTime = maxTime;
        currentEnergy = maxEnergy;
    }

    public void UpdateEnergyUI()
    {
        for (int i=0; i < (int)Mathf.Floor(currentEnergy); i++)
        {
            batteryUI[i].SetActive(true);
        }

        for (int i=(int)Mathf.Floor(currentEnergy+1); i < 11; i++)
        {
            batteryUI[i].SetActive(false);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(StartTimer_co());
    }

    private IEnumerator StartTimer_co()
    {
        while (currentTime > 0)
        {
            if (!gameStart)
            {
                yield break;
            }

            if (isSlow)
            {
                currentTime -= Time.deltaTime * 4.0f;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
            
            timerUI.GetComponent<Image>().fillAmount = (float)(currentTime / maxTime);
            yield return null;
        }

        yield break;
    }

    public void StartSlowMotion()
    {
        slowMotionCoroutine = StartSlowMotion_co();
        StartCoroutine(slowMotionCoroutine);
    }

    private IEnumerator StartSlowMotion_co()
    {
        isSlow = true;

        Time.timeScale = 0.25f;

        darkImage.SetActive(true);

        while (currentEnergy > 0)
        {
            GameManager.instance.currentEnergy -= Time.deltaTime * 4.0f;
            yield return null;
        }

        Time.timeScale = 1.0f;

        darkImage.SetActive(false);

        isSlow = false;

        yield break;
    }

    public void StopSlowMotion()
    {
        if (isSlow)
        {
            Time.timeScale = 1.0f;

            darkImage.SetActive(false);

            StopCoroutine(slowMotionCoroutine);

            isSlow = false;
        }
    }
}
