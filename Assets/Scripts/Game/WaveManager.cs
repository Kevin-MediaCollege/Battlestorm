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

	public Texture warningTexture;
	public Color warningcolor = new Color(1,1,1,0);
	public IslandData[] checkIsland;
	public bool backrowUnlocked;
	public ParticleSystem[] firstrow;
	public ParticleSystem[] backrow;
	public MusicFlow musicFlow;
	void Start() {
		waveData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveData>();
		enemyManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();

		StartCoroutine("SpawnTimer");
	}
	void OnGUI(){
		float rx = Screen.width / GameManager.nativeWidth;
		float ry = Screen.height / GameManager.nativeHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
		GUI.color = warningcolor;
		GUI.DrawTexture(new Rect(435,640,warningTexture.width,warningTexture.height),warningTexture);
	}
	void Update() {
		if(enemyManager.enemyList.Count == 0){
			musicFlow.Wait();
		}else{
			musicFlow.Wave();
		}
		if(waveData.spawningEnemies){
			if(backrowUnlocked){
				for(int i = 0; i < backrow.Length; i++){
					backrow[i].enableEmission = true;
				}
			}
			else{
				for(int i = 0; i < firstrow.Length; i++){
					firstrow[i].enableEmission = true;
				}
			}
		}else{
			for(int i = 0; i < backrow.Length; i++){
				backrow[i].enableEmission = false;
			}
			for(int i = 0; i < backrow.Length; i++){
				firstrow[i].enableEmission = false;
			}
		}
		if(enemyManager.enemyList.Count == 0 && waveData.waveArray[waveData.currentWave].hasWarning && !waveData.spawningEnemies){
			if(warningcolor.a < 1){
				warningcolor.a += 0.05f;
			}
		}else{
			if(warningcolor.a > 0){
			warningcolor.a -= 0.05f;
			}
		}
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
				bool checkspawn = false;
				int spawn = (int)eData.spawn;
				switch(spawn){
					case 0:
					spawn = 0;
					break;
					case 1:
					spawn = 2;
					break;
					case 2:
					spawn = 4;
					break;
				}
				for(int i = 0; i < checkIsland.Length; i++){
					if(checkIsland[i].isUnlocked){
						checkspawn = true;

						break;
					}
				}
				if(checkspawn)
					backrowUnlocked = true;

				if(checkspawn) {
					enemyManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, (spawn + 1));
				} else {
					enemyManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, spawn);
				}
			} else {
				waveData.spawningEnemies = false;
				StartCoroutine("SpawnTimer");
				break;
			}
		}
	}
}
