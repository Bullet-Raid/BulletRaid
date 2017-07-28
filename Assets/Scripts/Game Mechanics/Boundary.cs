using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

	private Vector3 LowerLeft;
	private Vector3 UpperRight;

	public Boundary(Vector3 lowerLeft, Vector3 upperRight) {
		LowerLeft = lowerLeft;
		UpperRight = upperRight;
	}

	public Vector3 MovementIntersection(Vector3 start, Vector3 movement) {
		if (!Exceeds(start + movement)) {return start + movement;}
	}

	public bool Exceeds(Vector3 v) {
		return (
			v.x < UpperRight.x &&
			v.y < UpperRight.y &&
			v.x > LowerLeft.x &&
			v.x > LowerLeft.y
		);
	}
}
