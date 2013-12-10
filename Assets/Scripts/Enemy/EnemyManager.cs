using UnityEngine;
using System.Collections;

public class EnemyManager:MonoBehaviour {
	public Transform parent;
	public Transform start;
	public Transform end;

	public float spawnDelay;

	private GameObject enemy;

	void Start() {
		StartCoroutine("spawner");
	}

	IEnumerator spawner() {
		while(true) {
			yield return new WaitForSeconds(spawnDelay);

			enemy = Instantiate(Resources.Load("Prefabs/Entity/Enemy"), start.position, Quaternion.identity) as GameObject;
			enemy.GetComponent<PathFollower>().target = end;
			enemy.transform.parent = parent;
		}
	}
}
