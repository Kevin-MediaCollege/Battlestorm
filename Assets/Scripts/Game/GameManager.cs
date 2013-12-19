using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static float nativeWidth = 1920.0f;
	public static float nativeHeight = 1080.0f;

	private EnemyManager eManager;
	
	private int currentWave = 0;
	//TODO use xml or something to load the wave amounts
	private int[] wavelength = {3,5,7,10,12,24,30};
	
	private int enemiesToSpawn;
	private bool allSpawned;
	
	private int waveTime = 35;
	private string waveTimer;
	private bool timerStarted = false;
	// Use this for initialization
	void Start () {
		eManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(allSpawned && eManager.enemyList.Count == 0 && !timerStarted){
			timerStarted = true;
			StartCoroutine("spawnTimer");
		}
	}
	void Nextwave(){
		currentWave++;
		allSpawned = false;
		timerStarted = false;
		enemiesToSpawn = wavelength[currentWave];
		StartCoroutine("spawnEnemies");
	}
	IEnumerator spawnTimer(){
		if(waveTime != 0){
			yield return new WaitForSeconds(1);
			waveTime--;
			waveTimer = "" + waveTime;
			StartCoroutine("spawnTimer");
		}
		else{
			waveTimer = "";
			Nextwave();
		}
	}
	IEnumerator spawnEnemies(){
		yield return new WaitForSeconds(1.8f);
		if(enemiesToSpawn != 0){
			waveTimer = "";
			eManager.spawnEnemy();
			enemiesToSpawn--;
			StartCoroutine("spawnEnemies");
		}
		else{
			waveTime = 35;
			allSpawned = true;
		}
	}
	void OnGUI(){
		/*Test GUI
		if(GUI.Button(new Rect(0,0,50,50),"Next Wave")){
			Debug.Log(eManager.enemyList.Count);
			if(eManager.enemyList.Count == 0 && enemiesToSpawn == 0){
				StopCoroutine("spawnTimer");
				waveTimer = "";
				Nextwave();
			}
		}
		GUI.Box(new Rect(50,0,50,50),waveTimer);*/
	}
}
