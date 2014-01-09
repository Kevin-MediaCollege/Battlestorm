using UnityEngine;
using System.Collections;

public class MusicFlow:MonoBehaviour {
	public AudioClip wait;
	public AudioClip wave;

	public AudioSource waitSource;
	public AudioSource waveSource;

	void Start () {
		waitSource = transform.FindChild("WaitSource").GetComponent<AudioSource>();
		waveSource = transform.FindChild("WaveSource").GetComponent<AudioSource>();
	}

	public void Wait() {
		Debug.Log ("Wait");
		waitSource.volume += 0.006f;
		waveSource.volume -= 0.006f;
	}

	public void Wave() {
		Debug.Log ("Wave");
		waitSource.volume -= 0.006f;
		waveSource.volume += 0.006f;
	}
}
