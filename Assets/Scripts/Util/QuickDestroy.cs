using UnityEngine;
using System.Collections;

public class QuickDestroy:MonoBehaviour {
	public bool onTime;
	public bool killdirect;
	public float killtime;
	void Start() {
		this.transform.parent = GameObject.FindGameObjectWithTag("ParticleObject").transform;
	}

	void Update() {
		if(killdirect){
			Destroy(gameObject);
		}
		if(onTime){
			Destroy(gameObject,killtime);
		}else{
		if(GetComponent<ParticleSystem>().isStopped)
			Destroy(gameObject);
		}
	}
}
