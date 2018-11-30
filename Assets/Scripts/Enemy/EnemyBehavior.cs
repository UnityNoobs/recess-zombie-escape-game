using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Zombie Stats")]
    public float health = 100f;
    public float damageVariance = 1f;
    public float speedVariance = 1f;
    public float sizeVariance = 1f;
    public int scorePoint = 1;

    [Header("Respwan")]
    public bool facingRight = true;
    public string enemyNestTag;

    private Collider2D box;
    private Rigidbody2D rb;
    private EnemyState state;
    private EnemyRespawn respawnNest;
    private Animator animator;
    private SpriteRenderer sprite;
    private float enemyHealth;
    private float variance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<Collider2D>();
        state = GetComponent<EnemyState>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        respawnNest = GameObject.FindGameObjectWithTag(enemyNestTag).GetComponent<EnemyRespawn>();
        // Cache initial health
        enemyHealth = health;

    }

    public void Start()
    {
        Restart();
    }

    public void HandleImpact(float impactDirection, float impactForce, float impactDamage)
    {
        if (!state.CompareState("dead"))
        {
            StartCoroutine(DisasbleMovement(0.25f, impactDirection, impactForce));
            HandleDamage(impactDamage);
        }
        
    }

    public void Restart()
    {
      StartCoroutine(Init(1f));
    }

    public void Flip(float direction)
    {
        if (direction != 0f)
        {
            facingRight = direction < 0 ? false : true;
            sprite.flipX = !facingRight;
        }
    }

    private void HandleDamage(float amount)
    {
        health -= amount;
        if (health <= 0) TriggerDeath();
    }

    private void TriggerDeath()
    {
        ScoreSystem.instance.UpdateScore(scorePoint);
        StartCoroutine(DisableEnemy());
    }

    private void PushBack(float pushDirection, float force)
    {
        rb.AddForce(Vector2.right * pushDirection * force, ForceMode2D.Impulse);
    }

    private IEnumerator DisasbleMovement(float waitTime, float impactDirection, float impactForce)
    {
        if (state.CompareState("dead")) yield return null;
        // Push enemy on bullet direction
        state.SetState("freeze");
        PushBack(impactDirection, impactForce);
        // Wait some time before enable the enemy movement
        yield return new WaitForSeconds(waitTime);
        state.SetState("follow");
        // Exit courutine
        yield return null;
    }

    private IEnumerator Init(float waitTime)
    {
        variance = GameManager.instance.enemyVariance.variance;
        //Reset and mutate Stats.
        float healthVariance = 1 + Random.Range(0, variance);
        health = enemyHealth * healthVariance;
        Debug.Log("Health Variance is: " + healthVariance);
        speedVariance = speedVariance * (1 + Random.Range(0, variance));
        Debug.LogFormat("Enemy being spawned with health: {0} and speed variance of {1}", health, speedVariance);
        rb.isKinematic = false;
        box.enabled = true;
        health = enemyHealth;
        // Transition
        state.SetState("start");
        // Wait some time before transition
        yield return new WaitForSeconds(waitTime);
        state.SetState("idle");
        // Exit courutine
        yield return null;
    }


    private IEnumerator DisableEnemy()
    {
        // Push enemy on bullet direction
        state.SetState("dead");
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.isKinematic = true;
        box.enabled = false;
        // Wait some time before destroy the enemy
        yield return new WaitForSeconds(1f);
        respawnNest.Spawn();
        gameObject.SetActive(false);
        // Exit courutine
        yield return null;
    }
}
