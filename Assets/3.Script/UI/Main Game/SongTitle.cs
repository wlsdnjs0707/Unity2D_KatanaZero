using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongTitle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject songTitleBackground;
    [SerializeField] private GameObject nowPlayingText;
    [SerializeField] private GameObject headPhone;
    [SerializeField] private GameObject underLine;
    [SerializeField] private GameObject songTitleText;

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
        while(songTitleBackground.GetComponent<Image>().fillAmount < 0.99f)
        {
            songTitleBackground.GetComponent<Image>().fillAmount = Mathf.Lerp(songTitleBackground.GetComponent<Image>().fillAmount, 1.0f, Time.deltaTime * 3.5f);
            yield return null;
        }

        songTitleBackground.GetComponent<Image>().fillAmount = 1;

        Color textColor = nowPlayingText.GetComponent<Text>().color;
        Color headPhoneColor = headPhone.GetComponent<Image>().color;

        while (nowPlayingText.GetComponent<Text>().color.a < 1.0)
        {
            textColor.a += Time.deltaTime * 1.5f;
            headPhoneColor.a += Time.deltaTime * 1.5f;

            nowPlayingText.GetComponent<Text>().color = textColor;
            headPhone.GetComponent<Image>().color = headPhoneColor;

            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        while (underLine.GetComponent<Image>().fillAmount < 0.99f)
        {
            underLine.GetComponent<Image>().fillAmount = Mathf.Lerp(underLine.GetComponent<Image>().fillAmount, 1.0f, Time.deltaTime * 20.0f);
            yield return null;
        }

        Color songTitleTextColor = songTitleText.GetComponent<Text>().color;

        while (songTitleText.GetComponent<Text>().color.a < 1.0)
        {
            songTitleTextColor.a += Time.deltaTime * 2.0f;

            songTitleText.GetComponent<Text>().color = songTitleTextColor;

            yield return null;
        }
    }

    private IEnumerator StopAnimation_co()
    {
        Color color = songTitleBackground.GetComponent<Image>().color;
        
        while (color.a > 0)
        {
            color.a -= Time.deltaTime * 5.0f;

            songTitleBackground.GetComponent<Image>().color = color;
            nowPlayingText.GetComponent<Text>().color = color;
            headPhone.GetComponent<Image>().color = color;
            underLine.GetComponent<Image>().color = color;
            songTitleText.GetComponent<Text>().color = color;

            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }
}
