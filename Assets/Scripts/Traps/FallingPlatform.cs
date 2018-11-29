using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //sets the variable as the rigidbody component
    }

    void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.gameObject.tag.Equals("Player"))
        {
        
            Invoke("DropPlatform", 1f); //When the platform detects the player, it invokes the "DropPlatform method". The number indicates the time to invoke.
            Destroy(gameObject, 3f); //This destroys the game object after 3 seconds.
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false; //This turns off the kinematics of the object, causing it to fall.
    }

}
