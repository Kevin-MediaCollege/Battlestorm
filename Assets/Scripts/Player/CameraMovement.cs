using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	float lookSpeed= 2.0f;
	float moveSpeed= 1.0f;
	
	float rotationX= 0.0f;
	float rotationY= 0.0f;

	float VInput;
	float HInput;
	private bool ignoreRotation;
	void  FixedUpdate (){
		if(ignoreRotation){
			rotationX += Input.GetAxis("Mouse X")*lookSpeed;
			rotationY += Input.GetAxis("Mouse Y")*lookSpeed;
			rotationY = Mathf.Clamp (rotationY, -90, 90);
			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		}
		transform.position += transform.forward*moveSpeed*Input.GetAxis("Vertical");
		transform.position += transform.right*moveSpeed*Input.GetAxis("Horizontal");
		if (Input.GetKey(KeyCode.LeftShift)){
			Screen.lockCursor = false;
			ignoreRotation = false;
		}
		else{
			Screen.lockCursor = true;
			ignoreRotation = true;
		}
	}
}