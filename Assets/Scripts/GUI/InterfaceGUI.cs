using UnityEngine;
using System.Collections;

public class InterfaceGUI:MonoBehaviour {
	public Texture texture;
	public GUIStyle style;
	public GUIStyle styleLarge;

	void OnGUI() {


		float rX = Screen.width / GameManager.nativeWidth;
		float rY = Screen.height / GameManager.nativeHeight;

		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		GUI.BeginGroup(new Rect(0, 0, texture.width, texture.height), new GUIContent(texture));
			GUI.Label(new Rect(285, 13, 0, 0), PlayerData.Instance.stoneAmount.ToString(), style);
			GUI.Label(new Rect(190, 13, 0, 0), PlayerData.Instance.woodAmount.ToString(), style);
			GUI.Label(new Rect(90, 13, 0, 0), PlayerData.Instance.goldAmount.ToString(), style);
			
			GUI.Label(new Rect(0, 0, 0, 0), GameManager.currentWave.ToString(), styleLarge);
		GUI.EndGroup();
	}
}
