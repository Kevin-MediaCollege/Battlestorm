using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static float nativeWidth = 1228.0f;
	public static float nativeHeight = 720.0f;

	private EnemyManager eManager;
	
	public static int currentWave = 0;
	
	private int enemiesToSpawn;
	private int currentEnemy;
	private bool allSpawned;
	
	private int waveTime = 35;
	private string waveTimer;
	private bool timerStarted = false;

	private MusicFlow bgm;

	public WaveData wData;
	
	// Use this for initialization
	void Start () {
		bgm = GetComponent<MusicFlow>();
		eManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(allSpawned && eManager.enemyList.Count == 0 && !timerStarted){
			timerStarted = true;
			StartCoroutine("spawnTimer");
			bgm.wavestarted = false;
		}
	}
	void Nextwave(){
		currentEnemy = 0;
		currentWave++;
		allSpawned = false;
		timerStarted = false;
		enemiesToSpawn = wData.waveArray[currentWave - 1].enemies.Length;
		bgm.wavestarted = true;
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
			currentEnemy++;
			waveTimer = "";
			Debug.Log("" + (currentWave - 1) + " : " + (currentEnemy - 1));
			WaveData.EnemyData eData = wData.waveArray[currentWave - 1].enemies[currentEnemy - 1];
			eManager.spawnEnemy("" + eData.name.ToString(),eData.health,eData.speed,(int)eData.spawn);
			enemiesToSpawn--;
			StartCoroutine("spawnEnemies");
		}
		else{
			waveTime = 35;
			allSpawned = true;
		}
	}
	void OnGUI(){
		if(GUI.Button(new Rect(0,0,50,50),"Next Wave")){
			Debug.Log(eManager.enemyList.Count);
			if(eManager.enemyList.Count == 0 && enemiesToSpawn == 0){
				StopCoroutine("spawnTimer");
				waveTimer = "";
				Nextwave();
			}
		}
		GUI.Box(new Rect(50,0,50,50),waveTimer);
	}
}
