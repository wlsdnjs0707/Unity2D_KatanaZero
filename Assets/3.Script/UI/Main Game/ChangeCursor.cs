using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField] RectTransform CustomCursor;

    private void Awake()
    {
        Cursor.visible = false; // �⺻ Ŀ�� �����
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        CustomCursor.position = mousePosition;
    }
}
