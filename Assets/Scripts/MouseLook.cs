using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MouseLook : MonoBehaviour
{
    public Slider sliderValue;
    
    [Flags]
    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }
    [SerializeField] private Vector2 sensitivity;
    [SerializeField] private float maxVerticalAngleFromHorizon;
    [SerializeField] private RotationDirection rotationDirections;
    private Vector2 rotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private Vector2 GetInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );
        return input;
    }
    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }
    private void Update()
    {
        sliderValue.value = sensitivity.x * 0.1f;
        Vector2 wantedVelocity = GetInput() * sensitivity;
        if ((rotationDirections & RotationDirection.Horizontal) == 0)
            wantedVelocity.x = 0;
        if ((rotationDirections & RotationDirection.Vertical) == 0)
            wantedVelocity.y = 0;
        rotation += wantedVelocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }
    public void mouseSens(float sens)
    {
        sensitivity.x = sens * 10;
        sensitivity.y = -sens * 10;
    }
}