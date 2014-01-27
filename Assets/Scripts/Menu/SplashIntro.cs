using UnityEngine;
using System.Collections;

public class SplashIntro : MonoBehaviour {

	public Color logocolor; //Logo Starting Color.

	public Texture logoTexture; // Texture of the Logo.

	public bool fadeinLogo; // Check whether to fade in the Logo

	public bool showlogo; // Show Game Logo.

	void Start () {
		logocolor = new Color(1,1,1,0);
		StartCoroutine("SplashEvent");
	}

	IEnumerator SplashEvent(){
		fadeinLogo = true;
		showlogo = true;
		yield return new WaitForSeconds(3);
		fadeinLogo = false;
		yield return new WaitForSeconds(3.5f);
		Application.LoadLevel("GameMenu");
	}

	void FixedUpdate () {

		if(fadeinLogo){
			logocolor.a += 0.01f;
		}else{
			if(logocolor.a >= 0){
			logocolor.a -= 0.01f;
			}
		}

	}

	void OnGUI(){
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(showlogo){
			Debug.Log(logocolor);
			GUI.color = logocolor;
			GUI.DrawTexture(new Rect(350,50,600,600),logoTexture);
		}

	}

}
