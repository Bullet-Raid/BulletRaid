using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	private List<Burst> Bursts;
	private int CurrentBurstIndex;
	private int Cooldown;
	private Vector3 Position;
	private Boundary Bounds;


	// Empty Constructor
	public Ship () {
		Bursts = new List<Burst> {new Burst()};
		Cooldown = 1;
		CurrentBurstIndex = 0;
	}

	// Moves ship and returns resultant position
	public void Move(Vector3 movementVector) {
		Position = ((Bounds == null) ?
			(Position + movementVector) :
			(Bounds.MoveClamped(Position, movementVector)));
	}

	public void SetBounds(Boundary newBounds) {
		Bounds = newBounds;
	}

	public Vector3 GetPosition() {
		return Position;
	}

	public void SetPosition(Vector3 newPosition) {
		Position = newPosition;
	}

	public void SetBursts(List<Burst> newBursts) {
		Bursts = newBursts;
	}

	public void SwitchBurst(int i) {
		CurrentBurstIndex = Mathf.Clamp(i, 0, Bursts.Count - 1);
	}

	public void FireBurst(Vector3 direction, GameObject bulletPrefab) {
		Vector3 quaternionDefaultVector = new Vector3(0,1,0);
		Vector3 translatedPosition = direction - Position;
		Vector3 zAxis = new Vector3(0,0,1);

		float middleRay = Vector3.SignedAngle(
			quaternionDefaultVector,
			translatedPosition,
			zAxis
		);

		float bulletRotation;
		Quaternion bulletDirection;
		Vector3 radiusAddition;
		Burst currentBurst = Bursts[CurrentBurstIndex];
		for(int i = 0; i < currentBurst.shots.Count; i++) {
			bulletRotation = middleRay + currentBurst.shots[i];
			bulletDirection = Quaternion.Euler(0, 0, bulletRotation);
			radiusAddition = bulletDirection * (new Vector3(0, 0.1f, 0));
			Instantiate(bulletPrefab, radiusAddition + Position, bulletDirection);
		}
		Cooldown = currentBurst.cooldown;
	}

	public void DecrementCooldown() {
		Cooldown = Mathf.Clamp(Cooldown - 1, 0, 1000);
	}

	public bool ReadyToFire() {
		return Cooldown == 0;
	}

	// public void Respawn() {
	// 	SetPosition(Bounds.RandomLocation());
	// }


}


// radiusAddition = direction * (new Vector3(0, 0.1f, 0));
