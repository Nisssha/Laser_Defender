using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject sprinklerPrefab;
	private float padding = 5f;
	float xmin;
	float xmax;
	float xmin2;
	float xmax2;
	public float speed = 0.1f;
	public float width = 10f;
	public float height = 5.5f;
	public int direction;
	
	void Start ()
    {
        //set right and leftmost position according to camera view
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));	
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distance));
		
		xmin = leftmost.x;
		xmax = rightmost.x;
		
        //add padding to prevent enemy formation to move partly out of camera range
		xmin2 = leftmost.x + padding;
		xmax2 = rightmost.x - padding;

		SpawnUntilFull();
	}
	
    //draw cubes
	void OnDrawGizmos ()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(new Vector3 (0, 2.2f,0), new Vector3 (width, height, 0));
	}
	
    //check if all the enemies are dead
	bool AllEnemiesAreDead ()
    {
		foreach (Transform childPositionGameObject in transform)
        {
			if (childPositionGameObject.childCount > 0)
            {
				return false;
			}
		}
		return true;	
	}
	
    //check if there is child object - enemy in each of the enemy positions
	Transform NextEmptyPosition()
    {
		foreach (Transform childPositionGameObject in transform)
        {
			if (childPositionGameObject.childCount == 0)
                {
				return childPositionGameObject;
				}
			}
		return null;
	}
	
    //spawn enemy to all of the enemies positions
	void EnemySpawn ()
    {
		foreach (Transform child in transform)
        {
		    GameObject enemy = Instantiate(sprinklerPrefab, child.transform.position, Quaternion.identity) as GameObject;
		    enemy.transform.parent = child;
		}
	}
	
    //if there is an empty position in enemy group - fill it with new enemy
	void SpawnUntilFull ()
    {
		if (NextEmptyPosition())
        {
			Transform freePosition = NextEmptyPosition();
			GameObject enemy = Instantiate(sprinklerPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		
		if (NextEmptyPosition())
        {
			Invoke ("SpawnUntilFull", 1f);
		}
	}
	
	void Update ()
    {
        //set direction of enemy movement
		if (transform.position.x == 0 || transform.position.x <= xmin + width/2){direction = 1;}	
		if (transform.position.x >= xmax - width/2) {direction = 0;}
		
        //move the enemy to the right
		if (direction == 1)
        {
			transform.position += new Vector3 (speed, 0, 0);
			float newX = (Mathf.Clamp(transform.position.x, xmin2, xmax2));
			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		
		}
        //move the enemy to the left
        else if(direction == 0)
        {
			transform.position -= new Vector3 (speed, 0, 0);
			float newX = (Mathf.Clamp(transform.position.x, xmin2, xmax2));
			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		}
		
        //if all enemies are dead create all new enemies
	if (AllEnemiesAreDead())
        {
		    SpawnUntilFull();
	    }
    }
}