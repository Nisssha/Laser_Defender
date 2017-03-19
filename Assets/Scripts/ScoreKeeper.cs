﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	Text myText;
	
	void Start()
    {
		myText = GetComponent<Text>();
	}
	
    //update and display score
	public void Score (int points)
    {
		score += points;
		myText.text = score.ToString();
	}
	
	public static void Reset()
    {
	 	score = 0;
	}
}
