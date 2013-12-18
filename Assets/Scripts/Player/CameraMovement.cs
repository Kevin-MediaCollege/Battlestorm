using UnityEngine;
using System.Collections;

public class CameraMovement:MonoBehaviour {
	public float sensitivity = 2.0f;
	public float speed = 1.0f;
	
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	private float VInput;
	private float HInput;

	private bool canRotate;

	void Update() {
		if(canRotate) {
			rotationX += Input.GetAxis("Mouse X") * sensitivity;
			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		}

		transform.position += transform.forward * speed * Input.GetAxis("Vertical");
		transform.position += transform.right * speed * Input.GetAxis("Horizontal");
		transform.position += transform.up * (speed / 4) * Input.GetAxis("Move Vertical");
		if(Input.GetMouseButtonDown(1)) {
			Screen.lockCursor = !Screen.lockCursor;
			canRotate = !canRotate;
		}
	}
}