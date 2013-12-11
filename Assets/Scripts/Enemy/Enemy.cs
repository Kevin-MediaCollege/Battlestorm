using UnityEngine;
using System.Collections;

public class Enemy:MonoBehaviour {	
	public float hitpoints;

	public int goldReward;

	public bool canStun;

	public void Damage(float amt) {
		hitpoints -= amt;

		if(hitpoints <= 0) {
			Kill();
		}
	}

	public void Kill() {
		PlayerData.goldAmount += goldReward;

		Destroy(gameObject);
	}
}
