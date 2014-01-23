using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {
	public static float nativeWidth = 1280.0f;
	public static float nativeHeight = 720.0f;
	
	private MusicFlow musicFlow;

	void Start() {
		Screen.lockCursor = false;

		musicFlow = GetComponent<MusicFlow>();
	}
	
	void FixedUpdate() {
		if(GameObject.FindGameObjectWithTag("Enemy")) {
			musicFlow.Wave();
		} else {
			musicFlow.Wait();
		}
	}

	public static void WinGame() {
		Debug.Log("You win!");
	}
}