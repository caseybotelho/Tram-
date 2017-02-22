using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public enum Types { // different enemy types
		backAndForth,
		upAndDown,
		orbiting,
		mob
	}

	public Types enemyType = Types.backAndForth; // default enemy type

	private Rigidbody body; // used for mob enemyType
	public float speed = 3.0f;
	float maxDistance = 1.0f;

	public int points = 10; // points enemy is worth

	public float distance = 20.0f; // max distance travelled
	private Vector3 startPos; // start position of enemy
    public float time; // travel time
    private float maxTime = 2.0f;

    bool alive; // is the enemy alive

	void Start () { // initializing start and end positions
		startPos = transform.position;

        alive = true;
	}

	void Update () {
        if (alive) {
			if (enemyType == Types.backAndForth) { // enemy in a back and forth pattern along z
				Vector3 currentPos = transform.position;
				transform.position = new Vector3 (currentPos.x, currentPos.y, Mathf.Lerp (startPos.z, startPos.z + distance, time)); // enemy movement

				time += 0.5f * Time.deltaTime;

				if (time > maxTime) {
					startPos = transform.position;
					distance = -distance;
					time = 0.0f;
					transform.Rotate (0, 180, 0); // change direction on reaching max distance
				}
			} else if (enemyType == Types.upAndDown) { // enemy in up and down pattern along y
				Vector3 currentPos = transform.position;
				transform.position = new Vector3 (currentPos.x, Mathf.Lerp (startPos.y, startPos.y + distance, time), currentPos.z); // enemy movement

				time += 0.5f * Time.deltaTime;

				if (time > maxTime) {
					startPos = transform.position;
					distance = -distance;
					time = 0.0f;
				}
			} else if (enemyType == Types.orbiting) { // enemy movement along sphere (enemy needs to be attached to rotating sphere)
				float rotSpeed = Random.Range (1.0f, 20.0f); 
				transform.Rotate (0, rotSpeed, 0);
			} else if (enemyType == Types.mob) { // enemy movement in a mob
				if (body == null) {
					FreezeRotation ();
				}
				transform.Translate (0, 0, speed * Time.deltaTime);
				Ray ray = new Ray (transform.position, transform.forward);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) { // changes travel direction if obstacle is in the way
					if (hit.distance < maxDistance) {
						float angle = Random.Range (-110, 110);
						transform.Rotate (0, angle, 0);
					}
				}
			}
        } else {
			transform.Translate(Random.Range(-15, 15) * Time.deltaTime, Random.Range(-15, 15) * Time.deltaTime, Random.Range(-15, 15) * Time.deltaTime); // movement on death
        }
	}

    public void Killed() { // triggers when hit by a bomb (bomb.cs)
        alive = false;
        StartCoroutine(Die());
    }

	void FreezeRotation() { // adds rigidbody to mob enemyType and freezes rotation
		gameObject.AddComponent<Rigidbody> ();
		body = GetComponent<Rigidbody> ();
		body.freezeRotation = true;
	}


    private IEnumerator Die() { // actions on death
        transform.Rotate(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));

        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
    }
}
