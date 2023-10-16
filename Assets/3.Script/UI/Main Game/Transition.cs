using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject fadePanel;
    private GameObject[] panels;

    private void Awake()
    {
        panels = new GameObject[39];

        SetPanel();
        FadeIn();
    }

    private void SetPanel()
    {
        float x = 950;
        int index = 0;

        while (x >= -950)
        {
            GameObject currentTransitionPanel = Instantiate(fadePanel, transform.position, Quaternion.identity, transform.parent);
            currentTransitionPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, 0, 0);
            panels[index] = currentTransitionPanel;
            index += 1;
            x -= 50;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut_co());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeIn_co());
    }

    private IEnumerator FadeOut_co()
    {
        GameManager.instance.canClick = false;

        for (int i = 0; i < 39; i++)
        {
            for (int j = 0; j < 24; j++)
            {
                panels[i].transform.GetChild(j).GetComponent<Animator>().SetTrigger("FadeOut");
            }

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2.0f);

        GameManager.instance.canClick = true;

        yield break;
    }

    private IEnumerator FadeIn_co()
    {
        GameManager.instance.canClick = false;

        for (int i = 0; i < 39; i++)
        {
            for (int j = 0; j < 24; j++)
            {
                panels[i].transform.GetChild(j).GetComponent<Animator>().SetTrigger("FadeIn");
            }

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2.0f);

        GameManager.instance.canClick = true;

        yield break;
    }
}
