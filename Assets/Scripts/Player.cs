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

    private String WALK_ANIMATION = "walk";
    // Start is called before the first frame update

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerMoveKeyboard();
    }

    void playerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal"); // GetAxis can give the float values!

        transform.position += new Vector3(movementX, 0, 0) * Time.deltaTime * moveForce;

        Debug.Log("Movement value : " + transform.position);
    }
}
