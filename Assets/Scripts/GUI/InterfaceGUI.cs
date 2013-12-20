using UnityEngine;
using System.Collections;

public class InterfaceGUI:MonoBehaviour {
	public Texture texture;
	public Texture button;
	public Font font;

	void OnGUI() {
		float center = (Screen.width / 2) - (texture.width / 2);

		float rX = Screen.width / GameManager.nativeWidth;
		float rY = Screen.height / GameManager.nativeHeight;

		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		GUI.BeginGroup(new Rect(center, 0, texture.width, texture.height), new GUIContent(texture));
			GUI.DrawTexture(new Rect(center + 16, 0, button.width, button.height), button);
		GUI.EndGroup();
	}
}
