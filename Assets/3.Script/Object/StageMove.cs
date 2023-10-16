using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (GameManager.instance.currentStage)
            {
                case 1:
                    GameManager.instance.currentStage += 1;
                    StageManager.instance.ChangeStage(collision.gameObject);
                    break;

                case 2:
                    GameManager.instance.currentStage += 1;
                    StageManager.instance.ChangeStage(collision.gameObject);
                    break;

                case 3:
                    GameManager.instance.GameEnd();
                    break;
            }
        }
    }

    
}
