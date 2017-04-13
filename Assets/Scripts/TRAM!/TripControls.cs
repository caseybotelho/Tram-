using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripControls : MonoBehaviour {

    public float movSpeed = 2.0f; //speed of the tram
    public float rotSpeed = 48.0f; // rotation speed of the tram

    GameObject passenger; // stores passenger once detected
    bool inProgress; // controls if the tram's journey is in progress

    bool changeDirection; // controls in the tram is turning
    int direction = 1; // which direction the tram is turning

    void Start() {
        inProgress = false;
        changeDirection = false;
    }

	void Update () {
        Ray ray = new Ray(transform.position, transform.up); // detecting passenger from floor
        RaycastHit hit;
        if (passenger == null) {
            if (Physics.SphereCast(ray, 2.0f, out hit)) {
				passenger = hit.transform.GetChild(5).gameObject; // not best way to get playercharacter, but won't be able to solve until i figure out how to maintain movement with tram without making player object a child of the tram
				if (passenger.GetComponent<DallasRex> ()) {
					StartCoroutine (BeginTrip ());
				}
            }
        }
        if (inProgress) { // tram movement
            this.transform.Translate(0, 0, movSpeed * Time.deltaTime);
            if (movSpeed == 0) {
                StartCoroutine(EndTrip());
            }
        }
        if (changeDirection) { // tram rotation
            transform.Rotate(0, rotSpeed * Time.deltaTime * direction, 0);
        }
    }

    public void SpeedChange(float newSpeed) { // updates tram speed when passing through a boost or brake
        movSpeed = newSpeed;
    }

    public void Turning() {
        StartCoroutine(Turn());
    }

    private IEnumerator Turn() { // tram turns for this period of time

        changeDirection = true;

        yield return new WaitForSeconds(1.85f);

        changeDirection = false;
    }

    private IEnumerator BeginTrip() {
        GameObject rearDoor = transform.GetChild(0).gameObject; // gets rear door and closes it
        GateControlsStart closeGate = rearDoor.GetComponent<GateControlsStart>();
        closeGate.NewPassenger();

        yield return new WaitForSeconds(2.5f);

        inProgress = true; // starts trip
    }

    private IEnumerator EndTrip() {
        GameObject frontDoor = transform.GetChild(1).gameObject; // gets front door and opens it
        GateControlsEnd openGate = frontDoor.GetComponent<GateControlsEnd>();
        openGate.TripOver();

        yield return new WaitForSeconds(2.5f);

        inProgress = false; // ends trip
    }
}
