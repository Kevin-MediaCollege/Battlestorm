using UnityEngine;
using System.Collections;

public class MusicFlow:MonoBehaviour {
	public AudioClip wait;
	public AudioClip wave;

	public AudioSource waitSource;
	public AudioSource waveSource;

	public bool waveStarted;
	
	void Start () {
		waitSource = transform.FindChild("WaitSource").GetComponent<AudioSource>();
		waveSource = transform.FindChild("WaveSource").GetComponent<AudioSource>();
	}

	void FixedUpdate() {
		if(waveStarted) {
			waitSource.volume -= 0.006f;
			waveSource.volume += 0.006f;
		} else {
			waitSource.volume += 0.006f;
			waveSource.volume -= 0.006f;
		}
	}
}
