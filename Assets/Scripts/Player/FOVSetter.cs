using UnityEngine;
using System.Collections;

public class FOVSetter : MonoBehaviour {
	//Atached to a Camera Object. Sets the FOV when changed in Menu.

	void FixedUpdate () {

		if(camera.fieldOfView != InputHandler.FOV){
			camera.fieldOfView = InputHandler.FOV;
		}

	}

}
