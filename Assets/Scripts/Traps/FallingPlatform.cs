using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    Rigidbody2D rb;

    public float dropTime = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //sets the variable as the rigidbody component
    }

    void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.gameObject.tag.Equals("Player"))
        {
            PlatformManager.Instance.StartCoroutine("spawnPlatform", new Vector2(transform.position.x, transform.position.y)); //Calls on the PlatformManager to instantiate a new platform. It passes it's own transform.
            Invoke("DropPlatform", 1f); //When the platform detects the player, it invokes the "DropPlatform method". The number indicates the time to invoke.
            Destroy(gameObject, dropTime); //This destroys the game object after 3 seconds.
        }
    }

    void DropPlatform()
    {
        rb.isKinematic = false; //This turns off the kinematics of the object, causing it to fall.
    }

}
