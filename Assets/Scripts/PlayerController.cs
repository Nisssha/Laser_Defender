using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject missilePrefab;
	public float movementSpeed = 15f;
	private float padding = 0.5f;
	public float health = 100f;
	float xmin;
	float xmax;
	public LevelMenager levelMenager;

	void Start ()
    {
        //reset the score value
		ScoreKeeper.Reset ();
        //set right and leftmost position according to camera view
        float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));	
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distance));
        //add padding to prevent enemy formation to move partly out of camera range
        xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Update ()
    {
        //move the player according to keyboard input
	    if (Input.GetKey(KeyCode.RightArrow))
        {
			transform.position += Vector3.right * movementSpeed * Time.deltaTime;
	    }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
			transform.position += Vector3.left * movementSpeed * Time.deltaTime;
		}
		float newX = (Mathf.Clamp(transform.position.x, xmin, xmax));
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		
        //fire a missile on space down and stop on up
	if (Input.GetKeyDown(KeyCode.Space))
        {
		InvokeRepeating("Fire", 0.00000001f, 0.3f);
		}
		
	if (Input.GetKeyUp(KeyCode.Space))
        {
		CancelInvoke("Fire");
	    }
	}
	
    //player collider events
	void OnTriggerEnter2D (Collider2D coll) {
		
		Droplet droplet = coll.gameObject.GetComponent<Droplet>();
        //if player is hit by enemy missile
		if (droplet)
        {
            //reduce player health by missile damage, if health after his is lower or equal than 0 call next level
			health -= droplet.getDamage();
			droplet.Hit ();
			if (health <= 0)
            {
				Destroy(gameObject);
				levelMenager.LoadNextLevel();
			}
		}
	}

	//instantiate missile at player position and add velocity to it
	void Fire (){
		GameObject missile = Instantiate(missilePrefab, new Vector3(transform.position.x-0.15f,transform.position.y,1), Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, 4f, 0);
	}
	
}
