using UnityEngine;
using System.Collections;

public class FOVSetter : MonoBehaviour {
	void FixedUpdate () {
	if(camera.fieldOfView != InputHandler.FOV){
			camera.fieldOfView = InputHandler.FOV;
		}
	}
}
