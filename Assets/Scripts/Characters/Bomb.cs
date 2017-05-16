using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float speed = 20.0f;

	public int points;

	[SerializeField] private GameObject explosionPrefab;
	private GameObject explosion;

	[SerializeField] private GameObject gameController;
	GameController gameScript;
        
	void Start () {
		Physics.IgnoreLayerCollision(10, 9 , true);

        StartCoroutine(Miss());

		points = 0;

		GameController gameScript = gameController.GetComponent<GameController>();
	}

	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other) {
        GameObject hitObject = other.transform.gameObject;
        Enemy gaspTheEnemy = hitObject.GetComponent<Enemy>();
		if (gaspTheEnemy) {
			gaspTheEnemy.Killed ();
			points = gaspTheEnemy.points;
			gameScript.Points (points);
		}

		explosion = Instantiate (explosionPrefab) as GameObject;
		explosion.transform.position = transform.position;
		explosion.transform.rotation = transform.rotation;

		Destroy (this.gameObject);
    }

    private IEnumerator Miss() {
        yield return new WaitForSeconds(2.5f);

		explosion = Instantiate (explosionPrefab) as GameObject;
		explosion.transform.position = transform.position;
		explosion.transform.rotation = transform.rotation;

        Destroy(this.gameObject);
    }
}
