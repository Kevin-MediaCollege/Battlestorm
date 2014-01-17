using UnityEngine;
using System.Collections;

public class EnemyTNT:Enemy {
	public override void OnTargetReached() {
		Destroy(gameObject);
		
		PlayerData.Instance.health -= 2;
		
		if(PlayerData.Instance.health <= 1)
			Application.LoadLevel(Application.loadedLevel);
	}
}
