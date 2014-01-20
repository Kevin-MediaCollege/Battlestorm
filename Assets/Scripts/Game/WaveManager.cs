using UnityEngine;
using System.Collections;

public class WaveManager:MonoBehaviour {
	public int waveDelay;
	public int longWaveDelay;

	[HideInInspector]
	public WaveData waveData;

	private EnemyManager enemyManager;

	private int enemiesToSpawn;
	private int currentEnemy;

	private bool finalWave;
	private bool pauseDelayStarted;
	public bool gonextwave;

	public AudioSource wait;
	public AudioSource wave;
	void Start() {
		waveData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveData>();
		enemyManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();

		StartCoroutine("SpawnTimer");
	}

	void Update() {
		if(gonextwave && waveData.spawningEnemies){
			Time.timeScale = 1;
			wait.pitch = 1;
			wave.pitch = 1;
			Debug.Log(enemiesToSpawn);
			gonextwave = false;
		}
		if(!pauseDelayStarted && (waveData.currentWave % 5) == 0) {
			waveData.waveTimer = longWaveDelay;
			pauseDelayStarted = true;
		}

		if(finalWave) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

			if(enemies.Length == 0)
				GameManager.WinGame();
		}
	}
	
	public void StartNextWaveButton() {
		if(!waveData.spawningEnemies) {
			gonextwave = true;
			Debug.Log("set Timescale");
			Time.timeScale = 5;
			wait.pitch = 1.4f;
			wave.pitch = 1.4f;
		}
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
				pauseDelayStarted = false;
				StartNextWave();
				break;
			}
		}
	}

	IEnumerator SpawnEnemies() {
		while(true) {
			yield return new WaitForSeconds(waveData.waveArray[waveData.currentWave - 1].spawnDelay);
			
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
