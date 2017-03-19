using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelMenager : MonoBehaviour {

	Text myText;
	
	void Start() {
		myText = GetComponent<Text>();
	}

	public void LoadLevel (string name){
		Application.LoadLevel (name);
		}
	
	public void QuitFunction () {
		Application.Quit ();
	}
	
	public void LoadNextLevel () {
		Application.LoadLevel (Application.loadedLevel + 1);
	}
}
