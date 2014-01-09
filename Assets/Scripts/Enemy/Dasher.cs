using UnityEngine;
using System.Collections;

public class Dasher:Enemy {	
	public float currentSpeed;

	public override void Start() {
		base.Start();

		maxHitpoints = hitpoints;
		currentSpeed = speed;
		
		StartCoroutine("quickWait");
	}
	
	IEnumerator quickWait(){
		yield return new WaitForSeconds(0.05f);

		maxHitpoints = hitpoints;
		currentSpeed = speed;
	}

	void FixedUpdate() {
		if(hitpoints !=  maxHitpoints){
			speed = (currentSpeed * 5);
		}
	}
}
