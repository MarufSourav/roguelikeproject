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
    public float verticalMovement;
    public float horizontalMovement;
    Rigidbody rb;
    Vector3 moveDirection;
    bool readyToMove = true;
    bool moving;

    [Header("Drag")]
    public float groundDrag;
    public float airDrag = 0.01f;

    [Header("Jump")]    
    public bool isGrounded;

    public bool parryCoolDown;

    private void Start(){
        ps.dashSpeed = ps.moveSpeed;
        ps.gunType = " ";
        ps.moveSpeed = 150f;
        ps.jumpForce = 10f;
        ps.dashSpeed = 120f;
        ps.dashCoolDown = 2f;
        ps.numOfExtraJump = 0;
        ps.numOfDash = 1;
        nOj = ps.numOfExtraJump;
        nOd = ps.numOfDash;
        ps.dashIsParry = false;
        ps.ammoOnParry = false;
        ps.parry = false;
        ps.readyToParry = true;
        parryCoolDown = false;
        ps.parryWindow = 0.1f;
        ps.slowOnParry = false;
        ps.invulnerable = false;
        ps.invulnerableReady = true;
        ps.invulnerableOnInput = false;
        ps.invulnerabilityLength = 2f;
        ps.invulnerabilityCoolDown = 30f;
        GetComponent<BoxCollider>().enabled = false;
        Physics.gravity = new Vector3(0f, -30f, 0f);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1 + 0.1f);
        playerInput();
        controlDrag();        
        if (Input.GetKeyDown(KeyCode.F) && ps.readyToParry)
            Parry();
        if (Input.GetKeyDown(KeyCode.E) && ps.invulnerableOnInput && ps.invulnerableReady)
            ActivateInvulnerability();
        
        if (nOd > ps.numOfDash)
            ReCalibrateDash();
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
        if ((Input.GetKeyDown(KeyCode.Mouse4) || Input.GetKeyDown(KeyCode.LeftShift)) && nOd > 0)
            Dash();
        if ((Mathf.Abs(horizontalMovement) > 0.0 || Mathf.Abs(verticalMovement) > 0.0))
        {
            if (isGrounded && !dashing)            
            {
                if (!moving)
                    FindObjectOfType<AudioManager>().Play("WalkingSound");
                moving = true;
            }
        }
        else{
            if(moving)
                FindObjectOfType<AudioManager>().Stop("WalkingSound");
            moving=false;
        }
        if (dashing && !isGrounded) { rb.drag = groundDrag; }
    }
    void ActivateInvulnerability() 
    {
        ps.invulnerable = true;
        ps.invulnerableReady = false;
        Invoke("ResetInvulnerability", ps.invulnerabilityLength);
        Invoke("ResetInvulnerabilityCoolDown", ps.invulnerabilityCoolDown);
    }
    void ResetInvulnerability() { ps.invulnerable = false; }
    void ResetInvulnerabilityCoolDown() { ps.invulnerableReady = false; }
    void Parry() 
    {
        ps.readyToParry = false;
        ps.parry = true;
        parryCoolDown = true;
        GetComponent<BoxCollider>().enabled = true;
        Invoke("ParryEnd", ps.parryWindow);
        Invoke("ParryCoolDownReset", ps.parryCoolDown);
    }
    void ParryCoolDownReset() { ps.readyToParry = true; parryCoolDown = false; }
    void DashParry() 
    {
        ps.parry = true;
        Invoke("ParryEnd", ps.parryWindow);
    }
    void ParryEnd(){GetComponent<BoxCollider>().enabled = false; ps.parry = false; }
    void Jump(){
        nOj--;
        if (isGrounded) 
            rb.AddForce(transform.up * ps.jumpForce, ForceMode.Impulse);
        else
            rb.AddForce(transform.up * ps.jumpForce * 1.5f, ForceMode.Impulse);

    }    
    void playerInput(){
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;        
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
        CancelInvoke("ResetMove");
        ps.readyToParry = false;
        if (ps.dashIsParry)
            DashParry();
        FindObjectOfType<AudioManager>().Stop("WalkingSound");
        dashing = true;
        nOd--;
        Invoke("ResetDash", ps.dashCoolDown);
        readyToMove = false;
        Invoke("ResetMove", 0.3f);
        if (!isGrounded)            
            rb.drag = groundDrag;
        FindObjectOfType<AudioManager>().Play("DashSound");
        if ((Mathf.Abs(horizontalMovement) > 0.0 || Mathf.Abs(verticalMovement) > 0.0))
            rb.AddForce(moveDirection.normalized * (ps.dashSpeed), ForceMode.Impulse);
        else
            rb.AddForce(transform.forward * (ps.dashSpeed), ForceMode.Impulse);
    }
    void ResetDash() { nOd++; }
    public void ReCalibrateDash() { 
        CancelInvoke("ResetDash"); 
        nOd = ps.numOfDash;
    }
    void ResetMove() {
        rb.drag = airDrag;
        readyToMove = true;
        dashing = false;
        if (!parryCoolDown)
            ps.readyToParry = true;
    }
    void movePlayer()
    {        
        if (isGrounded) 
        {
            rb.AddForce(moveDirection.normalized * ps.moveSpeed);
            nOj = ps.numOfExtraJump;
        }            
        else            
            rb.AddForce(moveDirection.normalized * ps.moveSpeed * airMultiplier);            
    }
}