using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftButtonImageOnOff : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Sprite[] buttonImage;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetImageOn() // Pushed
    {
        image.sprite = buttonImage[0];
    }   
    
    public void SetImageOff() // Released
    {
        image.sprite = buttonImage[1];
    }
}
