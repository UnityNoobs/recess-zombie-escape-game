using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Player attack")]
    public GameObject projectilePrefab;
    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private float attackTime = 0.0F;

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
        attackTime = attackTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && attackTime > nextFire)
        {
            nextFire = attackTime + fireDelta;
            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<BulletMovement>().direction = player.facingRight ? -1 : 1;


            // create code here that animates the newProjectile
            nextFire = nextFire - attackTime;
            attackTime = 0.0F;
        }
    }
}
