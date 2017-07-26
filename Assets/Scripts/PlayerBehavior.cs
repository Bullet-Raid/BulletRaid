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
		if (Input.GetKey(KeyCode.LeftShift)) {playerVelocity = 1f;}
		float yPos = (Input.GetAxis ("Vertical"));
		float xPos = (Input.GetAxis ("Horizontal"));
		playerPos = (new Vector3 (xPos, yPos, 0));
		playerPos.Normalize();
		gameObject.transform.position += playerPos * playerVelocity * Time.deltaTime;
		playerVelocity = 2f;

		// Burst switch handling
		for (int i = 1; i < 10; i++) {
			if (Input.GetKeyDown(i.ToString())) {
				currentBurst = new Burst(i);
			}
		}


		// Fire handling
		cooldown = Mathf.Clamp(cooldown - 1, 0 ,1000);
		if (Input.GetButton("Fire1") && cooldown == 0) {
			float camDis = cam.transform.position.z;
			Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
			Bullets.AddRange(FireBurst(transform.position, mouse, currentBurst, ref cooldown));
		}

		// Bullet movement handling
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

	List<GameObject> FireBurst(Vector3 origin, Vector3 target, Burst burst, ref int cd) {

		List<GameObject> retList = new List<GameObject>();
		float rotation;
		for (int i = 0; i < burst.shots.Count; i++) {
			rotation =  Vector3.SignedAngle(new Vector3(0,1,0), new Vector3(target.x - origin.x, target.y - origin.y, 0), new Vector3(0,0,1)) + burst.shots[i];
			retList.Add((GameObject)Instantiate(bulletPrefab, origin, Quaternion.Euler(0,0,rotation)));
		}
		cd = burst.cooldown;
		return retList;
	}
}
