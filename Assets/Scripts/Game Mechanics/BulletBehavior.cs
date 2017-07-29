using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

	private float bulletVelocity;
	private Camera cam;
	private Boundary boundaries;

	// Use this for initialization
	void Start () {
		bulletVelocity = 3f;
		cam = Camera.main;
		boundaries = new Boundary(
			cam.ScreenToWorldPoint(new Vector3(0,0,transform.position.z)),
			cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z))
		);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0, 1, 0) * bulletVelocity * Time.deltaTime);
		if (boundaries.Exceeds(transform.position)) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name != "bullet(Clone)") {
			Destroy(col.gameObject);
		}
	}
}
