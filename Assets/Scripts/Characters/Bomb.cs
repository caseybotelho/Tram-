using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float speed = 20.0f;
        
	void Start () {
        StartCoroutine(Miss());
	}

	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other) {
        GameObject hitObject = other.transform.gameObject;
        Enemy gaspItsTheEnemy = hitObject.GetComponent<Enemy>();
        if (gaspItsTheEnemy) {
            gaspItsTheEnemy.Killed();
        }
    }

    private IEnumerator Miss() {
        yield return new WaitForSeconds(4.0f);

        Destroy(this.gameObject);
    }
}
