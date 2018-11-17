using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [Header("Player Health")]
    public float health = 100f;
    private float playerHealth;

	// Use this for initialization
	void Awake () {
        // Cache initial health
        playerHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HandleDamage(float amount)
    {
        health -= amount;
        // UI: healthLifeBar.fillAmount = Health / 100f;
        if(health <= 0)
        {
          TriggerDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {    
            // Enemy shoud handle this
        }
    }
    private void TriggerDeath()
    {
        // Player Death Trigger + Animation
        gameObject.SetActive(false);
    }
}
