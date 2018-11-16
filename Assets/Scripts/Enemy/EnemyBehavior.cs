using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBehavior : MonoBehaviour {

    [Header("Life")]
    public float health = 10f;

    private Rigidbody2D rb;
    private EnemyState state;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<EnemyState>();
    }

    public void Start()
    {
        state.SetState("follow");
    }

    public void HandleImpact(float impactDirection, float impactForce)
    {
        if(!state.CompareState("dead"))
        {
            StartCoroutine(DisasbleMovement(1f, impactDirection, impactForce));
            HandleDamage(impactForce);
            Debug.Log("Collision!");
        }
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
        gameObject.SetActive(false);
        // Exit courutine
        yield return null;
    }
}
