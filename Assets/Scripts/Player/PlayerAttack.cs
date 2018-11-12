using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Player attack")]
    public GameObject projectilePrefab;
    private GameObject projectile;
    private PlayerMovement player;

    // Use this for initialization
    void Awake () {
        player = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        TriggerAttack();
	}

    // TODO: Refactoring
    private void TriggerAttack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            // TODO: Use object poll
            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<BulletMovement>().direction = player.facingRight ? 1 : -1;
        }
    }
}
