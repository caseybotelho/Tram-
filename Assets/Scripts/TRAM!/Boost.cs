using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        TripControls boost = other.GetComponent<TripControls>(); // checks if object is tram
        if (boost) {
            boost.SpeedChange(boost.movSpeed * 3); // increases speed of tram based on current speed
            Destroy(this.gameObject);
        }
    }
}
