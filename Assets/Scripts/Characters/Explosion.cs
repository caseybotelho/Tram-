using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	void Start () {
		StartCoroutine (CleanUp ());
	}
	
	private IEnumerator CleanUp() {
		yield return new WaitForSeconds (0.55f);

		Destroy (this.gameObject);
	}
}
