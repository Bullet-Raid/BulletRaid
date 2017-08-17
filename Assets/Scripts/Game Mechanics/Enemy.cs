using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : Ship
{

	public GameObject enemyBulletPrefab;
	private GameObject playerObj;
	private float enemyVelocity;
	private Camera cam;
	private float xMovement;
	private float yMovement;
	private int internalCooldown;

	// Use this for initialization
	void Start()
	{

		playerObj = GameObject.FindGameObjectWithTag("Player");
		xMovement = Random.Range(0f, 1.0f);
		yMovement = Random.Range(0f, 1.0f);
		internalCooldown = 25;
		enemyVelocity = 2f;
		cam = Camera.main;

		SetBounds(new Boundary(
			cam.ScreenToWorldPoint(new Vector3(0, 0, transform.position.z)),
			cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z)),
			0.1f
		));
		SwitchBurst(0);
		RandomizeLocation();
		transform.position = GetPosition();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		MoveEnemy();
		UpdateBehavior();
	}

	// Helper Functions

	void UpdateBehavior()
	{
		if (internalCooldown > 0)
		{
			internalCooldown = Mathf.Clamp(internalCooldown - 1, 0, 1000);
		}
		else
		{
			EnemyShoot();
			xMovement = Random.Range(-20.0f, 20.0f);
			yMovement = Random.Range(-20.0f, 20.0f);
			internalCooldown = 25;
		}
	}
	void MoveEnemy()
	{
		Vector3 movementVector = (new Vector3(xMovement, yMovement, 0));
		movementVector.Normalize();
		Move(movementVector * enemyVelocity * Time.deltaTime);
		transform.position = GetPosition();
		RotateToFace();
	}

	public void RotateToFace()
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, playerObj.transform.position - transform.position);
	}


	void EnemyShoot()
	{
		DecrementCooldown();
		if (ReadyToFire())
		{
			Vector3 playerPos = playerObj.transform.position;
			FireBurst(playerPos, enemyBulletPrefab);
		}
	}

}
