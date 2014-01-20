using UnityEngine;
using System.Collections;

public class Enemy:PathFollower {
	public float maxHitpoints;
	public float hitpoints;

	public float slowDownDelay;
	public float burnTime;
	public int burnDamage;

	public int goldReward;
	public bool canStun;

	public Color colorStart;
	public Color colorEnd;

	public Renderer[] child;

	public Shader alphaShader;

	public GUIStyle style;

	private bool isSlowDown;
	private bool isOnFire;

	[HideInInspector]
	public bool isDead = false;
	
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
			
			if(colorEnd.a <= 0)
				Destroy(gameObject);
		}
	}

	void OnGUI() {
		//Vector3 position = transform.position;

		//position.y += 3;

		//GUI.Label(new Rect(position.x, Screen.height + -position.y, 20, 20), hitpoints.ToString(), style);
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
		if(isOnFire) {
			isOnFire = false;
			StopCoroutine("BurnDelay");
		}

		isSlowDown = true;

		StartCoroutine("SlowDownDelay");
	}

	public void Burn() {
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
