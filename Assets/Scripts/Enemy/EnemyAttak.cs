using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttak : MonoBehaviour {
    public float attackDamage = 0.01f;
    private Animator animator;
    private EnemyState state;
    private EnemyBehavior enemy;
    private EnemyFollow follow;
    // Use this for initialization
	void Awake () {
        state = GetComponent<EnemyState>();
        enemy = GetComponent<EnemyBehavior>();
        follow = GetComponent<EnemyFollow>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            TriggerAttack(
                collision.gameObject.GetComponent<PlayerHealth>(),
                collision.gameObject.transform.position
            );
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
          
            state.SetState("follow");
            enemy.Flip(follow.GetTargetDirection(collision.gameObject.transform.position));
            Debug.Log("End");
        }


    }

    public void TriggerAttack(PlayerHealth player, Vector3 playerPosition)
    {
        //Add animations...
        player.HandleDamage(attackDamage);
        if(!state.CompareState("attack"))
        {
            state.SetState("attack");
            //.
            enemy.Flip(follow.GetTargetDirection(playerPosition));
        }
    }
}
