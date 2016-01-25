using UnityEngine;
using System.Collections;
using DG.Tweening;

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

        waitSource.DOFade(VolumeManager.MusicVolume, 5);
        waveSource.DOFade(0, 5);

    }

	public void Wave() {

        waveSource.DOFade(VolumeManager.MusicVolume, 5);
        waitSource.DOFade(0, 5);

    }

    public void SetPitch (float _value) {

        waveSource.DOPitch(_value, 2);
        waitSource.DOPitch(_value, 2);

    }

}
