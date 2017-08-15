using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary {

	private Vector3 LowerLeft;
	private Vector3 UpperRight;

	public Boundary(Vector3 lowerLeft, Vector3 upperRight, float buffer = 0) {
		Vector3 bufferVector = new Vector3(buffer, buffer, 0);
		LowerLeft = lowerLeft + bufferVector;
		UpperRight = upperRight - bufferVector;
	}

	public Vector3 MoveClamped(Vector3 start, Vector3 movement) {
		return new Vector3(
			Mathf.Clamp(start.x + movement.x, LowerLeft.x, UpperRight.x),
			Mathf.Clamp(start.y + movement.y, LowerLeft.y, UpperRight.y),
			0);
	}

	public bool Exceeds(Vector3 v) {
		return (
			v.x > UpperRight.x ||
			v.y > UpperRight.y ||
			v.x < LowerLeft.x ||
			v.y < LowerLeft.y
		);
	}

	public Vector3 RandomLocation() {
		return new Vector3(
			Random.Range(LowerLeft.x, UpperRight.x),
			Random.Range(LowerLeft.y, UpperRight.y));
	}
}
