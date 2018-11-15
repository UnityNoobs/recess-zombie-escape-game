using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    [Header("Follow options")]
    public float speed = 10f;
    public float distance = 1f;
    public string tagName = "Player";
    
    private float direction = 0f;
    private Transform target;
    private Rigidbody2D rb;
    
    // Flags
    private bool canMove = true;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag(tagName).transform;

	}
	
	// Update is called once per frame
	void Update () {
        // Follow player on axis x
        if(Vector2.Distance(transform.position, target.position) > distance && canMove)
        {
            direction = GetVelocityDirection();
            rb.velocity = Vector2.right * direction * speed * Time.deltaTime;
        }
    }

    public void HandleImpact(float impactDirection, float impactForce)
    {
        canMove = false;
        StartCoroutine(DisasbleMovement(1f, impactDirection, impactForce));

    }

    // Utils

    private void PushBack(float pushDirection, float force)
    {
       rb.AddForce(Vector2.right * pushDirection * force, ForceMode2D.Impulse);
    }

    private IEnumerator DisasbleMovement(float waitTime, float impactDirection, float impactForce)
    {
        canMove = false;
        // Push-back object
        PushBack(impactDirection, impactForce);
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        yield break;
 
    }

    private float GetVelocityDirection()
    {
        float x = transform.position.x;
        float targetX = target.position.x;

        if (x==targetX)
        {
            return 0f;
        } 
        return x < target.position.x ? 1 : -1;
    }
}
