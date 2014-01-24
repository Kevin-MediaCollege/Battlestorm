using UnityEngine;
using System.Collections;

public class EnemyTNT:Enemy {
	public Texture[] textures;
	public bool notnormal;
	public GameObject enemyRenders;
	public override void OnTargetReached() {
		Destroy(gameObject);
		
		PlayerData.Instance.health -= 2;
		
		if(PlayerData.Instance.health <= 1)
			Application.LoadLevel(Application.loadedLevel);
	}

	void FixedUpdate(){
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
