using UnityEngine;
using System.Collections;

public class EnemyTank:Enemy {
	public Texture[] textures;
	public bool notnormal;
	public GameObject[] enemyRenders;
	void FixedUpdate(){
		if(isOnFire || isSlowDown){
			notnormal = true;
			Texture appliedTexture = null;
			if(isOnFire){ 
				appliedTexture = textures[1];
			}else if(isSlowDown){
				appliedTexture = textures[2];
			}
			for (int i = 0; i < enemyRenders.Length; i++){
				enemyRenders[i].renderer.material.mainTexture = appliedTexture;
			}
		}else if (!notnormal){
			Texture appliedTexture;
			appliedTexture = textures[0];
			//renderer.material.mainTexture = appliedTexture;
			for (int i = 0; i < enemyRenders.Length; i++){
				enemyRenders[i].renderer.material.mainTexture = appliedTexture;
			}
		}
	}
}
