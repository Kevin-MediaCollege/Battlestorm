using UnityEngine;
using System.Collections;

public class WaveManager:MonoBehaviour {
	public static int currentWave = 0;
	public static int waveTimer = 0;
	public static bool spawningEnemies = false;

	public int waveDelay;
	
	private WaveData waveData;
	private EnemyManager enemyManager;

	private int enemiesToSpawn;
	private int currentEnemy;

	void Start() {
		waveData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveData>();
		enemyManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();

		StartCoroutine("SpawnTimer");
	}

	void OnGUI() {
		if(GUI.Button(new Rect(0, 0, 50, 50), "Next Wave")){
			if(!spawningEnemies) {
				StopCoroutine("SpawnTimer");
				StartNextWave();
			}
		}
		
		GUI.Box(new Rect(50, 0, 50, 50), waveTimer.ToString());
	}

	void StartNextWave() {
		currentEnemy = 0;
		currentWave++;

		enemiesToSpawn = waveData.waveArray[currentWave - 1].enemies.Length - 1;
		spawningEnemies = true;
		
		StartCoroutine("SpawnEnemies");
	}

	IEnumerator SpawnTimer() {
		waveTimer = waveDelay;

		while(true) {
			if(waveTimer > 0) {
				yield return new WaitForSeconds(1);
				
				waveTimer--;
			} else {
				StartNextWave();
			}
		}
	}

	IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(1.8f);
			
			if(enemiesToSpawn > 0) {
				currentEnemy++;
				enemiesToSpawn--;

				WaveData.EnemyData eData = waveData.waveArray[currentWave - 1].enemies[currentEnemy - 1];
				enemyManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, (int)eData.spawn);
			} else {
				spawningEnemies = false;
				StartCoroutine("SpawnTimer");
				break;
			}
		}
	}
}
