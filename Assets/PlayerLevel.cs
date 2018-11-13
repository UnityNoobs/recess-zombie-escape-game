using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {
    public int Level { get; set; }
    public int currentExperience { get; set; }
    public int requiredExperience { get { return Level * 25; } }

	// Use this for initialization
	void Start () {
        Level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnemyToExperience(EnemyStats enemy)
    {
        GrantExperience(enemy.Experience);
    }
    
    public void GrantExperience(int amount)
    {
        currentExperience += amount;
        while(currentExperience >= requiredExperience)
        {
            currentExperience -= requiredExperience;
            Level++;
        }
    }
}
