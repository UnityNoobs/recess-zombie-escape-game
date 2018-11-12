﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Platform options")]
    public float runSpeed = 6f;
    public float jumpVelocity = 8f;
    public float jumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;
    public bool facingRight = true;

    [Header("Platform options")]
    public float groundRadius = 0.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D playerRb;
    private SpriteRenderer sprite;
    private bool isGrounded = false;
    private float direction;
    private float jumpInput;

    // Use this for initialization
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = !facingRight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Handle movement
        direction = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(direction * runSpeed, playerRb.velocity.y);

        // Handle jumping
        HandleJump();

        // Handle sprite flip
        if(facingRight && direction < 0 || !facingRight && direction > 0)
        {
            Flip();
        }
    }

    private void UpdateJumpVelocity(float multiplier)
    {
        playerRb.velocity += Vector2.up * Physics2D.gravity.y * (multiplier - 1) * Time.deltaTime;
    }

    private void HandleJump()
    {
        jumpInput = Input.GetAxis("Jump");

        if (playerRb.velocity.y == 0 && jumpInput != 0 && isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpVelocity);
        }
        if (playerRb.velocity.y < 0)
        {
            UpdateJumpVelocity(fallMultiplier);
        }
        else if (playerRb.velocity.y > 0 && jumpInput == 0)
        {
            UpdateJumpVelocity(jumpMultiplier);
        }
    }

    private void Flip()
    {
        facingRight = sprite.flipX;
        sprite.flipX = !facingRight;
    }
}
