using UnityEngine;
using System.Collections;

public class SelectionParticle : MonoBehaviour {
	public ParticleSystem particleobj;
	public Light lightobj;
	// Use this for initialization
	void Start () {
		particleobj = transform.Find("Particle").particleSystem;
		lightobj = transform.parent.light;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(new Vector3(0,2.4f,0));
	}
}
