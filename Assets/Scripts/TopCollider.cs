using UnityEngine;
using System.Collections;

public class TopCollider : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D coll) {
		DestroyObject(coll.gameObject);
	}
	
	void OnTriggerEnter2D (Collider2D coll) {
		DestroyObject(coll.gameObject);
	}
}
