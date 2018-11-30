using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    // Movement options
    [Header("Movement")]
    public float speed = 20f;
    public float direction = 1f;

    // Raycast options
    [Header("Collision")]
    public float distance = 0.5f;
    public string enemyTag = "Enemy";
    public LayerMask collisionLayer;

    // Force used for the push-back effect of target
    [Range(1f, 100f)]
    public float impactForce = 10f;

    // Damage caused on collision
    public float impactDamage = 10f;

    // Force used for the push-back effect of weapon user
    [Range(1f, 100f)]
    public float pushForce = 10f;

    private EnemyBehavior targetEnemy;
 

    private void Awake()
    {
       
    }

 
    // Update is called once per frame
    void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * direction, distance, collisionLayer);
        // On collision
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag(enemyTag))
            {
                // Get target enemy
                targetEnemy = hit.collider.gameObject.GetComponent<EnemyBehavior>();
                // Handle enemy collision...
                targetEnemy.HandleImpact(direction, impactForce, impactDamage);
            }
            // Handle bullet collision
            DestroyBullet();
        }
        // Bullet movement
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime );
    }
 
    void OnEnable()
    {
       
      
   
        Debug.Log("ACtive");
    }

 

    public void DestroyBullet()
    {
        // Handle particles and effects..
        // Objects pooled cannot be destroyed, only set to inactive.
        // This is because the object is reused.
    
        gameObject.SetActive(false);
    }
}
