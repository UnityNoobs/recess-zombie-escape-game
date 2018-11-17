using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttak : MonoBehaviour {
    public float attackDamage = 0.01f;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            TriggerAttack(collision.gameObject.GetComponent<PlayerHealth>());
        }
    }

    public void TriggerAttack(PlayerHealth player)
    {
        //Add animations...
        player.HandleDamage(attackDamage);
    }
}
