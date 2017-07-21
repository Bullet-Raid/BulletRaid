using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public GameObject bulletPrefab;
	public Vector2 playerPos;

	private List<GameObject> Bullets = new List<GameObject>();
	private float playerVelocity;
	private float bulletVelocity;
	private Camera cam;

	// Use this for initialization
	void Start () {
		playerVelocity = 0.1f;
		bulletVelocity = 0.3f;
		cam = Camera.main;
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftShift)) {playerVelocity = 0.05f;}
		float yPos = gameObject.transform.position.y + (Input.GetAxis ("Vertical")) * playerVelocity;
		float xPos = gameObject.transform.position.x + (Input.GetAxis ("Horizontal")) * playerVelocity;
		playerPos = new Vector2 (Mathf.Clamp (xPos, -8, 8), Mathf.Clamp (yPos, -4, 4));
		gameObject.transform.position = playerPos;
		playerVelocity = 0.1f;

		if (Input.GetButton("Fire1")) {
			float camDis = cam.transform.position.y - playerPos.y;
			Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(mouse - transform.position, new Vector3(0,0,1)));
			Bullets.Add(bullet);
		}
		for (int i = 0; i < Bullets.Count; i++) {
			GameObject moveBullet = Bullets[i];
			if (moveBullet != null) {
				moveBullet.transform.Translate(new Vector3(0, 1, 0) * bulletVelocity);
				Vector2 bulletScreenPos = cam.WorldToScreenPoint(moveBullet.transform.position);
				if (bulletScreenPos.x > Screen.width || bulletScreenPos.y > Screen.height || bulletScreenPos.y < 0 || bulletScreenPos.x < 0) {
					DestroyObject(moveBullet);
					Bullets.Remove(moveBullet);
				}
			}
		}
	}
}
