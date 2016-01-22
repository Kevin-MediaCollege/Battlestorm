using UnityEngine;
using System.Collections;

public class EnemyTNT:Enemy {

	public Texture[] textures; // Textures used for being Slowed/Burned/Normal.

	public bool notnormal; // Check if enemy isnt burning or being slowed.

	public GameObject enemyRenders; // Renderers withing the Enemy.

	public override void OnTargetReached() {
		Destroy(gameObject);
		
		PlayerData.Instance.health -= 2;
		
		if(PlayerData.Instance.health <= 1){
			Application.LoadLevel(Application.loadedLevel);
		}

	}

	void FixedUpdate(){
		
		if(isOnFire || isSlowDown){
			//Apply new Texture to Enemy.
			notnormal = true;
			Texture appliedTexture = null;
			
			if(isOnFire){ 
				appliedTexture = textures[1];
			}else if(isSlowDown){
				appliedTexture = textures[2];
			}
			
			enemyRenders.GetComponent<Renderer>().material.mainTexture = appliedTexture;
			
		}else if (!notnormal){
			Texture appliedTexture;
			appliedTexture = textures[0];
			GetComponent<Renderer>().material.mainTexture = appliedTexture;
			enemyRenders.GetComponent<Renderer>().material.mainTexture = appliedTexture;
			
		}
		
	}

}
