using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControlsStart : MonoBehaviour {

    float closed = 0.25f;
    float gateSpeed = 0.5f;

    bool passenger; // used to detect if the player is on the tram

    GameObject barrier; // rear invisible wall

    void Start() {
        passenger = false;
    }
	
	void Update () {
        if (passenger) {
            if (barrier == null) {
                barrier = GameObject.CreatePrimitive(PrimitiveType.Cube);
                barrier.transform.localScale = new Vector3(4, 1, 0.5f);
                MeshRenderer mesh = barrier.GetComponent<MeshRenderer>();
                Destroy(mesh);
            }
            if (transform.position.y < closed) {
                transform.Translate(0, gateSpeed * Time.deltaTime, 0); // closes rear gate
            }
            barrier.transform.position = new Vector3(this.transform.position.x, 1.25f, this.transform.position.z);
            barrier.transform.rotation = this.transform.rotation;
        }
	}

    public void NewPassenger() {
        StartCoroutine(Raise());
    }

    private IEnumerator Raise() {

        yield return new WaitForSeconds(1);

        passenger = true; 
    }
}
