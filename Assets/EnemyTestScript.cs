using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour, EnemyStats {

    public float currentHealth;

    public int Experience
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    void Start()
    {

    }
    public void AttackDamage()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void EnemyStats.Die()
    {
        throw new System.NotImplementedException();
    }
}
