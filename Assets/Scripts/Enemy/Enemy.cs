using UnityEngine;
using System.Collections;

public class Enemy:MonoBehaviour {	
	public float hitpoints;

	public int goldReward;

	public bool canStun;

	private PlayerData playerData;

	void Start() {
		playerData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerData>();
	}

	public void Damage(float amt) {
		hitpoints -= amt;

		Debug.Log(hitpoints);

		if(hitpoints <= 0) {
			Kill();
		}
	}

	public void Kill() {
		playerData.goldAmount += goldReward;

		Destroy(gameObject);
	}
}
