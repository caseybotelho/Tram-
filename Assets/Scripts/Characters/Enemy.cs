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

	public int points = 10; // points enemy is worth

	// mob enemyType
	private Rigidbody body;
	public float speed = 1.0f; // default movement speed
	float maxDistance = 1.0f; // distance from collider before turning
	TripControls dallas; // stores tram if seen
	bool noticed; // if player was noticed
	int layer; // layermask for noticing only player

	// backAndForth and upAndDown
	public float distance = 20.0f; // max distance travelled
	private Vector3 startPos; // start position of enemy
    public float time; // travel time
    private float maxTime = 2.0f;

    bool alive; // is the enemy alive

	void Start () {
		startPos = transform.position; // enemy's spawn point

        alive = true;

        layer = LayerMask.GetMask("Player");
		noticed = false;
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
                    gameObject.AddComponent<Rigidbody>();
                    body = GetComponent<Rigidbody>();
                    body.freezeRotation = true;
                }
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit tram;
                if (Physics.SphereCast(ray, 0.75f, out tram, 100f, layer)) { // raycast checking for tram/player
                    if (tram.transform.gameObject) {
                        GameObject hitObject = tram.transform.gameObject;
                        dallas = hitObject.GetComponent<TripControls>();
                        if (dallas) { // if player is spotted, look at
                            Vector3 playerPos = tram.transform.GetChild(5).position;
                            transform.LookAt(playerPos);
							StartCoroutine (Run ());
                        }
                    }
                }
                if (dallas == null) {
				    transform.Translate (0, 0, speed * Time.deltaTime);
				    RaycastHit wall;
                    if (Physics.SphereCast(ray, 0.75f, out wall)) { // changes travel direction if obstacle is in the way
                        if (wall.distance < maxDistance) {
                            float angle = Random.Range(-110, 110);
                            transform.Rotate(0, angle, 0);
                        }
                    }
                }
				if (noticed) {
					transform.Translate (Random.Range(-0.01f, 0.01f), 0, speed * Time.deltaTime);
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

    private IEnumerator Die() { // actions on death
        transform.Rotate(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));

        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
	}

	private IEnumerator Run() { // run toward player

		yield return new WaitForSeconds (2.0f);

		speed = 10.0f;

		noticed = true;
	}
}
