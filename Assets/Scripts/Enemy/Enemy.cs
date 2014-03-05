using UnityEngine;
using System.Collections;

public class Enemy:PathFollower {

	public float maxHitpoints; // Maximum amount of health Enemy has.

	public float hitpoints; // Amount of health Enemy has.

	public float slowDownDelay; // Duration of Slow status.

	public float burnTime; // Duration of Burn status.

	public int burnDamage; // Amount of Damage when burned.

	public int goldReward; // Amount of gold rewarded for killing Enemy.

	public Color colorStart; // Starting color of fade Animation.

	public Color colorEnd; // End color of fade Animation.

	public Renderer[] child; // Renderers atached to the enemy.

	public Shader alphaShader; // The shader used for the Fade Animation.

	public bool isSlowDown; // Check whether Enemy is slowed.

	public bool isOnFire; // Check whether Enemy is on fire.

	[HideInInspector]
	public bool isDead = false; // Check if Enemy is dead.

	public EnemyManager eManager; //Reference to EnemyManager.
	
	public override void Start() {
		base.Start();
	
		maxHitpoints = hitpoints;
	}
	
	void LateUpdate(){

		if(isDead) {

			child = GetComponentsInChildren<Renderer>();
			
			for(int i = 0; i < child.Length; i++){

				child[i].material.shader = alphaShader;
				child[i].material.color = colorEnd;

			}
			
			colorEnd.a -= 0.01f;
			
			if(colorEnd.a <= 0){
				Destroy(gameObject);
				eManager.enemyList.Remove(gameObject);
			}

		}

	}
	
	public override void OnTargetReached() {
		Destroy(gameObject);
		
		if(PlayerData.Instance.health-- <= 1){
			GameManager.EndGame();
		}

	}

	public void Damage(float damage) {
		//Infict damage to Enemy.

		hitpoints -= damage;

		if(hitpoints <= 0) {
			Kill();
		}

	}

	public void Kill() {
		//Destroys the Enemy.

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
		//Slows the Enemy.

		//Removes Fire Status.
		if(isOnFire) {
			isOnFire = false;
			StopCoroutine("BurnDelay");
		}

		isSlowDown = true;

		StartCoroutine("SlowDownDelay");

	}

	public void Burn() {
		//Burns the Enemt.

		//Removes SlowDown Status.
		if(isSlowDown) {
			isSlowDown = false;
			StopCoroutine("SlowDownDelay");
		}

		isOnFire = true;

		StartCoroutine("BurnDelay");

	}

	IEnumerator SlowDownDelay() {
		speed /= 2;

		yield return new WaitForSeconds(slowDownDelay);
		
		speed *= 2;

	}

	IEnumerator BurnDelay() {

		for(int i = 0; i < burnTime; i++) {

			yield return new WaitForSeconds(1);
			
			Damage(burnDamage);

		}

	}

}
