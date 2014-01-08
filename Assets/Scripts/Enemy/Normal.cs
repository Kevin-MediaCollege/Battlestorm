using UnityEngine;
using System.Collections;

public class Normal:Enemy {	
	public override void Start() {
		base.Start();

		maxHitpoints = hitpoints;
		startHasRun = true;
	}
}
