using UnityEngine;
using System.Collections;
public class FadeScript : MonoBehaviour
{
	public Texture2D fadeTexture; //Texture use to fade with.
	public bool fadingOut = false; //if true, fade texture out. if false fade texture in.
	public float alphaFadeValue = 0; // the value of transparancy of the FadeTexture
	public float fadeSpeed = 1; // The fadespeed of the FadeTexture
	
	
	void Start ()
	{
		if (fadeTexture == null) {
			fadeTexture = new Texture2D (1, 0);
			fadeTexture.SetPixel (1, 1, new Color (0, 0, 0, 1));
		}
	}
	
	void OnGUI ()
	{
		if(fadingOut){
			fadeSpeed = 0.8f;
		}else{
			fadeSpeed = 1.5f;
		}
		GUI.depth = 1;
		alphaFadeValue = Mathf.Clamp01 (alphaFadeValue + ((Time.deltaTime / fadeSpeed) * (fadingOut ? 1 : -1)));
		if (alphaFadeValue != 0) {
			GUI.color = new Color (1, 1, 1, alphaFadeValue);
			GUI.DrawTexture (new Rect (-10000, -10000, 50000, 50000), fadeTexture);
		}
	}
}