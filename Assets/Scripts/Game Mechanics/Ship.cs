using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship {

	private float Speed;
	private List<Burst> Bursts;
	private int CurrentBurstIndex;
	private int cooldown = 1;
	private Vector3 Position;


	// Empty Constructor
	public Ship () {
		Bursts = new List<Burst> {new Burst()};
	}


	// Constructor with bursts
	public Ship (List<Burst> bursts) {
		Bursts = bursts;
	}

	// Moves ship and returns resultant position
	public Vector3 Move(Vector3 movementVector) {
		return Position;
	}

	public Vector3 GetPosition() {
		return Position;
	}

	public void SwitchBurst(int i) {
		CurrentBurstIndex = Mathf.Clamp(i, 0, Bursts.Count - 1);
	}

	public void FireBurst(Vector3 direction, GameObject bulletPrefab) {

	}
}
