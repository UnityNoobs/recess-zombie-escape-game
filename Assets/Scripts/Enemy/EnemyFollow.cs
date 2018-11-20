using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {
    [Header("Follow options")]
    public string targetName = "Player";
    public bool persistent = false;
    public float speed = 2f;

    [Range(0f, 10f)]
    public float distance = 1.4f;

    private float targetDirection = 0;
    private Vector3 origin;
    private Vector3 lastTarget;
    private Transform target;
    private Rigidbody2D rb;
    private EnemyState state;
    private Animator animator;
    private EnemyBehavior enemy;


    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<EnemyState>();
        enemy = GetComponent<EnemyBehavior>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag(targetName).transform;
        origin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Follow player on axis x
        if (state.CompareState("follow"))
        {
            bool foundTarget = Vector3.Distance(transform.position, target.position) <= distance;
            // Ignore player if is jumping or in another platform
            if (!foundTarget && target.position.y < transform.position.y)
            {
                // Save last target position
                lastTarget = target.position;
                FollowTarget(lastTarget);
            }
            else if (!foundTarget && !persistent && Vector3.Distance(transform.position, origin) > distance)
            {
                // Return to origin position
                FollowTarget(origin);
            } else if (persistent && !foundTarget && Vector3.Distance(transform.position, lastTarget) > distance)
            { 
            
                // Go to last target position
                FollowTarget(lastTarget);
            }

            if((foundTarget || rb.velocity.x == 0) && !state.CompareState("idle"))
            {
                state.SetState("idle");
            }
        }
    }

    public void FollowTarget(Vector3 newPosition)
    {
        targetDirection = GetTargetDirection(newPosition);
        rb.velocity = new Vector2(targetDirection * speed, rb.velocity.y);
        enemy.Flip(targetDirection);
        state.SetState("follow");
    }

    public float GetTargetDirection(Vector3 currentTarget)
    {
        float x = transform.position.x;
        float targetX = currentTarget.x;

        if (x == targetX) return 0f;

        return x < targetX ? 1 : -1;
    }
}
