using UnityEngine;
using System.Collections;

//enemy collider
public class SprinklerCollider : MonoBehaviour {

	public float health = 1000;
	public int points = 0;
	public GameObject dropletPrefab;
	public float shotsPerSecond = 0.8f;
	public int pointValue = 10;
	
	private ScoreKeeper scoreKeeper;
	
	void Start ()
    {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
    //randomize the times when enemy fires
	void Update()
    {
		float probability = shotsPerSecond * Time.deltaTime;
		if (Random.value < probability)
        {
			Fire ();
		}
	}

    //enemy collider events
    void OnTriggerEnter2D (Collider2D coll)
    {
		Missile missile = coll.gameObject.GetComponent<Missile>();
        //if enemy is hit by player missile
        if (missile)
        {
            //reduce enemy health by missile damage, if health after his is lower or equal than 0 destroy enemy
            health -= missile.getDamage();
			missile.Hit ();
			if (health <= 0)
            {
				scoreKeeper.Score(pointValue);
				Destroy(gameObject);
			}
		}
	}
	
    //instantiate the missile in enemy position and add velocity to it
    void Fire ()
    {
		float delay = Random.value *Time.deltaTime;
		GameObject droplet = Instantiate(dropletPrefab, new Vector3(transform.position.x-0.37f,transform.position.y,1), Quaternion.identity) as GameObject;
		droplet.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, -4f, 0);
	}
}
