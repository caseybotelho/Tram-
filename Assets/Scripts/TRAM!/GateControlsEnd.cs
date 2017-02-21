using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControlsEnd : MonoBehaviour {

    float open = -0.24f;
    float gateSpeed = 0.5f;

    bool arrived; // used to detect if the player is on the tram

    GameObject barrier; // front invisible wall

    void Start() {
        arrived = false;
        barrier = transform.GetChild(0).gameObject;
    }
	
	void Update () {
        if (arrived) {
            if (barrier) {
                Destroy(barrier);
            }
            if (transform.position.y > open) {
                transform.Translate(0, -gateSpeed * Time.deltaTime, 0); // closes rear gate
            }
        }
	}

    public void TripOver() {
        StartCoroutine(Lower());
    }

    private IEnumerator Lower() {

        yield return new WaitForSeconds(1);

        arrived = true; 
    }
}
