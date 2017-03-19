using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScore : MonoBehaviour {

	Text myText;

    //get the score and display it
	void Start ()
    {
		myText = GetComponent<Text>();
		myText.text = "score: " +ScoreKeeper.score.ToString();
	}

}
