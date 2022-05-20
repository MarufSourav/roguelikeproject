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
    bool readyToMove = true;
    bool readyToDash = true;

    [Header("Drag")]
    public float groundDrag;
    public float airDrag = 0.01f;

    [Header("Jump")]    
    public bool isGrounded;

    private void Start(){
        ps.dashSpeed = ps.moveSpeed;
        ps.gunType = " ";
        ps.moveSpeed = 150f;
        ps.jumpForce = 10f;
        ps.numOfJump = 0;
        ps.dashSpeed = 90f;
        ps.dashCoolDown = 2f;
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
        if ((Mathf.Abs(horizontalMovement) > 0.2 || Mathf.Abs(verticalMovement) > 0.2) && Input.GetKeyDown(KeyCode.LeftShift) && readyToDash)
            Dash();
    }
    void Jump(){
        nOj--;
        rb.AddForce(transform.up * ps.jumpForce, ForceMode.Impulse);        
    }    
    void playerInput(){
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        Debug.Log(Mathf.Abs(horizontalMovement));
        Debug.Log(Mathf.Abs(verticalMovement));
        
    }
    void controlDrag(){
        if (readyToMove) {
            if (isGrounded)
                rb.drag = groundDrag;
            else
                rb.drag = airDrag;
        }
        
    }
    private void FixedUpdate(){if(readyToMove)movePlayer();}
    void Dash() 
    {        
        Invoke("ResetDash", ps.dashCoolDown);
        readyToDash = false;
        if (!isGrounded) 
        {
            readyToMove = false;
            Invoke("ResetMove", 0.2f);
            rb.drag = groundDrag;
        }
        FindObjectOfType<AudioManager>().Play("DashSound");
        rb.AddForce(moveDirection.normalized * (ps.dashSpeed), ForceMode.Impulse);
    }
    void ResetDash() { readyToDash = true; }
    void ResetMove() { rb.drag = airDrag; readyToMove = true; }
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