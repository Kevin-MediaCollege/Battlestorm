using UnityEngine;
using System.Collections;

public class QuickDestroy:MonoBehaviour {

	void Start() {
		this.transform.parent = GameObject.FindGameObjectWithTag("ParticleObject").transform;
	}

	void Update() {
		if(particleSystem.isStopped)
			Destroy(gameObject);
	}
}
