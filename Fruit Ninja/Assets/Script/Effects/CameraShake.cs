using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Vector2 cameraShake;
    private Transform cameraTransform;
    private Vector3 intialPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        intialPosition = cameraTransform.position;
    }

    public void CameraShakes()
    {
        HorizontalCameraShake();
    }

    private void HorizontalCameraShake()
    {
        LeanTween.moveX(cameraTransform.gameObject, cameraShake.x, 0.01f).setOnComplete(VerticalCameraShake);
    }

    private void VerticalCameraShake()
    {
        LeanTween.moveY(cameraTransform.gameObject, cameraShake.y, 0.05f).setOnComplete(DefaultCameraPosition);
    }

    private void DefaultCameraPosition()
    {
        LeanTween.move(cameraTransform.gameObject, intialPosition, 0.05f).setOnComplete(DefaultCameraPosition);
    }
}
