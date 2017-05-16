using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject tram;
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

	int totalPoints = 0;

	void Update () {
		if (player == null) {
            player = Instantiate(playerPrefab) as GameObject;
            player.transform.parent = tram.transform;
            player.transform.position = new Vector3(0, 1.25f, -3);
            player.transform.rotation = transform.rotation;
        }
	}

	public void Points(int points) {
		totalPoints += points;
		Debug.Log (totalPoints);
	}
}
