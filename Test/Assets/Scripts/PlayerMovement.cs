using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed = 6f;
    public Transform cameraTransform;
    Vector2 moveInput;
    Vector3 movement;
    public Transform GroundCheck;
    public LayerMask groundMask;
    private bool isGrounded;
    private float gravity = 9.8f;
    private float groundDistance;
    Vector3 velocity;


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update() {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        forward.Normalize();
        right.Normalize();

        movement = forward * moveInput.y + right * moveInput.x;
        movement *= moveSpeed;

        characterController.Move(movement * Time.deltaTime);
        velocity.y -= gravity*Time.deltaTime;
        characterController.Move(velocity*Time.deltaTime);
    }
}
