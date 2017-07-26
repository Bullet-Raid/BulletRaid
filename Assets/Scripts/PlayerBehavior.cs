using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BurstFire;

public class PlayerBehavior : MonoBehaviour {

	public GameObject bulletPrefab;
	public Vector3 playerPos;

	private List<GameObject> Bullets = new List<GameObject>();
	private float playerVelocity;
	private float bulletVelocity;
	private Camera cam;
	private Burst currentBurst = new Burst();
	private int cooldown = 1;

	// Use this for initialization
	void Start () {
		playerVelocity = 2f;
		bulletVelocity = 3f;
		cam = Camera.main;
	}


	// Update is called once per frame
	void Update () {
		// Player movement handling
		MovePlayer();

		// Burst switch handling
		HandleBurst();


		// Fire handling
		Shoot();

		// Bullet movement handling
		MoveBullets();
	}

	void FireBurst(Vector3 origin, Vector3 target) {

		float rotation;
		for (int i = 0; i < currentBurst.shots.Count; i++) {
			rotation =  Vector3.SignedAngle(new Vector3(0,1,0), new Vector3(target.x - origin.x, target.y - origin.y, 0), new Vector3(0,0,1)) + currentBurst.shots[i];
			Bullets.Add((GameObject)Instantiate(bulletPrefab, origin, Quaternion.Euler(0,0,rotation)));
		}
		cooldown = currentBurst.cooldown;
	}

	void MoveBullets() {
		for (int i = 0; i < Bullets.Count; i++) {
			GameObject moveBullet = Bullets[i];
			if (moveBullet != null) {
				moveBullet.transform.Translate(new Vector3(0, 1, 0) * bulletVelocity * Time.deltaTime);
				Vector3 bulletScreenPos = cam.WorldToScreenPoint(moveBullet.transform.position);
				if (bulletScreenPos.x > Screen.width || bulletScreenPos.y > Screen.height || bulletScreenPos.y < 0 || bulletScreenPos.x < 0) {
					DestroyObject(moveBullet);
					Bullets.Remove(moveBullet);
				}
			}
		}
	}

	void Shoot() {
		cooldown = Mathf.Clamp(cooldown - 1, 0 ,1000);
		if (Input.GetButton("Fire1") && cooldown == 0) {
			float camDis = cam.transform.position.z;
			Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
			FireBurst(transform.position, mouse);
		}
	}

	void MovePlayer() {
		if (Input.GetKey(KeyCode.LeftShift)) {playerVelocity = 1f;}
		float yPos = (Input.GetAxis ("Vertical"));
		float xPos = (Input.GetAxis ("Horizontal"));
		playerPos = (new Vector3 (xPos, yPos, 0));
		playerPos.Normalize();
		gameObject.transform.position += playerPos * playerVelocity * Time.deltaTime;
		playerVelocity = 2f;
	}

	void HandleBurst() {
		for (int i = 1; i < 10; i++) {
			if (Input.GetKeyDown(i.ToString())) {
				currentBurst = new Burst(i);
			}
		}
	}
}
