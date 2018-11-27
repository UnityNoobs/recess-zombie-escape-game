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
    public float distanceFollow = 1.4f;

    private float p1Distance; // Distance to Player 1
    private float p2Distance; // Distance to Player 2, if they exist.
    private int indexOfTargetPlayer;
    private Transform target;
    private float targetDirection = 0;
    private Vector3 origin;
    private Vector3 lastTarget;
    private Rigidbody2D rb;
    private EnemyState state;
    private Animator animator;
    private EnemyBehavior enemy;
    private EnemyAttak attack;
    private float initialSpeed;
    private Color debugColor;
    private Vector3 targetPosi;


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
        //Catch if there are no players.
        p1Distance = Vector3.Distance(transform.position, GameManager.instance.players[0].transform.position);

        //If we are in a one player game
        if(GameManager.instance.players.Count == 1)
        {
            //If only 1 player, set distance to that player.
            indexOfTargetPlayer = 0;
        }
        else if (GameManager.instance.players.Count > 1) // Two Players
        {
            //Save the index of the closer player.
            p2Distance = Vector3.Distance(transform.position, GameManager.instance.players[1].transform.position);
            indexOfTargetPlayer = p1Distance < p2Distance ? 0 : 1;
        }
        else // No players?! Something is horribly wrong.
        {
            Debug.Log("There are no players in the players array");
            indexOfTargetPlayer = 2;
        }
        if(indexOfTargetPlayer != 2)
        {
            //Set target to the closer player
            target = GameManager.instance.players[indexOfTargetPlayer].transform;
        }
        else
        {
            //Set target to self. So it shouldn't move.
            target = transform;
        }
        Debug.DrawLine(transform.position, target.position, debugColor);
        debugColor = Color.red;
        //If in attacking distance
        if (Vector3.Distance(transform.position,target.position) < distance )
        {
            debugColor = Color.white;
            attack.TriggerAttack(GameManager.instance.players[indexOfTargetPlayer]);
        }
        //If in follow distance.
        else if (Vector3.Distance(transform.position, target.position) < distanceFollow)
        {
            if (state.CompareState("idle"))
            {
                state.SetState("follow");
            }
            debugColor = Color.green;
        }
        //If too far away to follow
        else
        {
            state.SetState("idle");
        }
        if (state.CompareState("follow"))
        {
            // Ignore player if is jumping or in another platform. Added some range incase the numbers aren't an exact match.
            if (target.position.y -5 <= transform.position.y && target.position.y + 5 >= transform.position.y)
            {
                // Save last target position
                lastTarget = target.position;
                FollowTarget(lastTarget);
            }
            else if (!persistent)
            {
                // Return to origin position
                FollowTarget(origin);
            } else if (persistent)
            {
                FollowTarget(lastTarget);
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
