using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager:MonoBehaviour {

	public Transform[] spawnPositions; // Spawnpositions of the Enemies.

	public Transform end; // End location of Enemies.

	private Transform spawnPosition; // Current SpawnPosition of Enemy.

	private Transform parent; // The Parent of the Enemy. Used for Clean Heirachy.

	private GameObject enemy; // Referenc to Enemy.

	public List<GameObject> enemyList = new List<GameObject>(); // List of Enemies Spawned.

	void Start() {
		//Reference to Enemies object for Clean Heirachy.
		parent = GameObject.Find("Enemies").GetComponent<Transform>();
	}

	public void SpawnEnemy(string name, float health, float speed, int spawn) {
		//Spawns the Enemy.

		switch(spawn){
		case 0:
			spawnPosition = spawnPositions[0];
			break;
		case 1:
			spawnPosition = spawnPositions[1];
			break;
		case 2:
			spawnPosition = spawnPositions[2];
		break;
		case 3:
			spawnPosition = spawnPositions[3];
		break;
		case 4:
			spawnPosition = spawnPositions[4];
		break;
		case 5:
			spawnPosition = spawnPositions[5];
		break;
		}

		enemy = Instantiate(Resources.Load("Prefabs/Enemies/" + name), spawnPosition.position, Quaternion.identity) as GameObject;

		Enemy en = enemy.GetComponent<Enemy>();
		en.hitpoints = health;
		en.speed = speed;
		en.eManager = this;

		enemy.GetComponent<PathFollower>().target = end;
		enemy.transform.parent = parent;

		enemyList.Add(enemy);
	}

}