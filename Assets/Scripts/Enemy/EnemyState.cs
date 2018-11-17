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

    // Use this function to switch from states
    public void SetState(string currentState)
    {
        // Add new state handlers here:
        switch (currentState)
        {
            case "dead": HandleDeadState(); break;
            case "freeze": HandleFreezeState(); break;
            case "follow": HandleFollowState(); break;
        }
    }

    // Simplify to arrow expresion "() => value"
    public bool CompareState(string state)
    {
        return currentState == state;
    }

    private void HandleDeadState()
    {
        isDead = true;
        HandleFreezeState();
        currentState = "dead";
    }

    private void HandleFreezeState()
    {
        if (!isDead)
        {
            // Toogle state options
            canMove = false;
            // Select current state
            currentState = "freeze";
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
        }
    }


}

