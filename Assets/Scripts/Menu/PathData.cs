using UnityEngine;
using System.Collections;

public class PathData:MonoBehaviour {
	//Data of CameraPath in Menu.

	public GameObject pointA; // Starting Position and Rotation of Camera.

	public GameObject pointB; // End Position and Rotation of Camera.

	public Quaternion rotation; // Rotation.

	public float duration; // Duration of the Movement.

}
