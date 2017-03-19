using UnityEngine;
using System.Collections;

public class Music_Player : MonoBehaviour {

	static Music_Player instance = null;
	 
    //If there exists other instance - destoy object, if not set this as the instance
	void Awake()
    {
		if (instance != null)
        {
			Destroy(gameObject);
		}
		else
        {
		    instance = this;
		    GameObject.DontDestroyOnLoad (gameObject);
		}
	}
}
