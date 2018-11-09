using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHorde : MonoBehaviour {
    //Speed options for level designers
    [Header("Horde Speed Settings")]
    public float hordeSpeed = 0.5f;
    public float hordeBoostModifier = 1.5f;
    public float hordeBoostDuration = 2000f;
    //Horde options for level designers
    [Header("Horde Options")]
    [Tooltip("Horde will speed up briefly if it 'eats' an enemy")]
    public bool canEatEnemies = true;
    [Tooltip("Will destory level geometry on contact -Needs a level layer-")]
    public bool canDestroyLevel = false;
    [Tooltip("Touching the horde means an instant kill")]
    public bool instantKill = false;

    private Rigidbody2D rb;
    //The Actual Speed is the speed after modifiers are accounted for
    private float hordeActualSpeed;
    private float boostTimer = 0f;
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hordeActualSpeed = hordeSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        //Move Right
        transform.Translate(new Vector3(hordeActualSpeed * Time.deltaTime, 0));
	}
    private IEnumerator Boost()
    {
        boostTimer = hordeBoostDuration;
        while(boostTimer != 0)
        {
            hordeActualSpeed = hordeSpeed * hordeBoostModifier;
            boostTimer--;
            yield return null;
        }
        hordeActualSpeed = hordeSpeed;

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("The Zombie Horde has reached the player ");
            if (instantKill)
            {
                Destroy(collision.gameObject);
            }
            //TODO: Call Game Controller Player Hits Horde behaviour
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("The Zombie Horde eaten an enemy");
            if (canEatEnemies)
            {
                StartCoroutine("Boost");
                Destroy(collision.gameObject);
            }
        }
        //This code needs a level layer to be added.
        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Debug.Log("The Zombie Horde Eats a part of the map");
            if (canDestroyLevel) { Destroy(collision.gameObject); }
        }
        */
    }
}
