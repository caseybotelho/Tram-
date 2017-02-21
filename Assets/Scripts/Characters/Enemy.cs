using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int points = 10; // points enemy is worth

	public float speed = 4.0f; // enemy speed
	public float distance = 10.0f; // max distance travelled

	private Vector3 startPos; // start position of enemy
	private int direction = 1; // direction of movement based on starting movement

	void Start () {
		startPos = transform.position;
	}

	void FixedUpdate () {
		Vector3 currentPos = transform.position;
		Vector3 deltaPos = startPos - currentPos;
		float deltaZ = deltaPos.z;
		if (Mathf.Abs(deltaZ) > distance) { //changes direction when max movement distance is reached
			direction = -direction;
			Turn ();
		}
		transform.Translate (0, 0, speed * Time.deltaTime * direction);
	}

	private void Turn() {
		transform.Rotate (0, 180, 0);
	}
}
