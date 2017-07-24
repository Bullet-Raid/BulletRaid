using System.Collections.Generic;
using UnityEngine;

namespace BurstFire {

	public class Burst {
		public List<float> shots;
		public float spread;
		public int cooldown;

		public Burst(int newSize, float newSpread = 45f) {
			shots = MakeSpread(newSize, newSpread);
			cooldown = newSize;
		}

		public ChangeAngle(int index, float newAngle) {
			if (Mathf.Abs(newAngle) > (spread / 2)) {
				throw new System.ArgumentException("Angle exceeds maximum spread");
			}
			shots[i] = newAngle;
		}

		private List<float> MakeSpread(int newSize, float newSpread) {
			List<float> newShots = List<float>(newSize);
			float midway = newSpread / 2;
			float spacing = newSpread / (newSize - 1);
			float currSpread = newSpread;
			for (int i = 0; i < newSize; i++) {
				newShots[i] = currSpread - (i * spacing) - midway
			}
			return newShots;
		}

	}
}
