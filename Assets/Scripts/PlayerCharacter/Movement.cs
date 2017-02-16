using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Controls/FPS Movement")]
public class Movement : MonoBehaviour {

	public float speed = 6.0f;
	float gravity = -9.8f;

	private CharacterController player;

	void Start () {
		player = GetComponent<CharacterController> ();
	}

	void Update () {
		float movX = Input.GetAxis ("Horizontal") * speed;
		float movZ = Input.GetAxis ("Vertical") * speed;

		Vector3 mov = new Vector3 (movX, 0, movZ);
		mov = Vector3.ClampMagnitude (mov, speed);
		mov.y = gravity;
		mov *= Time.deltaTime;
		mov = transform.TransformDirection (mov);

		player.Move (mov);
	}
}
