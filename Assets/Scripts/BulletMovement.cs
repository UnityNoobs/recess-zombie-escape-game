﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    
    // Movement options
    public float speed = 20f;
    public float direction = 1f;
    // Force used for the push-back effect
    [Range(1f, 100f)]
    public float impactForce = 10f;

    // Raycast options
    public float distance = 0.5f;
    public string enemyTag = "Enemy";
    public LayerMask collisionLayer;

	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * direction, distance, collisionLayer);
        // On collision
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag(enemyTag))
            {
                // Handle enemy collision...
                // TODO: Simplify this line :(
                hit.collider.gameObject.GetComponent<EnemyBehavior>().HandleImpact(direction, impactForce);
                Debug.Log("Enemy hit!");
                
            }
            // Handle bullet collision
            DestroyBullet();
        }
        // Bullet movement
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime );
    }

    public void DestroyBullet()
    {
        // Handle particles and effects..
        //Destroy(gameObject);
        //Objects pooled cannot be destroyed, only set to inactive.
        //This is because the object is reused.
        gameObject.SetActive(false);
    }
}
