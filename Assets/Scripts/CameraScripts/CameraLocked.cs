using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocked : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera, setCamera;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.cameraFree = false;
            mainCamera.transform.position = setCamera.transform.position;
        }
    }
}
