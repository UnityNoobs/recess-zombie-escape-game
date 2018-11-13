using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {

    public float Health = 100f;


	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DealDamage(float amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            //Enemy Death Trigger + Death Animation
        }
    }
}
