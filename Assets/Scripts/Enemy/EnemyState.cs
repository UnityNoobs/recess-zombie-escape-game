using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    // Don't change this values directly,
    // Instead use state.SetState(newState)
    public bool isDead = false;
    public bool canMove = true;
    public bool canAttack = false;
    public string currentState = "idle";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this function to switch from states
    public void SetState(string currentState)
    {
        // Add new state handlers here:
        switch (currentState)
        {
            case "idle": HandleIdleState(); break;
            case "dead": HandleDeadState(); break;
            case "start": HandleStartState(); break;
            case "attack": HandleAttackState(); break;
            case "freeze": HandleFreezeState(); break;
            case "follow": HandleFollowState(); break;
        }
    }

    // Simplify to arrow expresion "() => value"
    public bool CompareState(string state)
    {
        return currentState == state;
    }

    private void HandleIdleState()
    {
        if(!isDead)
        {
            canMove = true;
            canAttack = true;
            currentState = "idle";
            animator.SetBool("Walk", false);
        }
    }

    private void HandleDeadState()
    {
        // Toggle state options
        isDead = true;
        canAttack = false;
        HandleFreezeState();
        // Select current state
        currentState = "dead";
        // Handle animations
        animator.SetBool("Dead", isDead);
    }

    private void HandleFreezeState()
    {
        if (!isDead)
        {
            // Toogle state options
            canMove = false;
            // Select current state
            currentState = "freeze";
            // Handle animations
            animator.SetBool("Attack", false);
        }
    }

    private void HandleFollowState()
    {
        if (!isDead)
        {
            // Toogle state options
            canMove = true;
            // Select current state
            currentState = "follow";
            // Handle animations
            animator.SetBool("Attack", false);
        }
    }

    // Reset state
    private void HandleStartState()
    {
        // Toogle state options
        isDead = false;
        canMove = false;
        canAttack = false;
        // Select current state
        currentState = "start";
    }

    private void HandleAttackState()
    {
        if(!isDead)
        {
            currentState = "attack";
            animator.SetBool("Attack", canAttack);
        }
    }


}

