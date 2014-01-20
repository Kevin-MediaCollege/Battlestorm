using UnityEngine;
using System.Collections;

public class VolumeSetter : MonoBehaviour {
	public bool isMusic;
	public bool isSound;
	// Use this for initialization
	void Start () {
		if(isSound){
			audio.volume = VolumeManager.SoundVolume;
		}
		if(isMusic){
			audio.volume = VolumeManager.MusicVolume;
		}
	}
}
