using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float damage = 200f; 

	public float getDamage(){
		return damage;
	}
	
	public void Hit(){
		Destroy (gameObject);
	}
}
