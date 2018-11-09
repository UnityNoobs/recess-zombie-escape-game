using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour {

    [Header("Platform options")]

    public float runSpeed = 4f;
    public float jumpVelocity = 6f;
    public float jumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;

    [Header("Player controls")]

    public KeyCode keyJump;
    public KeyCode keyMoveLeft;
    public KeyCode keyMoveRight;

    private Rigidbody2D playerRb;

    // Use this for initialization
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
    
    private void UpdateJumpVelocity(float multiplier)
    {
        playerRb.velocity += Vector2.up * Physics2D.gravity.y * (multiplier-1) * Time.deltaTime;
    }

    private void PlayerMovement()
    {
        // Left movement behavior 
        if (Input.GetKey(keyMoveLeft))
        {
            transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
        }

        // Right movement behavior
        if (Input.GetKey(keyMoveRight))
        {
            transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
        }
        // Jump behavior
        if(playerRb.velocity.y == 0 && Input.GetKey(keyJump))
        {
          
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpVelocity);
        }
      
        if(playerRb.velocity.y < 0)
        {
            UpdateJumpVelocity(fallMultiplier);
        }
        else if (playerRb.velocity.y > 0 && !Input.GetKey(keyJump))
        {
            UpdateJumpVelocity(jumpMultiplier);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle player collisions
    }
}
