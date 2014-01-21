using UnityEngine;
using System.Collections;

public class QuickDestroy:MonoBehaviour {
	public bool onTime;
	public float killtime;
	void Start() {
		this.transform.parent = GameObject.FindGameObjectWithTag("ParticleObject").transform;
	}

	void Update() {
		if(onTime){
			Destroy(gameObject,killtime);
		}else{
		if(particleSystem.isStopped)
			Destroy(gameObject);
		}
	}
}
