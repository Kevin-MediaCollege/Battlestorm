using UnityEngine;
using System.Collections;

public class WaveManager:MonoBehaviour {
	public int waveDelay;
	
	private WaveData waveData;
	private EnemyManager enemyManager;

	private int enemiesToSpawn;
	private int currentEnemy;

	private bool finalWave;

	void Start() {
		waveData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveData>();
		enemyManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();

		StartCoroutine("SpawnTimer");
	}

	void Update() {
		if(finalWave) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

			if(enemies.Length == 0)
				GameManager.WinGame();
		}
	}

	void OnGUI() {
		if(GUI.Button(new Rect(0, 0, 50, 50), "Next Wave")){
			if(!waveData.spawningEnemies) {
				StopCoroutine("SpawnTimer");
				StartNextWave();
			}
		}
		
		GUI.Box(new Rect(50, 0, 50, 50), waveData.waveTimer.ToString());
	}

	void StartNextWave() {
		currentEnemy = 0;
		waveData.currentWave++;

		if(waveData.currentWave > waveData.waveArray.Length) {
			finalWave = true;
			return;
		}

		enemiesToSpawn = waveData.waveArray[waveData.currentWave - 1].enemies.Length - 1;
		waveData.spawningEnemies = true;
		
		StartCoroutine("SpawnEnemies");
	}

	IEnumerator SpawnTimer() {
		waveData.waveTimer = waveDelay;

		while(true) {
			if(waveData.waveTimer > 0) {
				yield return new WaitForSeconds(1);
				
				waveData.waveTimer--;
			} else {
				StartNextWave();
				break;
			}
		}
	}

	IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(1.8f);
			
			if(enemiesToSpawn >= 0) {
				currentEnemy++;
				enemiesToSpawn--;

				WaveData.EnemyData eData = waveData.waveArray[waveData.currentWave - 1].enemies[currentEnemy - 1];
				enemyManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, (int)eData.spawn);
			} else {
				waveData.spawningEnemies = false;
				StartCoroutine("SpawnTimer");
				break;
			}
		}
	}
}
