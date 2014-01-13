using UnityEngine;
using System.Collections;

public class InterfaceGUI:MonoBehaviour {
	public Texture texture;

	public GUIStyle style;
	public GUIStyle styleLarge;

	public GUIStyle styleMenu;

	private bool speedup = false;

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
			GUI.Label(new Rect(95, 13, 0, 0), PlayerData.Instance.goldAmount.ToString(), style);
			GUI.Label(new Rect(200, 13, 0, 0), PlayerData.Instance.woodAmount.ToString(), style);
			GUI.Label(new Rect(295, 13, 0, 0), PlayerData.Instance.stoneAmount.ToString(), style);
			
			if(GUI.Button(new Rect(0, 90, 50, 50), ""))
				speedup = !speedup;
			
			GUI.Label(new Rect(505, 35, 0, 0), WaveData.Instance.currentWave.ToString(), styleLarge);
			
			if(!WaveData.Instance.spawningEnemies) {
				GUI.Label(new Rect(630, 13, 0, 0), "Next wave in: " + WaveData.Instance.waveTimer, style);
			} else {
				GUI.Label(new Rect(630, 13, 0, 0), "Spawning enemies!", style);
			}

			if(GUI.Button(new Rect(925, 18, 45, 19), "", styleMenu))
				Application.LoadLevel(0);
		GUI.EndGroup();
	}
}