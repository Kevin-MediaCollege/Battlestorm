using UnityEngine;
using System.Collections;

public class MinimapObject:MonoBehaviour {
	//the minimap Point of the player.

	private GameObject playerPosition; // Position of the Player.

	void Start() {
		playerPosition = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void Update() {
		Vector3 position = playerPosition.transform.position;

		position.y = 50;
		transform.position = position;
	}

}
