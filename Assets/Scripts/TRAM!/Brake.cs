using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brake : MonoBehaviour {

    private GameObject endPlatform;

    void Start() {
        endPlatform = this.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other) {
        TripControls brake = other.GetComponent<TripControls>(); // checks if object is tram
        if (brake) {
            if (endPlatform) {
                brake.SpeedChange(0); // stops tram if speed gets too low. should mean it reached the end platform
            } else {
                brake.SpeedChange(brake.movSpeed / 3); // decreases speed of tram based on current speed
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
