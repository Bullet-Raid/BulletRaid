using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship {

	private List<Burst> Bursts;
	private int CurrentBurstIndex;
	private int Cooldown;
	private Vector3 Position;
	private Boundary Bounds;


	// Empty Constructor
	public Ship () {
		Bursts = new List<Burst> {new Burst()};
		Cooldown = 1;
		CurrentBurstIndex = 1;
	}


	// Constructor with bursts
	public Ship (List<Burst> bursts) : this() {
		Bursts = bursts;
	}

	// Moves ship and returns resultant position
	public Vector3 Move(Vector3 movementVector) {
		return ((Bounds == null) ?
			(Position + movementVector) :
			(Bounds.MoveClamped(Position, movementVector)));
	}

	public void SetBounds(Vector3 lowerLeft, Vector3 upperRight, float buffer = 0) {
		Bounds = new Boundary(lowerLeft, upperRight, buffer);
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
