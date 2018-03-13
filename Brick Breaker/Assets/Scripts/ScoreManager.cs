using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    
	private int currentScore;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + currentScore;
	}

	public void AddScore(int pointsToAdd)
	{
		currentScore += pointsToAdd;
	}
}