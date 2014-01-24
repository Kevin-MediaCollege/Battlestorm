using UnityEngine;
using System.Collections;
public class FadeScript:MonoBehaviour {
	private Texture2D fadeTexture;
	public Texture2D blackTexture;
	public Texture2D whiteTexture;
	public bool fadingOut = false;
	public float alphaFadeValue = 0;
	public float fadeSpeed = 1;
	

	void Start() {
		fadeTexture = blackTexture;
		StartCoroutine("delay");
		if(fadeTexture == null) {
			fadeTexture = new Texture2D(1, 0);
			fadeTexture.SetPixel(1, 1, new Color (0, 0, 0, 1));
		}
	}
	IEnumerator delay(){
		yield return new WaitForSeconds(1);
		fadeTexture = whiteTexture;
	}
	void OnGUI () {
		GUI.depth = -2;

		if(fadingOut) {
			fadeSpeed = 0.8f;
		} else {
			fadeSpeed = 1.5f;
		}

		GUI.depth = 1;
		alphaFadeValue = Mathf.Clamp01 (alphaFadeValue + ((Time.deltaTime / fadeSpeed) * (fadingOut ? 1 : -1)));

		if(alphaFadeValue != 0) {
			GUI.color = new Color (1, 1, 1, alphaFadeValue);
			GUI.DrawTexture (new Rect (-10000, -10000, 50000, 50000), fadeTexture);
		}
	}
}