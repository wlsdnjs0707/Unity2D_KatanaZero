using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

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

    [Header("UI")]
    [SerializeField] private GameObject hudPanel;

    [Header("Interactable Object")]
    [SerializeField] private GameObject laserSwitch;

    public void ChangeStage(GameObject player)
    {
        StartCoroutine(ChangeStage_co(player));
    }

    private IEnumerator ChangeStage_co(GameObject player)
    {
        if (GameManager.instance.currentStage == 2)
        {
            GameManager.instance.gameStart = false;
            GameManager.instance.savedStage = GameManager.instance.currentStage;

            player.GetComponent<PlayerController>().canMove = false;

            hudPanel.GetComponent<Transition>().FadeOut();

            yield return new WaitForSeconds(2.0f);

            laserSwitch.GetComponent<SwitchControl>().Switch_On();

            CameraControl.instance.SetPosition(new Vector3(0, -32.0f, 0));

            player.transform.position = new Vector3(-20.5f, -35.8f, 0);

            player.GetComponent<Animator>().SetTrigger("Idle");
            player.GetComponent<SpriteRenderer>().flipX = false;

            hudPanel.GetComponent<Transition>().FadeIn();

            yield return new WaitForSeconds(2.0f);

            GameManager.instance.ResetStatus();
            GameManager.instance.gameStart = true;
            GameManager.instance.gameOver = false;
            GameManager.instance.StartTimer();
            player.GetComponent<PlayerController>().ResetStatus();
        }
        else if (GameManager.instance.currentStage == 3)
        {
            GameManager.instance.gameStart = false;
            GameManager.instance.savedStage = GameManager.instance.currentStage;

            player.GetComponent<PlayerController>().canMove = false;

            hudPanel.GetComponent<Transition>().FadeOut();

            yield return new WaitForSeconds(2.0f);

            CameraControl.instance.SetPosition(new Vector3(0, -70.0f, 0));

            player.transform.position = new Vector3(-20.5f, -76.87f, 0);

            player.GetComponent<Animator>().SetTrigger("Idle");
            player.GetComponent<SpriteRenderer>().flipX = false;

            hudPanel.GetComponent<Transition>().FadeIn();

            yield return new WaitForSeconds(2.0f);

            GameManager.instance.ResetStatus();
            GameManager.instance.gameStart = true;
            GameManager.instance.gameOver = false;
            GameManager.instance.StartTimer();
            player.GetComponent<PlayerController>().ResetStatus();
        }

        yield break;
    }
}
