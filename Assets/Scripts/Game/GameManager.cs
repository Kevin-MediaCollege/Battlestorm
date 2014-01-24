using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {
	public static float nativeWidth = 1280.0f;
	public static float nativeHeight = 720.0f;

	void Start() {
		Screen.lockCursor = false;
	}

	public static void WinGame() {
		Debug.Log("You win!");
	}
}