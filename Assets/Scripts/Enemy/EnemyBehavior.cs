using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Life")]
    public float health = 100f;

    [Header("Respwan")]
    public string enemyNestTag;

  
    private Rigidbody2D rb;
    private EnemyState state;
    private EnemyRespawn respawnNest;

    private float enemyHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<EnemyState>();
        respawnNest = GameObject.FindGameObjectWithTag(enemyNestTag).GetComponent<EnemyRespawn>();

        // Cache initial health
        enemyHealth = health;
        
    }

    public void Start()
    {
        state.SetState("follow");
    }

    public void HandleImpact(float impactDirection, float impactForce, float impactDamage)
    {
        if (!state.CompareState("dead"))
        {
            StartCoroutine(DisasbleMovement(1.25f, impactDirection, impactForce));
            HandleDamage(impactDamage);
        }
    }

    public void Restart()
    {
        // Restore health
        health = enemyHealth;
        // Revive enemy
        state.isDead = false;
        // Start moving...
        state.SetState("follow");
    }

    private void HandleDamage(float amount)
    {
        health -= amount;
        if (health <= 0) TriggerDeath();
    }

    private void TriggerDeath()
    {
        StartCoroutine(DisableEnemy());
    }

    private void PushBack(float pushDirection, float force)
    {
        rb.AddForce(Vector2.right * pushDirection * force, ForceMode2D.Impulse);
    }

    private IEnumerator DisasbleMovement(float waitTime, float impactDirection, float impactForce)
    {
        // Push enemy on bullet direction
        state.SetState("freeze");
        PushBack(impactDirection, impactForce);
        // Wait some time before enable the enemy movement
        yield return new WaitForSeconds(waitTime);
        state.SetState("follow");
        // Exit courutine
        yield return null;
    }

    private IEnumerator DisableEnemy()
    {
        // Push enemy on bullet direction
        state.SetState("dead");
        // Wait some time before destroy the enemy
        yield return new WaitForSeconds(1f);
        respawnNest.Spawn();
        gameObject.SetActive(false);
        // Exit courutine
        yield return null;
    }
}
