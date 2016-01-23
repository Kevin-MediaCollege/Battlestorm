using UnityEngine;

using System.Collections;



public class LoadingScreen : MonoBehaviour {
	private static LoadingScreen instance = null;
	private string levelname;
	public Texture loadbar;
	public float loadingwidth;
	public Texture loadingTexture;
	public Texture loadingOverlay;
	
	private AsyncOperation async = null; 
	
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}



	public static LoadingScreen Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(LoadingScreen)) as LoadingScreen;
			}
			
			if(instance == null) {
				GameObject go = new GameObject("LoadingScreen");
				instance = go.AddComponent(typeof(LoadingScreen)) as LoadingScreen;
			}
			
			return instance;
		}
	}
	void OnApplicationQuit() {
		instance = null;
	}
	void Update () {
		if(Application.isLoadingLevel){
		}else{
			async = null;
		}
	}
	public void loadLoadingScreen(string level){
	//	levelname = level;
		StartCoroutine(LoadALevel(level));
	}
	private IEnumerator LoadALevel(string levelName) {
		async = Application.LoadLevelAsync(levelName);
		yield return async;
	}
	
	void OnGUI() {
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		if (async != null) {
			GUI.DrawTexture(new Rect(0, 0, 1285,750), loadingTexture);
			GUI.DrawTexture(new Rect(390, 575, 500 * async.progress, 40), loadbar);
			GUI.DrawTexture(new Rect(390, 575, 500, 40), loadingOverlay);
		}
	}
}