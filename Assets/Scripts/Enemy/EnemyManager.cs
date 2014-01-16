using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager:MonoBehaviour {
	public Transform[] spawnPositions;

	public Transform end;

	private Transform spawnPosition;
	private Transform parent;

	private GameObject enemy;

	void Start() {
		parent = GameObject.Find("Enemies").GetComponent<Transform>();
	}

	public void SpawnEnemy(string name, float health, float speed, int spawn) {
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

		enemy.GetComponent<PathFollower>().target = end;
		enemy.transform.parent = parent;
	}
}
