using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {
    [Header("Follow options")]
    public bool followPlayer = true;
    public bool persistent = false;
    public float speed = 2f;

    [Range(0f, 10f)]
    public float distance = 1.4f;
    [Range(0f, 1000f)]
    public float distanceFollow = 1f;

    /* target */
    private Transform target;
    private float targetDirection = 0;
    private Vector3 origin;
    private Vector3 lastTarget;
    /* Self */
    private Rigidbody2D rb;
    private EnemyState state;
    private Animator animator;
    private EnemyBehavior enemy;
    private EnemyAttak attack;
    private float initialSpeed;
    private Color debugColor;

    private GameObject[] players;
    



    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<EnemyState>();
        enemy = GetComponent<EnemyBehavior>();
        animator = GetComponent<Animator>();
        attack = GetComponent<EnemyAttak>();
        origin = transform.position;
        initialSpeed = speed;
        debugColor = Color.red;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = initialSpeed * enemy.speedVariance;
        players = GameObject.FindGameObjectsWithTag("Player");
        target = players[0].transform;

        Debug.DrawLine(transform.position, target.position, debugColor);
        debugColor = Color.red;

        bool followTarget = Vector3.Distance(transform.position, target.position) < distanceFollow;
        bool foundTarget = Vector3.Distance(transform.position, target.position) <= distance;
        bool foundFakeTarget = Vector3.Distance(transform.position, lastTarget) <= distance;
   
        // Start following target but don't interrupt attack state.
        if (
            followTarget &&
            target.position.y - 1f < transform.position.y &&
            target.position.y + 1f > transform.position.y
          )
        {
            if (state.CompareState("idle"))
            {
                state.SetState("follow");
            }

        } 

        if (state.CompareState("follow"))

        {

            
           
            // Ignore player if is jumping or in another platform
            if (
                followTarget &&
                !foundTarget &&
                target.position.y - 1f < transform.position.y &&
                target.position.y + 1f > transform.position.y
               )
            {
                // Save last target position
                lastTarget = target.position;
                FollowTarget(lastTarget);
              

            }

           
            else if(!followTarget || foundFakeTarget || foundTarget)
            {
                state.SetState("idle");

                // Debug
            }
          

        } 
       
    }

    public void FollowTarget(Vector3 newPosition)
    {
        targetDirection = GetTargetDirection(newPosition);
        rb.velocity = new Vector2(targetDirection * speed, rb.velocity.y);
        enemy.Flip(targetDirection);
        state.SetState("follow");
        animator.SetBool("Walk", targetDirection == 0f ? false : true);
        debugColor = Color.green;
    }


    public float GetTargetDirection(Vector3 currentTarget)
    {
        float x = transform.position.x;
        float targetX = currentTarget.x;

        if (x == targetX) return 0f;

        return x < targetX ? 1f : -1f;
    }
}
