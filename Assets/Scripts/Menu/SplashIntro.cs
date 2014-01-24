using UnityEngine;
using System.Collections;

public class SplashIntro : MonoBehaviour {
	public GUIStyle nameStyle;
	public GUIStyle logoStyle;
	public Color logocolor;
	public Color namecolor;
	public Texture logoTexture;
	public Texture mediacollege;
	public bool fadein;
	public bool fadeinLogo;
	public bool showlogo;
	public int mediacollegeheight;
	// Use this for initialization
	void Start () {
		logocolor = new Color(1,1,1,0);
		namecolor = new Color(1,1,1,0);
		nameStyle.normal.textColor = namecolor;
		StartCoroutine("SplashEvent");
	}
	IEnumerator SplashEvent(){
		fadein = true;
		yield return new WaitForSeconds(3);
		fadein = false;
		yield return new WaitForSeconds(4);
		fadeinLogo = true;
		showlogo = true;
		yield return new WaitForSeconds(3);
		fadeinLogo = false;
		yield return new WaitForSeconds(3.5f);
		Application.LoadLevel("GameMenu");
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(fadein){
			namecolor.a += 0.01f;
			if(mediacollegeheight < 300){
				mediacollegeheight += 5;
			}
		}else{
			namecolor.a -= 0.01f;
			if(mediacollegeheight > 0){
				mediacollegeheight -= 2;
			}
		}
		if(fadeinLogo){
			logocolor.a += 0.01f;
		}else{
			if(logocolor.a >= 0){
			logocolor.a -= 0.01f;
			}
		}
		logoStyle.normal.textColor = logocolor;
		nameStyle.normal.textColor = namecolor;

	}

	void OnGUI(){
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		GUI.DrawTexture(new Rect(700,150,300,mediacollegeheight),mediacollege);
		GUI.Label(new Rect(675,660,100,500),"MediaCollege Amsterdam",nameStyle);
		GUI.Label(new Rect(25,100,100,500),"Jasper Boerstra",nameStyle);
		GUI.Label(new Rect(25,200,100,500),"Kevin Breurken",nameStyle);
		GUI.Label(new Rect(25,300,100,500),"Pieter Hoogerdijk",nameStyle);
		GUI.Label(new Rect(25,400,100,500),"Kevin Krol",nameStyle);
		GUI.Label(new Rect(25,500,100,500),"Charlie Mercelina",nameStyle);
		GUI.Label(new Rect(25,600,100,500),"Thomas Schrama",nameStyle);

		if(showlogo){
			Debug.Log(logocolor);
			GUI.color = logocolor;
			GUI.DrawTexture(new Rect(350,50,600,600),logoTexture);
		}
		//GUI.color = new Color(1,1,1,1);
	}
}
