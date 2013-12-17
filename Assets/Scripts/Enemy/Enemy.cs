using UnityEngine;
using System.Collections;

public class Enemy:PathFollower {	
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
		PlayerData.Instance.goldAmount += goldReward;

		Destroy(gameObject);
	}

	public override void OnTargetReached() {
		Destroy(gameObject);

		if(PlayerData.Instance.health-- <= 1) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
