using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {
	public GUIStyle backbutton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){
		float rX = Screen.width / GameManager.nativeWidth;
		float rY = Screen.height / GameManager.nativeHeight;
		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		if(GUI.Button(new Rect (550, 500, 200, 75), "", backbutton)) {
			Application.LoadLevel("GameMenu");
		}
	}
}
