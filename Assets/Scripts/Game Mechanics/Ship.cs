using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private float Speed;
	private List<Burst> Bursts;
	private int CurrentBurstIndex;
	private int cooldown = 1;

	public Vector3 Position;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	Vector3 Move(Vector3 movementVector) {

	}

	void FireBurst(Vector3 direction, GameObject bulletPrefab) {

	}
}
