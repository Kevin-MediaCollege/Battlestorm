using UnityEngine;
using System.Collections;

public class MinimapObject:MonoBehaviour {
	private GameObject playerPosition;

	void Start() {
		playerPosition = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void Update() {
		Vector3 position = playerPosition.transform.position;

		position.y = 50;
		transform.position = position;
	}
}
