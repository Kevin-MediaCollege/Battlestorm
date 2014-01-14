using UnityEngine;
using System.Collections;

public class CameraMovement:MonoBehaviour {
	public float sensitivity = 2.0f;
	public float speed = 1.0f;

	public int minX;
	public int maxX;

	public int minY;
	public int maxY;

	public int minZ;
	public int maxZ;
	
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	private bool canRotate;

	void Update() {
		rigidbody.velocity = Vector3.zero;

		if(canRotate) {
			rotationX += Input.GetAxis("Mouse X") * sensitivity;
			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		}

		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

		transform.position += transform.forward * speed * Input.GetAxis("Vertical");
		transform.position += transform.right * speed * Input.GetAxis("Horizontal");
		transform.position += transform.up * (speed / 4) * Input.GetAxis("Move Vertical");

		Vector3 newPosition = transform.position;

		if(transform.position.x < minX) {
			newPosition.x = minX;
		} else if(transform.position.x > maxX) {
			newPosition.x = maxX;
		}

		if(transform.position.y < minY) {
			newPosition.y = minY;
		} else if(transform.position.y > maxY) {
			newPosition.y = maxY;
		}

		if(transform.position.z < minZ) {
			newPosition.z = minZ;
		} else if(transform.position.z > maxZ) {
			newPosition.z = maxZ;
		}

		transform.position = newPosition;

		if(Input.GetMouseButtonDown(1)) {
			Screen.lockCursor = !Screen.lockCursor;
			canRotate = !canRotate;
		}
	}
}