using UnityEngine;
using System.Collections;

public class Dasher : Enemy {	
	public float maxhealth;
	public float currentSpeed;
	// Update is called once per frame
	void FixedUpdate () {
		if(hitpoints !=  maxhealth){
			speed = (currentSpeed * 5);
		}
	}
}
