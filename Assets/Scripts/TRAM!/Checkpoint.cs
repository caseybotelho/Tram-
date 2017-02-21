using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        TripControls turning = other.GetComponent<TripControls>();
        if (turning) {
            turning.Turning();
            Destroy(this.gameObject);
        }
    }

}
