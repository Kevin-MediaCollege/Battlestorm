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

		rigidbody.velocity += transform.forward * speed * Input.GetAxis("Vertical");
		rigidbody.velocity += transform.right * speed * Input.GetAxis("Horizontal");
		rigidbody.velocity += transform.up * (speed / 2) * Input.GetAxis("Move Vertical");

		Vector3 newPosition = transform.position;
		Vector3 newVelocity = rigidbody.velocity;

		if(transform.position.x < minX) {
			newPosition.x = minX;
			newVelocity.x = 0;
		} else if(transform.position.x > maxX) {
			newPosition.x = maxX;
			newVelocity.x = 0;
		}

		if(transform.position.y < minY) {
			newPosition.y = minY;
			newVelocity.y = 0;
		} else if(transform.position.y > maxY) {
			newPosition.y = maxY;
			newVelocity.y = 0;
		}

		if(transform.position.z < minZ) {
			newPosition.z = minZ;
			newVelocity.z = 0;
		} else if(transform.position.z > maxZ) {
			newPosition.z = maxZ;
			newVelocity.z = 0;
		}
		if(!InterfaceGUI.lockmovement){
		transform.position = newPosition;
		rigidbody.velocity = newVelocity * 70 / Time.timeScale;
		
		if(Input.GetMouseButtonDown(1)) {
			Screen.lockCursor = !Screen.lockCursor;
			canRotate = !canRotate;
		}
		}
	}
}