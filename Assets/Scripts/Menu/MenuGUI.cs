using UnityEngine;
using System.Collections;

public class MenuGUI:MonoBehaviour {
	public GUIStyle buttonStyle;
	public float minorOffset;
	private bool offsetbool;

	private string lastGUITooltip;
	private string tooltip;

	public AudioClip buttonHover;
	public AudioClip buttonClick;

	void Start () {
		StartCoroutine("giveOffset");
	}

	void FixedUpdate() {
		if(tooltip != "" && tooltip != lastGUITooltip) {
			playSound (2);
			lastGUITooltip = tooltip;
		}

		if(tooltip == "") {
			lastGUITooltip = "NoTooltip";
		}
	}

	IEnumerator giveOffset(){
		while (true) {
			yield return new WaitForSeconds (0.1f);

			if(offsetbool) {
				minorOffset += 0.5f;
			} else {
				minorOffset -= 0.5f;
			}

			if (minorOffset == 5)
				offsetbool = false;

			if (minorOffset == -5)
				offsetbool = true;
		}
	}

	void playSound(int sound) {
		if (!audio.isPlaying) {
			switch (sound) {
			case 1:
				audio.PlayOneShot(buttonClick);
				break;
			case 2:
				audio.PlayOneShot(buttonHover);
				break;
			}
		}
	}

	void OnGUI() {
		GUI.depth = -100000;
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(GUI.Button(new Rect(30, 455, 190 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Play", "Play"), buttonStyle)) {
			playSound(1);
			Application.LoadLevel("GameMap");
		}

		if(GUI.Button(new Rect(15, 555, 300 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Options", "Options"), buttonStyle))
			playSound(1);

		if(GUI.Button(new Rect(15, 655, 300 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Credits", "Credits"), buttonStyle))
			playSound(1);

		tooltip = GUI.tooltip;

		if(!Application.isWebPlayer) {
			if(GUI.Button(new Rect(795, 655, 190 + (minorOffset / 2), 90 + minorOffset), new GUIContent("Quit", "Quit"), buttonStyle))
				Application.Quit();
		}
	}
}
