using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		MovePlayer();
		HandleBurst();
		Shoot();
	}

	// Is triggered when player rigidbody collides with any other rigidbody
	void OnTriggerEnter2D(Collider2D col) {
		Destroy(gameObject);
	}

	// Helper Functions
	void FireBurst(Vector3 origin, Vector3 target) {

		float middleRay = Vector3.SignedAngle(new Vector3(0,1,0), new Vector3(target.x - origin.x, target.y - origin.y, 0), new Vector3(0,0,1));
		float rotation;
		Quaternion direction;
		Vector3 radiusAddition;
		for (int i = 0; i < currentBurst.shots.Count; i++) {
			rotation = middleRay + currentBurst.shots[i];
			direction = Quaternion.Euler(0,0,rotation);
			radiusAddition = direction * (new Vector3(0,0.1f,0));
			Bullets.Add((GameObject)Instantiate(bulletPrefab, origin + radiusAddition, direction));
		}
		cooldown = currentBurst.cooldown;
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


	void OnDestroy() {
		SceneManager.LoadScene("Main Menu");
	}
}
