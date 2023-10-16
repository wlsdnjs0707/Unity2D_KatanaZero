using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField] RectTransform CustomCursor;

    private void Awake()
    {
        Cursor.visible = false; // 기본 커서 숨기기
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        CustomCursor.position = mousePosition;
    }
}
