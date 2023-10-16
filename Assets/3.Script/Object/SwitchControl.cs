using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Sprite[] switchImages;
    [SerializeField] private Sprite[] glowImages;

    [Header("Objects")]
    [SerializeField] private GameObject[] alarms;
    [SerializeField] private GameObject[] treads;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject glow;
    [SerializeField] private GameObject spaceBarUI;

    private SpriteRenderer spr;
    private bool canInteract = false;
    private bool isOn = false;

    private Color empty;
    private Color full;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();

        empty = spaceBarUI.GetComponent<SpriteRenderer>().color;
        full = spaceBarUI.GetComponent<SpriteRenderer>().color;
        empty.a = 0;
        full.a = 1;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 3.0f)
        {
            // Glow
            glow.SetActive(true);

            // UI On
            spaceBarUI.GetComponent<SpriteRenderer>().color = full;

            // Enable Interact
            canInteract = true;
        }
        else
        {
            glow.SetActive(false);
            spaceBarUI.GetComponent<SpriteRenderer>().color = empty;
            canInteract = false;
        }

        if (canInteract && Input.GetKeyDown(KeyCode.Space))
        {
            if (isOn)
            {
                Switch_Off();
            }
            else
            {
                Switch_On();
            }
        }
    }

    public void Switch_On()
    {
        isOn = true;

        spr.sprite = switchImages[1];

        for (int i = 0; i < 5; i++)
        {
            alarms[i].GetComponent<Animator>().SetTrigger("On");
        }

        for (int i = 0; i < treads.Length; i++)
        {
            treads[i].GetComponent<TreadControl>().Switch_On();
        }

        glow.GetComponent<SpriteRenderer>().sprite = glowImages[1];
    }

    public void Switch_Off()
    {
        isOn = false;

        spr.sprite = switchImages[0];

        for (int i = 0; i < 5; i++)
        {
            alarms[i].GetComponent<Animator>().SetTrigger("Off");
        }

        for (int i = 0; i < treads.Length; i++)
        {
            treads[i].GetComponent<TreadControl>().Switch_Off();
        }

        glow.GetComponent<SpriteRenderer>().sprite = glowImages[0];
    }
}
