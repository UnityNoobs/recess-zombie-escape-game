using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Attack options")]
    public GameObject projectilePrefab;
    public string attackButton = "Attack_p1";

    private GameObject projectile;
    private PlayerMovement player;

    // Use this for initialization
    void Awake () {
        player = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(attackButton))
        {
            TriggerAttack();
        }
	}

    // TODO: Refactoring
    private void TriggerAttack()
    {
        // TODO: Use object poll
        float direction = player.GetDirection();
        projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<BulletMovement>().direction = direction;
    }
}
