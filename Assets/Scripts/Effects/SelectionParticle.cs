using UnityEngine;
using System.Collections;

public class SelectionParticle:MonoBehaviour {
	public ParticleSystem particleobj;
	public Light lightobj;

	void Start() {
		particleobj = transform.Find("Particle").particleSystem;
		lightobj = transform.parent.light;
	}

	void FixedUpdate() {
		transform.Rotate(new Vector3(0, 2.4f, 0));
	}
}
