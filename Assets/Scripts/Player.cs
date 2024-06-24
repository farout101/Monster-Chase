using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded = true;  // Flag to indicate if player is grounded
    private readonly string GROUND_TAG = "Ground";

    private readonly string WALK_ANIMATION = "walk";
    private bool isJumping = false;  // Flag to indicate jump input

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Initialization code if needed
    }

    void Update()
    {
        // Handle input detection
        playerMoveKeyboard();
        AnimatePlayer();
        CheckJump();  // Check for jump input
    }

    private void FixedUpdate()
    {
        // Handle physics-based movement and jumping
        MovePlayer();

        if (isJumping)
        {
            PlayerJump();
            isJumping = false;  // Reset jump flag
        }
    }

    void playerMoveKeyboard()
    {
        // Detect horizontal movement input
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxisRaw for immediate input
        Debug.Log("MovementX: " + movementX); // Debug movement input
    }

    void MovePlayer()
    {
        // Apply movement force
        Vector3 movement = new Vector3(movementX, 0, 0);
        transform.position += moveForce * Time.deltaTime * movement;
        Debug.Log("New Position: " + transform.position); // Debug new position
    }

    void AnimatePlayer()
    {
        // Handle sprite animation based on movement direction
        if (movementX > 0)
        {
            sr.flipX = false;
            anim.SetBool(WALK_ANIMATION, true);
        }
        else if (movementX < 0)
        {
            sr.flipX = true;
            anim.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void CheckJump()
    {
        // Check if the jump button is pressed and the player is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;  // Set flag to indicate player is no longer grounded
            isJumping = true;  // Set jump flag when button is pressed
        }
    }

    void PlayerJump()
    {
        // Apply jump force
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        // Check if the player collides with ground
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            Debug.Log("Landed on Ground"); // Debug ground landing
        }
    }
}
