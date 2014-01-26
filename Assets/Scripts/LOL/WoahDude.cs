using UnityEngine;
using System.Collections;

public class WoahDude : MonoBehaviour {
	public Camera cam;
	public Light disco;
	public bool zoom;
	public bool hasstarted = false;
	public AudioSource one;
	public AudioSource two;

	public AudioClip badger;
	public AudioClip wavemusic;
	public AudioClip waitmusic;

	public Color ambient;
	public Color lightcol;

	void Start () {
		lightcol = disco.color;
		ambient = RenderSettings.ambientLight;
	}

	void Update () {
		if(Application.isEditor) {
			if(Input.GetKey(KeyCode.F2) && hasstarted){
				StopCoroutine("WoahDisco");
				cam.fieldOfView = 70;
				hasstarted = false;
				two.Stop();
				one.Stop();
				disco.intensity = 0.45f;
				RenderSettings.ambientLight = ambient;
				disco.color = lightcol;
				one.clip = waitmusic;
				two.clip = wavemusic;
				two.Play();
				one.Play();
			}
			if(Input.GetKey(KeyCode.F1) && !hasstarted){
				hasstarted = true;
				two.Stop();
				one.Stop();
				StartCoroutine("WoahDisco");
				disco.intensity = 8;
				one.clip = badger;
				two.clip = badger;
				two.Play();
				one.Play();
			}
		}

		if(hasstarted){
			if(cam.fieldOfView < 70){
				zoom = true;
			}
			if(cam.fieldOfView > 140){
				zoom = false;
			}
			if(zoom){
				cam.fieldOfView += 4;
			}else{
				cam.fieldOfView -= 4;
			}
		}
	}

	IEnumerator WoahDisco(){
		yield return new WaitForSeconds(0.5f);
		disco.color =  new Color((float)Random.Range(0.0f,1.0f),(float)Random.Range(0.0f,1.0f),(float)Random.Range(0.0f,1.0f));
		RenderSettings.ambientLight = new Color((float)Random.Range(0.0f,1.0f),(float)Random.Range(0.0f,1.0f),(float)Random.Range(0.0f,1.0f));
		StartCoroutine("WoahDisco");
	}
}