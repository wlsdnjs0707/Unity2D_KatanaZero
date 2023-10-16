using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioClip bgm;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BGM_Play()
    {
        audioSource.Play();
    }

    public void BGM_Stop()
    {
        audioSource.Stop();
    }
}
