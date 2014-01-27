using UnityEngine;
using System.Collections;

public class MusicFlow:MonoBehaviour {
	//Changes the Music when enemies are comming.

	public AudioClip wait; // Waiting AudioClip
	public AudioClip wave; // Wave AudioClip.

	public AudioSource waitSource; // Source of the WaitMusic.
	public AudioSource waveSource; // Source of the WaveMusic.

	void Start () {
		//Get Reference to Music Sources.

		waitSource.volume = VolumeManager.MusicVolume;
		waitSource = transform.FindChild("WaitSource").GetComponent<AudioSource>();
		waveSource = transform.FindChild("WaveSource").GetComponent<AudioSource>();
	}

	public void Wait() {
		//Flows the Music to WaitMusic.
		if(waitSource.volume < VolumeManager.MusicVolume){
		waitSource.volume += 0.006f;
		}

		waveSource.volume -= 0.006f;

	}

	public void Wave() {
		if(waitSource.volume < VolumeManager.MusicVolume){
		waveSource.volume += 0.006f;
		}

		waitSource.volume -= 0.006f;

	}

}
