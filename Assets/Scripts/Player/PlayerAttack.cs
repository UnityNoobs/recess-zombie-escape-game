using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Attack options")]
    public GameObject projectilePrefab;
    public string attackButton = "Attack_p1";

    private GameObject projectile;
    private PlayerMovement player;
    private PlayerINV playerInv;

    // Use this for initialization
    void Awake () {
        player = GetComponent<PlayerMovement>();
        playerInv = gameObject.GetComponent<PlayerINV>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(attackButton))
        {
            if (playerInv.PlayerHasWeapon()) {
                playerInv.getCurrentWeapon().Fire(player.GetDirection(),gameObject.transform);
            }
        }
	}

    // TODO: Refactoring
    /*
    private void TriggerAttack()
    {
        // TODO: Use object poll
        float direction = player.GetDirection();
        projectile = ObjectPool.instance.SpawnFromPool("bullets", gameObject.transform.position, gameObject.transform.rotation);
        projectile.GetComponent<BulletMovement>().direction = direction;
    }
    */
}
