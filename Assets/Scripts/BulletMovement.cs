using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletDirection { Left, Right }

public class BulletMovement : MonoBehaviour {
    public float velocity;
    public BulletDirection bulletDirection;
    private Rigidbody2D rb;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }

    // We don't need this...
    public float GetDirection(BulletDirection direction)
    {
        if (direction == BulletDirection.Left)
        {
            return -1f;
        }
        if (direction == BulletDirection.Right)
        {
            return 1f;
        }
        return 0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float direction = GetDirection(bulletDirection);
        rb.AddForce((transform.right * direction) * velocity * Time.deltaTime, ForceMode2D.Impulse);
    }
}
