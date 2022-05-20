using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    public PlayerState ps;    
    public int nOj, nOd;
    public bool dashing;

    [Header("Movement")]
    public float airMultiplier = 0.4f;
    float verticalMovement;
    float horizontalMovement;
    Rigidbody rb;
    Vector3 moveDirection;
    bool readyToMove = true;

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
        ps.dashSpeed = 90f;
        ps.dashCoolDown = 2f;
        ps.numOfJump = 0;
        ps.numOfDash = 1;
        nOj = ps.numOfJump;
        nOd = ps.numOfDash;
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
        if ((Mathf.Abs(horizontalMovement) > 0.2 || Mathf.Abs(verticalMovement) > 0.2) && (Input.GetKeyDown(KeyCode.Mouse4) || Input.GetKeyDown(KeyCode.LeftShift)) && nOd > 0)
            Dash();
        if (dashing && !isGrounded) { rb.drag = groundDrag; }
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
        dashing = true;
        nOd--;
        Invoke("ResetDash", ps.dashCoolDown);
        readyToMove = false;
        Invoke("ResetMove", 0.2f);
        if (!isGrounded) 
        {            
            rb.drag = groundDrag;
        }
        FindObjectOfType<AudioManager>().Play("DashSound");
        rb.AddForce(moveDirection.normalized * (ps.dashSpeed), ForceMode.Impulse);
    }
    void ResetDash() { nOd++; }
    void ReCalibrateDash() { nOd = ps.numOfDash; }
    void ResetMove() { rb.drag = airDrag; readyToMove = true; dashing = false; }
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