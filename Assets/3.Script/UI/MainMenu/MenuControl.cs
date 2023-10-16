using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject bar;
    [SerializeField] GameObject keyPanel;

    private int menuIndex = 0;
    private float[] targetPositions = new float[3] { -265, -350, -435 };
    private bool isMoving = false;
    private bool keyPanelOn = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isMoving && !keyPanelOn)
        {
            // 위
            if (menuIndex != 0)
            {
                menuIndex -= 1;
                StartCoroutine(MoveBar_co());
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && !isMoving && !keyPanelOn)
        {
            // 아래
            if (menuIndex != 2)
            {
                menuIndex += 1;
                StartCoroutine(MoveBar_co());
            }
        }
        else if(Input.GetKeyDown(KeyCode.Return) && !keyPanelOn)
        {
            // 선택
            switch (menuIndex)
            {
                case 0:
                    SceneManager.LoadScene("MainGame");
                    break;

                case 1:
                    keyPanelOn = true;
                    keyPanel.SetActive(true);
                    break;

                case 2:
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            keyPanelOn = false;
            keyPanel.SetActive(false);
        }
    }

    private IEnumerator MoveBar_co()
    {
        isMoving = true;

        RectTransform barRect = bar.GetComponent<RectTransform>();

        while (Mathf.Abs(barRect.anchoredPosition.y - targetPositions[menuIndex]) > 1.0f)
        {
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector3(barRect.anchoredPosition.x, Mathf.Lerp(barRect.anchoredPosition.y, targetPositions[menuIndex], 50.0f * Time.deltaTime), 0);

            yield return null;
        }

        barRect.anchoredPosition = new Vector3(barRect.anchoredPosition.x, targetPositions[menuIndex], 0);

        isMoving = false;

        yield break;
    }
}
