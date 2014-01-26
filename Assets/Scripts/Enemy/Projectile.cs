using UnityEngine;
using System.Collections;

public class Projectile:MonoBehaviour {

	public Transform target; //Target of the Projectile.

	public AudioClip hitSound; // Sound of the Projectile hitting the enemy.

	public float damage; // Amount of damage the Projectile gives to Enemy.

	public Enemy targetScript; // Target of Projectile.

	void FixedUpdate() {

		if(target != null) {
			transform.LookAt(target.transform.position);
			transform.Translate(Vector3.forward);
			renderer.enabled = true;
		} else {
			renderer.enabled = false;
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter(Collider coll) {
		Instantiate(Resources.Load("Particles/Arrowhit"), transform.position, transform.rotation);
	}

	void OnTriggerStay(Collider coll) {
		targetScript.Damage(damage);
		Destroy(gameObject);
	}

	void OnDestroy() {
		audio.PlayOneShot(hitSound);
	}

}
