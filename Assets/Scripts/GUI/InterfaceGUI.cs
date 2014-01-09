using UnityEngine;
using System.Collections;

public class InterfaceGUI:MonoBehaviour {
	public Texture texture;
	public GUIStyle style;
	public GUIStyle styleLarge;

	public bool speedup = false;

	void FixedUpdate() {
		if(speedup) {
			Time.timeScale = 2;
		} else {
			Time.timeScale = 1;
		}
	}

	void OnGUI() {
		float rX = Screen.width / GameManager.nativeWidth;
		float rY = Screen.height / GameManager.nativeHeight;

		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rX, rY, 1));

		GUI.BeginGroup(new Rect(115, 0, texture.width, texture.height), new GUIContent(texture));
			GUI.Label(new Rect(285, 13, 0, 0), PlayerData.Instance.stoneAmount.ToString(), style);
			GUI.Label(new Rect(190, 13, 0, 0), PlayerData.Instance.woodAmount.ToString(), style);
			GUI.Label(new Rect(90, 13, 0, 0), PlayerData.Instance.goldAmount.ToString(), style);

			if(GUI.Button(new Rect(0, 90, 50, 50), ""))
				speedup = !speedup;
			
			GUI.Label(new Rect(500, 35, 0, 0), WaveManager.currentWave.ToString(), styleLarge);
			
			if(!WaveManager.spawningEnemies) {
				GUI.Label (new Rect (620, 13, 0, 0), "Next wave in: " + WaveManager.waveTimer, style);
			} else {
				GUI.Label (new Rect (620, 13, 0, 0), "Spawning enemies!", style);
			}
		GUI.EndGroup();
	}
}