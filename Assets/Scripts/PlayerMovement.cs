using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerState ps;
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float airMultiplier = 0.4f;
    float verticalMovement;
    float horizontalMovement;
    Rigidbody rb;
    Vector3 moveDirection;

    [Header("Drag")]
    public float groundDrag;
    public float airDrag;

    [Header("Jump")]
    public float jumpForce;
    public bool isGrounded;

    private void Start(){
        ps.gunType = " ";
        Physics.gravity = new Vector3(0f, -30f, 0f);
        rb = GetComponent<Rigidbody>();
    }
    private void Update(){
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1 + 0.1f);
        playerInput();
        controlDrag();
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
    }
    void Jump(){
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void playerInput(){
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
    void controlDrag(){
        if (isGrounded)
            rb.drag = groundDrag;
        else 
            rb.drag = airDrag;
    }
    private void FixedUpdate(){movePlayer();}
    void movePlayer(){
        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier);
    }
}