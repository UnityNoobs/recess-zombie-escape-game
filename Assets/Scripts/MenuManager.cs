using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void StartGame()
    {
        // Start game
        // To be decided...
        Debug.Log("Starting game...");
    }

    public void ExitGame()
    {
        Debug.Log("Exit game!");
        Application.Quit();
    }
} 
