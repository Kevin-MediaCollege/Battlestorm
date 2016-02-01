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
	
	private bool controllerpressed;
	
	void Update() {

		if(canRotate) {
			rotationX += Input.GetAxis("Mouse X") * sensitivity;
			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			rotationY = Mathf.Clamp(rotationY, -90, 90);
			
		}

		transform.rotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		transform.rotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal"), (speed / 4) * Input.GetAxis("Move Vertical"), speed * Input.GetAxis("Vertical")));

		if(Input.GetMouseButtonDown(1)) {

			Screen.lockCursor = !Screen.lockCursor;
			canRotate = !canRotate;

		}

	}
}