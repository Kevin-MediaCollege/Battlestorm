using UnityEngine;
using System.Collections;

public class OptionRotation : MonoBehaviour {
	//Rotation of the Option Menu Camera.

	public float duration = 100.0f; // Duration of the Rotation

	void Update () {
		transform.Rotate (Vector3.down, 360.0f * Mathf.Deg2Rad / duration * Time.deltaTime);
	}

}
