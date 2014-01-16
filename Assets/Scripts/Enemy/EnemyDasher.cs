using UnityEngine;
using System.Collections;

public class EnemyDasher:Enemy {	
	public float rushSpeed;

	void FixedUpdate() {
		if(hitpoints <  maxHitpoints){
			speed = rushSpeed;
			Debug.Log("Rushing");
		}
	}
}
