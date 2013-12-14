using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager:MonoBehaviour {
	public Transform parent;
	public Transform start;
	public Transform end;
	
	public float spawnDelay;
	
	private GameObject enemy;
	
	public GameObject[] enemies;
	public List<GameObject> enemyList;
	void Start() {
		enemyList = new List<GameObject>();
	}
	void FixedUpdate(){
		for(int i = 0; i < enemyList.Count; i++){
			if(enemyList[i].gameObject == null){
				enemyList.RemoveAt(i);
			}
		}
	}
	public void spawnEnemy() {
		enemy = Instantiate(Resources.Load("Prefabs/Entity/Enemy"), start.position, Quaternion.identity) as GameObject;
		enemy.GetComponent<PathFollower>().target = end;
		enemy.transform.parent = parent;
		enemyList.Add(enemy.transform.gameObject);
	}
}
