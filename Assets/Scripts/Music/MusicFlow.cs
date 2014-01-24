using UnityEngine;
using System.Collections;

public class MusicFlow:MonoBehaviour {
	public AudioClip wait;
	public AudioClip wave;

	public AudioSource waitSource;
	public AudioSource waveSource;

	void Start () {
		waitSource.volume = VolumeManager.MusicVolume;
		waitSource = transform.FindChild("WaitSource").GetComponent<AudioSource>();
		waveSource = transform.FindChild("WaveSource").GetComponent<AudioSource>();
	}

	public void Wait() {
		if(waitSource.volume < VolumeManager.MusicVolume){
		waitSource.volume += 0.006f;
		}
		waveSource.volume -= 0.006f;
	}

	public void Wave() {
		waitSource.volume -= 0.006f;
		if(waitSource.volume < VolumeManager.MusicVolume){
		waveSource.volume += 0.006f;
		}
	}
}
