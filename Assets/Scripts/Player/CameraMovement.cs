using UnityEngine;
using System.Collections;

public class CameraMovement:MonoBehaviour {
	public float sensitivity = 2.0f;
	public float speed = 1.0f;
	public float acceleration = 0.05f;
	public float stopSpeed = 0.05f;

	public int minX;
	public int maxX;

	public int minY;
	public int maxY;

	public int minZ;
	public int maxZ;
	
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

	private float speedX = 0.0f;
	private float speedY = 0.0f;
	private float speedZ = 0.0f;

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

		if(Input.GetKey(InputHandler.right)) {
			if(speedX < 1.0f) {
				int x2 = 1;
				
				if(speedX > 0)
					x2 = 2;
				
				speedX += acceleration * x2;
			}
		} else if(Input.GetKey(InputHandler.left)) {
			if(speedX > -1.0f) {
				int x2 = 1;

				if(speedX > 0)
					x2 = 2;

				speedX -= acceleration * x2;
			}
		} else {
			if(speedX > 0) {
				if(speedX - stopSpeed < 0) {
					speedX = 0;
				} else {
					speedX -= stopSpeed;
				}
			} else if(speedX < 0) {
				if(speedX + stopSpeed > 0) {
					speedX = 0;
				} else {
					speedX += stopSpeed;
				}
			} else {
				speedX = 0;
			}
		}

		if(Input.GetKey(InputHandler.up)) {
			if(speedY < 1.0f)
				speedY += acceleration;
		} else if(Input.GetKey(InputHandler.down)) {
			if(speedY > -1.0f)
				speedY -= acceleration;
		} else {
			if(speedY > 0) {
				if(speedY - stopSpeed < 0) {
					speedY = 0;
				} else {
					speedY -= stopSpeed;
				}
			} else if(speedY < 0) {
				if(speedY + stopSpeed > 0) {
					speedY = 0;
				} else {
					speedY += stopSpeed;
				}
			} else {
				speedY = 0;
			}
		}

		if(Input.GetKey(InputHandler.forward)) {
			if(speedZ < 1.0f)
				speedZ += acceleration;
		} else if(Input.GetKey(InputHandler.back)) {
			if(speedZ > -1.0f)
				speedZ -= acceleration;
		} else {
			if(speedZ > 0) {
				if(speedZ - stopSpeed < 0) {
					speedZ = 0;
				} else {
					speedZ -= stopSpeed;
				}
			} else if(speedZ < 0) {
				if(speedZ + stopSpeed > 0) {
					speedZ = 0;
				} else {
					speedZ += stopSpeed;
				}
			} else {
				speedZ = 0;
			}
		}

		Vector3 velocity = rigidbody.velocity;

		if(speedX != 0) {
			velocity += transform.right * speed * speedX;
		} else {
			velocity.x = 0;
		}

		if(speedY != 0) {
			velocity += transform.up * (speed / 2) * speedY;
		} else {
			velocity.y = 0;
		}

		if(speedZ != 0) {
			velocity += transform.forward * speed * speedZ;
		} else {
			velocity.z = 0;
		}

		rigidbody.velocity = velocity;

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

		if(!InterfaceGUI.lockmovement) {
			transform.position = newPosition;
			rigidbody.velocity = newVelocity * 70 / Time.timeScale;

			Debug.Log (rigidbody.velocity);
		
			if(Input.GetMouseButtonDown(1)) {
				Screen.lockCursor = !Screen.lockCursor;
				canRotate = !canRotate;
			}
		}
	}
}