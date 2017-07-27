using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	private float bulletVelocity;
	private Camera cam;

	// Use this for initialization
	void Start () {
		bulletVelocity = 3f;
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0, 1, 0) * bulletVelocity * Time.deltaTime);
		Vector3 bulletScreenPos = cam.WorldToScreenPoint(transform.position);
		if (bulletScreenPos.x > Screen.width || bulletScreenPos.y > Screen.height || bulletScreenPos.y < 0 || bulletScreenPos.x < 0) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name != "bullet(Clone)") {
			Destroy(col.gameObject);
		}
	}
}
