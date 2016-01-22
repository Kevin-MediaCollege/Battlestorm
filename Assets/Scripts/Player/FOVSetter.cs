using UnityEngine;
using System.Collections;

public class FOVSetter : MonoBehaviour {
	//Atached to a Camera Object. Sets the FOV when changed in Menu.

	void FixedUpdate () {

		if(GetComponent<Camera>().fieldOfView != InputHandler.FOV){
			GetComponent<Camera>().fieldOfView = InputHandler.FOV;
		}

	}

}
