using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoading : MonoBehaviour
{
    private float timer = 0;

    private void Update()
    {
        if (timer < 3.0f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
