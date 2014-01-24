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
	public WaveManager waveManager;

	public bool openverificationWindow;
	public Texture alphatext;
	public static bool lockmovement;
	public Texture background;
	public GUIStyle yes;
	public GUIStyle no;
	void FixedUpdate() {
		if(!waveManager.gonextwave){
		if(speedup) {
			Time.timeScale = 2;
		} else {
			Time.timeScale = 1;
		}
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
				GUI.Label(new Rect(630, 15, 0, 0), "Incoming", style);
			}

			if(GUI.Button(new Rect(895, 10, 73, 35), "", styleMenu)){
				openverificationWindow = true;
				lockmovement = true;
				//Application.LoadLevel("Menu");
			}
			
			if(!waveManager.waveData.spawningEnemies) {
				if(GUI.Button(new Rect (450, texture.height + 5, 117, 23), "", styleNextWave)) {
					waveManager.StartNextWaveButton();
				}
			}

			if(GUI.Button(new Rect(855, 12, 34, 31), "", styleFastForward)) {
				speedup = !speedup;
				styleFastForward.normal.background = speedup ? fastForwardActive : fastForward;
			}
		   		
		GUI.EndGroup();
		if(openverificationWindow){
		GUI.DrawTexture(new Rect(0,0,Screen.width + 500,Screen.height + 500),alphatext);
		GUI.DrawTexture(new Rect(430,300,400,200),background);
			GUI.Label(new Rect(470, 330, 0, 0), "All progress will be lost", style);
			GUI.Label(new Rect(540, 370, 0, 0), "Are you sure?", style);
			if(GUI.Button(new Rect(465, 415, 100, 50), "", yes)){
				lockmovement = false;
				Application.LoadLevel("GameMenu");
			   }
			if(GUI.Button(new Rect(690, 415, 100, 50), "", no)){
				lockmovement = false;
				openverificationWindow = false;
			}
		}
	}
}