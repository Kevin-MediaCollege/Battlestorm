using UnityEngine;
using System.Collections;

public class EnemyTank:Enemy {

	public Texture[] textures; // Textures used for being Slowed/Burned/Normal.

	public bool notnormal; // Check if enemy isnt burning or being slowed.

	public GameObject[] enemyRenders; // Renderers withing the Enemy.

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
			
			for(int i = 0; i < enemyRenders.Length; i++){
				enemyRenders[i].GetComponent<Renderer>().material.mainTexture = appliedTexture;
			}
			
		}else if (!notnormal){
			Texture appliedTexture;
			appliedTexture = textures[0];
			GetComponent<Renderer>().material.mainTexture = appliedTexture;

			for(int i = 0; i < enemyRenders.Length; i++){
			enemyRenders[i].GetComponent<Renderer>().material.mainTexture = appliedTexture;
			}

		}

	}

}