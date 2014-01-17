using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeedbackText:MonoBehaviour {
	public int duration;

	public GUIStyle textStyle;

	private ArrayList texts = new ArrayList();

	public void AddText(string text) {
		texts.Add(text);
	}

	void OnGUI() {
		float rX = Screen.width / GameManager.nativeWidth;
		float rY = Screen.height / GameManager.nativeHeight;
		
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		for(int i = 0; i < texts.Count; i++) {
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 0, 0), texts[i].ToString(), textStyle);
		}
	}
}