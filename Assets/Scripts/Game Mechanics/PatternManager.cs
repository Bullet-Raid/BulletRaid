using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour {

	GameObject enemyPrefab;

	public PatternManager(GameObject enemyPrefabConstructor){
		enemyPrefab = enemyPrefabConstructor;
	}


	void Start () {


	}


	void CreateEnemyPattern(int x, int y, int count, float variability){
		for(int i = 0; i < count; i++){
			
		}



	}
	
}