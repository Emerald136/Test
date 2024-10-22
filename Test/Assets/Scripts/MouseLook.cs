using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 0.05f;

    private Transform playerBody;
    private float cameraPitch = 0.0f;

    private void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue(); 

        float lookX = mouseDelta.x * mouseSensitivity;
        float lookY = mouseDelta.y * mouseSensitivity;

        cameraPitch -= lookY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerBody.Rotate(Vector3.up * lookX);

        transform.localEulerAngles = Vector3.right * cameraPitch;
    }
}
