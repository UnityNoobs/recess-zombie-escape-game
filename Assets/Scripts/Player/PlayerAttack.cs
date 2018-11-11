using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Player attack")]
    public KeyCode AttackButton;
    public KeyCode switchWeapon;
    public float fireRate;
    public GameObject attackProjectile;
    private float nextFire = 0f;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // TODO: Refactoring
    private void TriggerAttack()
    {
        if (Input.GetKey(AttackButton))
        {
            if (Time.time > nextFire)
            {
                Instantiate(attackProjectile, transform.position, transform.rotation);
            }
            nextFire = Time.time + fireRate;
        }
    }
}
