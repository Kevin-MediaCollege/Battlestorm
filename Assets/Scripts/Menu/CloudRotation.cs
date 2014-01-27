using UnityEngine;
using System.Collections;

public class CloudRotation:MonoBehaviour {
	//Rotation of the CloudSpheres.

	public bool left; // sets rotation of the Sphere.

	void Update() {

		if(!left) {
			transform.Rotate(new Vector3(0, 0.1f, 0));
		} else {
			transform.Rotate(new Vector3(0, -0.1f, 0));
		}

	}

}
