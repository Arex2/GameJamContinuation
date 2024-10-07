using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    private float smoothing = 1.0f;

    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
        //transform.position = new Vector3(newPosition.x, transform.position.y) + offset; //the camera will not move along the Y axis
        transform.position = newPosition;
    }
}
