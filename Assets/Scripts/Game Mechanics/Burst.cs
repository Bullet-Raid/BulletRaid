using System.Collections.Generic;
using UnityEngine;

public class Burst
{
	public List<float> shots;
	private float spread;
	public int cooldown;

	// Constructor
	public Burst(int newSize = 1, float newSpread = 45f)
	{
		shots = MakeSpread(newSize, newSpread);
		cooldown = newSize * 5;
	}

	public void ChangeAngle(int index, float newAngle)
	{
		if (Mathf.Abs(newAngle) > (spread / 2))
		{
			throw new System.ArgumentException("Angle exceeds maximum spread");
		}
		shots[index] = newAngle;
	}

	// Helper functions
	private List<float> MakeSpread(int newSize, float newSpread)
	{
		List<float> newShots = new List<float>(newSize);
		if (newSize == 1)
		{
			newShots.Add(0);
			return newShots;
		}
		float midway = newSpread / 2;
		float spacing = newSpread / (newSize - 1);
		float currSpread = newSpread;
		for (int i = 0; i < newSize; i++)
		{
			newShots.Add(currSpread - (i * spacing) - midway);
		}
		return newShots;
	}

}
