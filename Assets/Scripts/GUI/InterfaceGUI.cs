using UnityEngine;
using System.Collections;

public class InterfaceGUI:MonoBehaviour {
	public Texture texture;
	public GUIStyle style;
	public GUIStyle styleLarge;

	void OnGUI() {
		float center = (Screen.width / 2) - (texture.width / 2);

		float rX = Screen.width / GameManager.nativeWidth;
		float rY = Screen.height / GameManager.nativeHeight;

		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		GUI.BeginGroup(new Rect(center, 0, texture.width, texture.height), new GUIContent(texture));
			GUI.Label(new Rect(center - 375, 0, 0, 0), PlayerData.Instance.goldAmount.ToString(), style);
			GUI.Label(new Rect(center - 270, 0, 0, 0), PlayerData.Instance.woodAmount.ToString(), style);
			GUI.Label(new Rect(center - 175, 0, 0, 0), PlayerData.Instance.stoneAmount.ToString(), style);
			
			GUI.Label(new Rect(center, 0, 0, 0), GameManager.currentWave.ToString(), styleLarge);
		GUI.EndGroup();
	}
}
