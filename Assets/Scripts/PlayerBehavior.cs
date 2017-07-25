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
	private Burst testBurst = new Burst();
	private int cooldown = 1;

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
		playerPos = new Vector3 (Mathf.Clamp (xPos, -8, 8), Mathf.Clamp (yPos, -4, 4), 0);
		gameObject.transform.position = playerPos;
		playerVelocity = 0.1f;

		cooldown = Mathf.Clamp(cooldown - 1, 0 ,1000);
		if (Input.GetButton("Fire1") && cooldown == 0) {
			float camDis = cam.transform.position.z;
			Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));
			Bullets.AddRange(FireBurst(transform.position, mouse, testBurst, ref cooldown));
		}
		for (int i = 0; i < Bullets.Count; i++) {
			GameObject moveBullet = Bullets[i];
			if (moveBullet != null) {
				moveBullet.transform.Translate(new Vector3(0, 1, 0) * bulletVelocity);
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
