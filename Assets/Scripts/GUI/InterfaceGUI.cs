using UnityEngine;
using System.Collections;

public class InterfaceGUI:MonoBehaviour {
	public Texture texture;

	public Texture2D fastForward;
	public Texture2D fastForwardActive;

	public GUIStyle style;
	public GUIStyle styleLarge;

	public GUIStyle styleMenu;
	public GUIStyle styleFastForward;
	public GUIStyle styleNextWave;

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

		GUI.BeginGroup(new Rect(115, 0, texture.width, texture.height + 28), new GUIContent(texture));
			GUI.Label(new Rect(85, 15, 0, 0), PlayerData.Instance.goldAmount.ToString(), style);
			GUI.Label(new Rect(192, 15, 0, 0), PlayerData.Instance.woodAmount.ToString(), style);
			GUI.Label(new Rect(292, 15, 0, 0), PlayerData.Instance.stoneAmount.ToString(), style);
			
			GUI.Label(new Rect(505, 35, 0, 0), WaveData.Instance.currentWave.ToString(), styleLarge);
			
			if(!WaveData.Instance.spawningEnemies) {
				GUI.Label(new Rect(630, 15, 0, 0), "Next wave in: " + WaveData.Instance.waveTimer, style);
			} else {
				GUI.Label(new Rect(630, 15, 0, 0), "Incomming", style);
			}

			if(GUI.Button(new Rect(895, 10, 73, 35), "", styleMenu))
				Application.LoadLevel("Menu");
			
			
			WaveManager waveManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveManager>();
			if(!waveManager.waveData.spawningEnemies) {
				if(GUI.Button(new Rect (450, texture.height + 5, 117, 23), "", styleNextWave)) {
					waveManager.StartNextWaveButton();
				}
			}

			if(GUI.Button(new Rect(855, 12, 34, 31), "", styleFastForward)) {
				speedup = !speedup;
				styleFastForward.hover.background = speedup ? fastForward : fastForwardActive;
				styleFastForward.normal.background = speedup ? fastForwardActive : fastForward;

				gameObject.GetComponent<FeedbackText>().AddText("Fapfapfap");
			}
		   		
		GUI.EndGroup();
	}
}