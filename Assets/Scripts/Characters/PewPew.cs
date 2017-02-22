using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PewPew : MonoBehaviour {

    private Camera cam;

    [SerializeField] private GameObject bombPrefab;
    private GameObject bomb;

    void Start() {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI() {
        int size = 24;

        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "o");
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            bomb = Instantiate(bombPrefab) as GameObject;
            bomb.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            bomb.transform.rotation = transform.rotation;
        }
    }
}
