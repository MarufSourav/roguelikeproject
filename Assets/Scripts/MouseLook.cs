using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Vector2 sensitivity;
    [SerializeField] private float maxVerticalAngleFromHorizon;
    private Vector2 rotation;
    private Vector2 GetInput() 
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"), 
            Input.GetAxis("Mouse Y")
        );
        return input;
    }
    private float ClampVerticalAngle(float angle) 
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }
    private void Update()
    {
        Vector2 wantedVelocity = GetInput() * sensitivity;
        rotation += wantedVelocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }
}
