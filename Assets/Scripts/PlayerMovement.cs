using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    float speed = 10;
    float jumpForce = 600;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    float checkRadius = 0.1f;
    public int maxJumpCount;

    private Rigidbody rb;
    private bool facingRight = true;
    private bool isJumping = false;
    private float moveDirection;
    private int jumpCount;
    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, checkRadius, groundObjects);
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Inputs
        ProcessInputs();

        //Animate (turn character)
        Animate();

    }

    //
    void FixedUpdate()
    {
        if (isGrounded())
        {
            jumpCount = maxJumpCount;
        }

        Move();
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector3(moveDirection * speed, rb.velocity.y, 0);
        if (isJumping)
        {
            rb.AddForce(new Vector3(rb.velocity.x, jumpForce, rb.velocity.z));
            jumpCount--;
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,0f,0f);
    }

}
