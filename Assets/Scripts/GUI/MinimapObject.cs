using UnityEngine;
using System.Collections;

public class MinimapObject:MonoBehaviour {
	public GameObject pointTarget;
	
	void Update() {
		Vector3 position = pointTarget.transform.position;

		position.y = 50;
		transform.position = position;
	}
}
