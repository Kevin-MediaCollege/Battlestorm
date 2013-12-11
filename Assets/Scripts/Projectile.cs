using UnityEngine;
using System.Collections;

public class Projectile:MonoBehaviour {
	public float speed;

	public Transform target;

	void Update() {
		transform.Translate(target.position * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log(collider.name);
	}
}
