using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookVert : MonoBehaviour {

	public float sens = 9.0f;

	float minVert = -45.0f;
	float maxVert = 45.0f;

	float rotationX = 0;

	void Update () {
		rotationX -= Input.GetAxis ("Mouse Y") * sens;
		rotationX = Mathf.Clamp (rotationX, minVert, maxVert);

		transform.localEulerAngles = new Vector3 (rotationX, 0, 0);
	}
}
