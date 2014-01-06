using UnityEngine;
using System.Collections;

public class Dasher : Enemy {	
	public float maxhealth;
	public float currentSpeed;
	public virtual void Start () {
		maxhealth = hitpoints;
		currentSpeed = speed;
		startHasRun = true;
		OnEnable();
	}
	void FixedUpdate () {
		if(hitpoints !=  maxhealth){
			speed = (currentSpeed * 5);
		}
	}
}
