using UnityEngine;
using System.Collections;

public class Projectile:MonoBehaviour {
	public Transform target;
	public AudioClip hitSound;

	public float damage;
	public Enemy targetScript;

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
