using UnityEngine;
using System.Collections;

public class MusicFlow : MonoBehaviour {
	public AudioClip wait;
	public AudioClip wave;

	public AudioSource waitSource;
	public AudioSource waveSource;

	public bool wavestarted;
	// Use this for initialization
	void Start () {
		waitSource = transform.FindChild("WaitSource").GetComponent<AudioSource>();
		waveSource = transform.FindChild("WaveSource").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(wavestarted){
			waitSource.volume -= 0.006f;
			waveSource.volume += 0.006f;
		}
		else{
			waitSource.volume += 0.006f;
			waveSource.volume -= 0.006f;
		}
	}
}
