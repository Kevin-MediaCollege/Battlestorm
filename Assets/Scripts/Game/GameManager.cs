using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {

	public static float nativeWidth = 1280.0f; // The native width of the Game.

	public static float nativeHeight = 720.0f; // The native height of the Game.

	void Start() {
		Screen.lockCursor = false;
	}

	public static void EndGame() {
		Application.LoadLevel("GameMenu");
	}
}