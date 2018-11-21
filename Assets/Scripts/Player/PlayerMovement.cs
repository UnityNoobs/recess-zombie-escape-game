using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Platform options")]
    public float runSpeed = 6f;
    public float jumpVelocity = 8f;
    public float jumpMultiplier = 2f;
    public float fallMultiplier = 2.5f;
    public bool facingRight = true;

    [Header("Platform controls")]
    public int playerIndex = 1;
    public string movementInput = "Horizontal_p1";
    public string jumpButton = "Jump_p1";

    [Header("Ground detection")]
    public float groundRadius = 0.15f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D playerRb;
    private SpriteRenderer sprite;
    private bool isGrounded = false;
    private float direction;
    private float jumpInput;
    private PlayerINV playerInv;

    // Use this for initialization
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerInv = GetComponent<PlayerINV>();
        sprite.flipX = !facingRight;
        //Add the player to the list of players
        GameManager.instance.players.Add(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Handle movement
        direction = Input.GetAxis(movementInput);
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
        jumpInput = Input.GetAxis(jumpButton);

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

    public float GetDirection()
    {
        return facingRight ? 1 : -1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Weapon>() != null)
        {
            Weapon newWeapon = collision.GetComponent<Weapon>();
            playerInv.addWeapon(newWeapon);
            collision.gameObject.SetActive(false);
        }
    }
}
