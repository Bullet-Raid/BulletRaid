using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public GameObject enemyPrefab;

	private int spawnTimer;

	// Use this for initialization
	void Start()
	{
		spawnTimer = 0;

	}

	// Update is called once per frame
	void Update()
	{
		if (spawnTimer == 100)
		{
			Debug.Log("Instantiated New Enemy");
			spawnTimer = 0;
			Instantiate(enemyPrefab);
		}
		else
		{

			//Debug.Log("Spawn Timer: " + spawnTimer);
			spawnTimer++;
		}

	}
}
