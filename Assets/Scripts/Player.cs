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

    }

    void Update()
    {
        playerMoveKeyboard();
        AnimatePlayer();
        CheckJump();  // Check for jump input
    }

    private void FixedUpdate() {
        if (isJumping) {
            PlayerJump();
            isJumping = false;  // Reset jump flag
        }
    }

    void playerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxis can give the float values!

        transform.position += moveForce * Time.deltaTime * new Vector3(movementX, 0, 0);
    }

    void AnimatePlayer()
    {
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

    void CheckJump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            isGrounded = false ;  // Set flag to indicate player is no longer grounded
            isJumping = true;  // Set jump flag when button is pressed
        }
    }
  
    void PlayerJump() {
        myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = true;
        }
    }
}
