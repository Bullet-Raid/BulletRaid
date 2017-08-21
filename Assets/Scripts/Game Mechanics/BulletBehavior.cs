using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

	private float bulletVelocity;
	private Camera cam;
	private Boundary bounds;

	// Use this for initialization
	void Start()
	{
		bulletVelocity = 3f;
		cam = Camera.main;
		bounds = new Boundary(
			cam.ScreenToWorldPoint(new Vector3(0, 0, transform.position.z)),
			cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z))
		);
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(new Vector3(0, 1, 0) * bulletVelocity * Time.deltaTime);
		if (bounds.Exceeds(transform.position))
		{
			Destroy(gameObject);
		}
	}

}
