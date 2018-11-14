using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [Header("Player Health")]
    public float Health = 100f;
    public Image healthLifeBar;


	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(float amount)
    {
        Health -= amount;
        healthLifeBar.fillAmount = Health / 100f;
        if(Health <= 0)
        {
            Die();
          
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            TakeDamage(20f);
        }
    }
    private void Die()
    {
        // Player Death Trigger + Animation
    }
}
