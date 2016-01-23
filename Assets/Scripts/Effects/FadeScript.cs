using UnityEngine;
using System.Collections;
public class FadeScript:MonoBehaviour {

    public delegate void FadeEvent ();
    public event FadeEvent OnFadeComplete;

    private Texture2D fadeTexture; // Current FadeTexture.

	public Texture2D blackTexture; // Black FadeTexture.

	public Texture2D whiteTexture; // White FadeTexture.

	public bool fading = false; // Sets FadeState.

	public float alphaFadeValue = 0; // Value of the Alpha.

	public float fadeSpeed = 1; // Speed of FadeAnimation
	

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

    public void StartFade () {
        fading = true;
    }

	void OnGUI () {
		GUI.depth = -2;

		if(fading) {
			fadeSpeed = 0.8f;
		} else {
			fadeSpeed = 1.5f;
		}
        if (alphaFadeValue == 1) {
            if (OnFadeComplete != null) {
                OnFadeComplete();
             
            }
            fading = false;
        }
		GUI.depth = 1;
		alphaFadeValue = Mathf.Clamp01 (alphaFadeValue + ((Time.deltaTime / fadeSpeed) * (fading ? 1 : -1)));

		if(alphaFadeValue != 0) {
			GUI.color = new Color (1, 1, 1, alphaFadeValue);
			GUI.DrawTexture (new Rect (-10000, -10000, 50000, 50000), fadeTexture);
		}
	}
}