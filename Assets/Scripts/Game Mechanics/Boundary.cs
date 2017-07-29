using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary {

	private Vector3 LowerLeft;
	private Vector3 UpperRight;

	public Boundary(Vector3 lowerLeft, Vector3 upperRight) {
		LowerLeft = lowerLeft;
		UpperRight = upperRight;
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
}
