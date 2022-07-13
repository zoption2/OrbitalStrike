using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private float paralaxSpeed = 0.9f;
    private float lastCameraX;
    private float lastCameraY;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        float deltaY = cameraTransform.position.y - lastCameraY;
        transform.position += (Vector3.right * deltaX * paralaxSpeed) + (Vector3.up * deltaY * paralaxSpeed);
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
    }
}
