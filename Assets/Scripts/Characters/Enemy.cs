using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int points = 10; // points enemy is worth

	public float distance = 20.0f; // max distance travelled
	private float startPos; // start position of enemy
    private float endPos; // end position

    public float time;
    private float maxTime = 2.0f;

    bool alive; // is the enemy alive

	void Start () { // initializing start and end positions
		startPos = transform.position.z;
        endPos = startPos + distance;

        alive = true;
	}

	void Update () {
        if (alive) {
            transform.position = new Vector3(0, 0, Mathf.Lerp(startPos, endPos, time)); // enemy movement

            time += 0.5f * Time.deltaTime;

            if (time > maxTime) {
                float placeholder = startPos;
                startPos = endPos;
                endPos = placeholder;
                time = 0.0f;
                transform.Rotate(0, 180, 0); // change direction on reaching max distance
            }
        } else {
            transform.Translate(Random.Range(-5, 5) * Time.deltaTime, Random.Range(-5, 5) * Time.deltaTime, Random.Range(-5, 5) * Time.deltaTime);
        }
	}

    public void Killed() { // triggers when hit by a bomb (bomb.cs)
        alive = false;
        StartCoroutine(Die());
    }

    private IEnumerator Die() {
        transform.Rotate(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));

        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
    }
}
