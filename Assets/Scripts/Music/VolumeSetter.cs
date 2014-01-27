using UnityEngine;
using System.Collections;

public class VolumeSetter : MonoBehaviour {

	public bool isMusic; // if the Source is Music.

	public bool isSound; // if the Source is Audio.
	
	void Start () {
		//Sets the volume.

		if(isSound){
			audio.volume = VolumeManager.SoundVolume;
		}

		if(isMusic){
			audio.volume = VolumeManager.MusicVolume;
		}

	}

}
