using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{

	public GameObject enemyPrefab;
	public GameObject patternEnemyPrefab;
	public GameObject playerPrefab;

	private int spawnTimer;

	// Use this for initialization
	void Start()
	{
		spawnTimer = 0;
		Instantiate(playerPrefab);
		CreateEnemyPattern(-8, 3f, 20, 0.00f);
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

	void CreateEnemyPattern(float x, float y, int count, float variability){
		int xInc = -1;
		for(int i = 0; i < count; i++){

			int rand = (int) Random.Range(0, 2);
			xInc++;

			if(rand == 0){
				y -= 0.5f;
				xInc = 0;
			} 


			Vector3 pos = new Vector3(x + (xInc * 0.5f), y, 0);
			patternEnemyPrefab.transform.position = pos;




			Instantiate(patternEnemyPrefab);

		}
	}
}
