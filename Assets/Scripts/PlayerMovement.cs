using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Platform options")]

    public float runSpeed = 4f;
    public float maxRunSpeed = 10f;
    public float jumpVelocity = 6f;
    public float jumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;

    //[Header("Player controls")]

    //public KeyCode keyJump;
    //public KeyCode keyMoveLeft;
    //public KeyCode keyMoveRight;

    private Rigidbody2D playerRb;
    private bool jump = false;

    // Use this for initialization
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
    }

    private void UpdateJumpVelocity(float multiplier)
    {
        playerRb.velocity += Vector2.up * Physics2D.gravity.y * (multiplier - 1) * Time.deltaTime;
    }

    private void HandleInput()
    {
        float hAxis = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector2.right * runSpeed * hAxis);
        if(Mathf.Abs(playerRb.velocity.x) > maxRunSpeed) { playerRb.velocity = new Vector2(Mathf.Sign(playerRb.velocity.x) * maxRunSpeed, playerRb.velocity.y); }
        // Left movement behavior 
        /*if (Input.GetKey(keyMoveLeft))
        {
            playerRb.AddForce(Vector2.left * runSpeed);
        }

        // Right movement behavior
        if (Input.GetKey(keyMoveRight))
        {
            playerRb.AddForce(Vector2.right * runSpeed);
        }
        */
        // Jump behavior

        if (playerRb.velocity.y == 0 && Input.GetAxis("Jump") !=0)
        {

            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpVelocity);
            jump = true;
        }

        if (playerRb.velocity.y < 0)
        {
            UpdateJumpVelocity(fallMultiplier);
        }
        else if (playerRb.velocity.y > 0 && Input.GetAxis("Jump") == 0)
        {
            UpdateJumpVelocity(jumpMultiplier);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground" && jump)
        {
            Debug.Log("Landed");
            jump = false;
            //if (Input.GetAxis("Horizontal") == 0) { playerRb.velocity = new Vector2(0, 0); }
        }
    }
}
