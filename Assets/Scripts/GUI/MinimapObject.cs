using UnityEngine;
using System.Collections;

public class MinimapObject:MonoBehaviour {
	public GameObject pointTarget;
	
	void Update() {
		Debug.Log (pointTarget.transform.position);
		Vector3 position = new Vector3(pointTarget.transform.position.x, 50, pointTarget.transform.position.z);

		transform.position = position;
	}
}
