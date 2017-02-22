using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

	float rotSpeed = 5.0f;

	void Update () {
		transform.Rotate (rotSpeed, 0, 0);
	}
}
