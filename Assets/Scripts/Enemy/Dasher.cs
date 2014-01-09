using UnityEngine;
using System.Collections;

public class Dasher:Enemy {	
	public float currentSpeed;

	public override void Start() {
		base.Start();

		maxHitpoints = hitpoints;
		currentSpeed = speed;
		
		StartCoroutine("quickwait");
	}
	
	IEnumerator quickwait(){
		yield return new WaitForSeconds(0.05f);
		maxhealth = hitpoints;
		currentSpeed = speed;
	}

	void FixedUpdate() {
		if(hitpoints !=  maxHitpoints){
			speed = (currentSpeed * 5);
		}
	}
}
