using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    
    private int scorePoints = 0;
    private int cachePoints = 0;
    public Text scoreText;
    public static ScoreSystem instance;


    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {

	}
	

	// Update is called once per frame
	void Update () {
		if(cachePoints != scorePoints)
        {
            cachePoints = scorePoints;
            scoreText.text = (scorePoints < 9 ? "0" : "") + scorePoints;
        }
	}

    public void UpdateScore(int points)
    {
        scorePoints += points;
    }
}
