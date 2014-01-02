using UnityEngine;
using System.Collections;

public class QuickDestroy : MonoBehaviour {
	void Start(){
		this.transform.parent = GameObject.FindGameObjectWithTag("ParticleObject").transform;
	}
	// Used for Particle's
	void Update () {
		if(particleSystem.isStopped){
		Destroy(gameObject);
		}
	}
}
