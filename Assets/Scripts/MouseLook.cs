using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MouseLook : MonoBehaviour
{
    public Slider sliderValue;
    [SerializeField] Transform playerCamera;
    [SerializeField] float sensitivity;
    [SerializeField] float maxVerticalAngleFromHorizon;
    float cameraPitch = 0.0f;
    public PlayerState ps;
    private void Start()
    {
        sensitivity = ps.sensitivity;        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        sliderValue.value = sensitivity * 10f;
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        cameraPitch -= input.y * sensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * input.x * sensitivity);
    }
    public void mouseSens(float sens)
    {
        sensitivity = sens * 0.1f;
        ps.sensitivity = sensitivity;
    }
}