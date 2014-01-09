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
		StartCoroutine("quickwait");
	}
	IEnumerator quickwait(){
		yield return new WaitForSeconds(0.05f);
		maxhealth = hitpoints;
		currentSpeed = speed;

	}
	void FixedUpdate () {
		if(hitpoints !=  maxhealth){
			speed = (currentSpeed * 5);
		}
	}
}
