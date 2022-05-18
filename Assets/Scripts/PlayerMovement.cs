using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    public PlayerState ps;
    public int nOj;
    [Header("Movement")]
    public float airMultiplier = 0.4f;
    float verticalMovement;
    float horizontalMovement;
    Rigidbody rb;
    Vector3 moveDirection;

    [Header("Drag")]
    public float groundDrag;

    [Header("Jump")]    
    public bool isGrounded;    

    private void Start(){
        ps.gunType = " ";
        ps.moveSpeed = 150f;
        ps.jumpForce = 10f;
        ps.numOfJump = 0;
        nOj = ps.numOfJump;
        Physics.gravity = new Vector3(0f, -30f, 0f);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1 + 0.1f);
        playerInput();
        controlDrag();
        if (Input.GetButtonDown("Jump")) 
        {
            if (nOj < 1) 
            {
                if(isGrounded)
                    Jump();
            }
            else
                if(nOj != 0)
                    Jump();
        }                     
    }
    void Jump(){
        nOj--;
        rb.AddForce(transform.up * ps.jumpForce, ForceMode.Impulse);        
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
            rb.drag = 0.01f;
    }
    private void FixedUpdate(){movePlayer();}
    void movePlayer()
    {
        if (isGrounded) 
        {
            rb.AddForce(moveDirection.normalized * ps.moveSpeed);
            nOj = ps.numOfJump;
        }            
        else            
            rb.AddForce(moveDirection.normalized * ps.moveSpeed * airMultiplier);            
    }
}