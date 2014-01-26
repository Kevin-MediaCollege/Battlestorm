using UnityEngine;
using System.Collections;

public class EnemyDasher:Enemy {	
	public float rushSpeed;

	public Texture[] textures;
	public bool notnormal;
	public GameObject enemyRenders;
	void FixedUpdate() {
		if(hitpoints <  maxHitpoints){
			if(!isSlowDown) {
				speed = rushSpeed * 2;
				Debug.Log("Rushing");
			}
		}
		if(isOnFire || isSlowDown){
			notnormal = true;
			Texture appliedTexture = null;
			if(isOnFire){ 
				appliedTexture = textures[1];
			}else if(isSlowDown){
				appliedTexture = textures[2];
			}
			enemyRenders.renderer.material.mainTexture = appliedTexture;
		}else if (!notnormal){
			Texture appliedTexture;
			appliedTexture = textures[0];
			renderer.material.mainTexture = appliedTexture;
			enemyRenders.renderer.material.mainTexture = appliedTexture;
		}
	}
}
