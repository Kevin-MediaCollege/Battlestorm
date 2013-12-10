using UnityEngine;
using System.Collections;

public class Projectile:MonoBehaviour {
	public float speed;

	private Transform target;

	public Transform Target { set { this.target = target; } get { return target; } }

	void Update() {
		transform.Translate(target.position * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider collider) {
		Debug.Log(collider.name);
	}
}
