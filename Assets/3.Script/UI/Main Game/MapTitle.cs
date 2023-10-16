using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTitle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mapTitleBackground;
    [SerializeField] private GameObject leftText;
    [SerializeField] private GameObject rightText;
    [SerializeField] private GameObject middleText;
    [SerializeField] private GameObject mouseIcon;
    [SerializeField] private Sprite[] mouseIcons;

    private IEnumerator mouseCoroutine;

    private void Awake()
    {
        mouseCoroutine = MouseIcon_co();
    }

    public void StartAnimation()
    {
        StartCoroutine(StartAnimation_co());
    }

    public void StopAnimation()
    {
        StartCoroutine(StopAnimation_co());
    }

    private IEnumerator StartAnimation_co()
    {
        Color color = mapTitleBackground.GetComponent<Image>().color;

        while (mapTitleBackground.GetComponent<Image>().color.a < 200.0f / 255.0f)
        {
            color.a += Time.deltaTime * 10.0f;
            mapTitleBackground.GetComponent<Image>().color = color;

            yield return null;
        }

        StartCoroutine(LeftText_co());
        StartCoroutine(RightText_co());

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(mouseCoroutine);

        yield break;
    }

    private IEnumerator MouseIcon_co()
    {
        middleText.SetActive(true);
        mouseIcon.SetActive(true);

        while (true)
        {
            middleText.SetActive(true);
            mouseIcon.GetComponent<Image>().sprite = mouseIcons[1];

            yield return new WaitForSeconds(0.5f);

            middleText.SetActive(false);
            mouseIcon.GetComponent<Image>().sprite = mouseIcons[0];

            yield return new WaitForSeconds(0.5f);

            yield return null;
        }
    }

    private IEnumerator LeftText_co()
    {
        while (leftText.GetComponent<RectTransform>().anchoredPosition.x < 1.0f)
        {
            leftText.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(Mathf.Lerp(leftText.GetComponent<RectTransform>().anchoredPosition.x, 0f, Time.deltaTime * 5.0f), -2, 0);

            yield return null;
        }
    }

    private IEnumerator RightText_co()
    {
        while (rightText.GetComponent<RectTransform>().anchoredPosition.x > 1.0f)
        {
            rightText.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(Mathf.Lerp(rightText.GetComponent<RectTransform>().anchoredPosition.x, 0f, Time.deltaTime * 5.0f), 2, 0);

            yield return null;
        }
    }

    private IEnumerator StopAnimation_co()
    {
        StopCoroutine(mouseCoroutine);

        middleText.SetActive(false);
        mouseIcon.SetActive(false);

        Color textColor = leftText.GetComponent<Text>().color;

        while (leftText.GetComponent<Text>().color.a > 0)
        {
            textColor.a -= Time.deltaTime * 10.0f;

            leftText.GetComponent<Text>().color = textColor;
            rightText.GetComponent<Text>().color = textColor;

            yield return null;
        }

        Color color = mapTitleBackground.GetComponent<Image>().color;

        while (mapTitleBackground.GetComponent<Image>().color.a > 0)
        {
            color.a -= Time.deltaTime * 10.0f;
            mapTitleBackground.GetComponent<Image>().color = color;

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
