using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public float attackDamage = 100;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            TriggerKillPlayer(collision.gameObject.GetComponent<PlayerHealth>()); //Accesses 
        }
    }

    public void TriggerKillPlayer(PlayerHealth player)
    {
        player.HandleDamage(attackDamage);
    }
}
