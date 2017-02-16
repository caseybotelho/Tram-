using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookHor : MonoBehaviour {

	public float sens = 9.0f;

	void Start () {
		Rigidbody body = GetComponent<Rigidbody>();
		if (body) {
			body.freezeRotation = true;
		}
	}

	void Update () {
		transform.Rotate (0, Input.GetAxis ("Mouse X") * sens, 0);
	}
}
