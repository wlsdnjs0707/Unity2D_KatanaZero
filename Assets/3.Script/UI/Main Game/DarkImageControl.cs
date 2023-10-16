using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkImageControl : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] GameObject mainCamera;

    private void Update()
    {
        transform.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}
