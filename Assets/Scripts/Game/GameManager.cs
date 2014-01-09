using UnityEngine;
using System.Collections;

public class GameManager:MonoBehaviour {
	public static float nativeWidth = 1280.0f;
	public static float nativeHeight = 720.0f;

	public static int currentWave = 0;
	public static string waveTimer;

	public WaveData waveData;
	
	private int waveTime = 35;
	private int enemiesToSpawn;
	private int currentEnemy;

	private bool timerStarted = false;
	private bool allSpawned;

	private MusicFlow musicFlow;
	private EnemyManager eManager;

	void Start() {
		musicFlow = GetComponent<MusicFlow>();
		eManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();
	}

	void FixedUpdate() {
		if(allSpawned && eManager.enemyList.Count == 0 && !timerStarted) {
			timerStarted = true;
			StartCoroutine("spawnTimer");
			musicFlow.waveStarted = false;
		}
	}

	void Nextwave() {
		currentEnemy = 0;
		currentWave++;
		allSpawned = false;
		timerStarted = false;
		enemiesToSpawn = waveData.waveArray[currentWave - 1].enemies.Length - 1;
		musicFlow.waveStarted = true;
		
		StartCoroutine("spawnEnemies");
	}

	IEnumerator spawnTimer() {
		while(true) {
			if(waveTime != 0) {
				yield return new WaitForSeconds(1);

				waveTime--;
				waveTimer = "" + waveTime;
			} else {
				waveTimer = "";
				Nextwave();
			}
		}
	}

	IEnumerator spawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(1.8f);

			if(enemiesToSpawn != 0) {
				currentEnemy++;
				waveTimer = "";
				WaveData.EnemyData eData = waveData.waveArray[currentWave - 1].enemies[currentEnemy - 1];
				eManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, (int)eData.spawn);
				enemiesToSpawn--;
			} else {
				waveTime = 35;
				allSpawned = true;
				break;
			}
		}
	}

	void OnGUI() {
		if(GUI.Button(new Rect(0, 0, 50, 50), "Next Wave")){
			if(eManager.enemyList.Count == 0 && enemiesToSpawn == 0) {
				StopCoroutine("spawnTimer");
				waveTimer = "";
				Nextwave();
			}
		}

		GUI.Box(new Rect(50, 0, 50, 50), waveTimer);
	}
}
