using UnityEngine;
using System.Collections;

public class ResourceText:MonoBehaviour {
	void Start() {
		transform.localEulerAngles = new Vector3(0, 0, 0);
		Destroy(gameObject, 1.5f);
	}

	void FixedUpdate() {
		// Keep looking at the player
		transform.LookAt(Camera.main.transform);
		transform.Translate(new Vector3(0, 0.02f, 0));
	}
}
