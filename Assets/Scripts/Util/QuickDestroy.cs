using UnityEngine;
using System.Collections;

/** This script is mainly used to destroy particles and sounds */
public class QuickDestroy:MonoBehaviour {
	public bool onTime;
	public bool killDirect;

	public float killTime;

	void Start() {
		this.transform.parent = GameObject.FindGameObjectWithTag("ParticleObject").transform;
	}

	void Update() {
		if(killDirect)
			Destroy(gameObject);

		if(onTime) {
			Destroy(gameObject, killTime);
		} else {
			if(particleSystem.isStopped)
				Destroy(gameObject);
		}
	}
}
