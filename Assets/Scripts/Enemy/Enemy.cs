using UnityEngine;
using System.Collections;

public class Enemy:PathFollower {
	public float maxHitpoints;
	public float hitpoints;

	public int goldReward;

	public bool isDead = false;
	public bool canStun;

	public Color colorStart;
	public Color colorEnd;

	public Renderer[] child;

	public Shader alphaShader;

	void LateUpdate(){
		if(isDead) {
			child = GetComponentsInChildren<Renderer>();
			
			for(int i = 0; i < child.Length; i++){	
				child[i].material.shader = alphaShader;
				child[i].material.color = colorEnd;
			}
			
			colorEnd.a -= 0.01f;
			
			if(colorEnd.a <= 0)
				Destroy(gameObject);
		}
	}

	public override void OnTargetReached() {
		Destroy(gameObject);
		
		if(PlayerData.Instance.health-- <= 1)
			Application.LoadLevel(Application.loadedLevel);
	}

	public void Damage(float amt) {
		hitpoints -= amt;
		
		if(hitpoints <= 0) {
			Kill();
		}
	}

	public void Kill() {
		if(!isDead){
			Instantiate(Resources.Load("Particles/EnemyExplosion"), transform.position, transform.rotation);
			PlayerData.Instance.goldAmount += goldReward;
			rigidbody.isKinematic = false;
			rigidbody.useGravity = true;
			rigidbody.AddForce(Vector3.down * 2);
			gameObject.tag = "Untagged";
			isDead = true;
			OnDisable();
		}
	}

	public void Slowdown() {
		speed /= 2;

		StartCoroutine("SlowDownDelay");
	}

	IEnumerator SlowDownDelay() {
		yield return new WaitForSeconds(3);
		
		speed *= 2;
	}
}
