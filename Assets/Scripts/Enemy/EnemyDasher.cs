using UnityEngine;
using System.Collections;

public class EnemyDasher:Enemy {	

	public float rushSpeed;

	public Texture[] textures; // Textures used for being Slowed/Burned/Normal.

	public bool notnormal; // Check if enemy isnt burning or being slowed.

	public GameObject enemyRenders; // Renderers withing the Enemy.

	void FixedUpdate() {

		if(hitpoints <  maxHitpoints){
			if(!isSlowDown) {
				speed = rushSpeed * 2;
				Debug.Log("Rushing");
			}
		}

		if(isOnFire || isSlowDown){
			//Apply new Texture to Enemy.
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
