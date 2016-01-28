using UnityEngine;
using System.Collections;

public class WaveManager:MonoBehaviour {

    public delegate void WaveEvent ();
    public event WaveEvent OnWaveStarted;
    public event WaveEvent OnEnemiesSpawned;

    public int waveDelay; // Delay between waves.

	public int startDelay; // Delay before first wave.

	[HideInInspector]
	public WaveData waveData; // Reference to WaveData.

	private EnemyManager enemyManager; // Reference to EnemyManager.

	private int enemiesToSpawn; // Amount of remaining enemies to spawn.

	private int currentEnemy; // Current enemy being spawned.

	private bool finalWave; // check if its the last wave.

	private bool pauseDelayStarted; // check if pausedelay is Started.

	public bool gonextwave; // check if next wave will be started.

	public IslandData[] checkIsland; // Reference to island being checked for moving spawnposition.

	public bool backrowUnlocked; // if the backrow of spawners has been unlocked.

	public ParticleSystem[] firstrow; // Effect of enemies spawnposition.

	public ParticleSystem[] backrow; // Effect of enemies spawnposition.

	public MusicFlow musicFlow; // Reference to Musicflow.

   
	void Start() {
		//Get reference to Objects.
		waveData = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaveData>();
		enemyManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyManager>();

        EnablePortals(false, false);
        EnablePortals(true, false);

        StartCoroutine("SpawnTimer");
	}


	void Update() {

		if(gonextwave && waveData.spawningEnemies){
			Time.timeScale = 1;
            musicFlow.SetPitch(1);
			gonextwave = false;
		}

		if(!pauseDelayStarted && (waveData.currentWave % 5) == 0) {
			waveData.waveTimer = startDelay;
			pauseDelayStarted = true;
		}

        //TODO: is dirty
		if(finalWave) {

			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

			if(enemies.Length == 0){

				GameManager.WinGame();

			}

		}

	}
	
	public void StartNextWaveButton() {
		if(!waveData.spawningEnemies) {
			gonextwave = true;
			Time.timeScale = 5;
            musicFlow.SetPitch(1.4f);
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

    private void EnablePortals(bool _firstRow,bool _state) {

        if (_firstRow) {

            for (int i = 0; i < firstrow.Length; i++) {
                firstrow[i].enableEmission = _state;
            }

        } else {

            for (int i = 0; i < backrow.Length; i++) {
                backrow[i].enableEmission = _state;
            }

        }

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
        OnWaveStarted();

        //Set the portals
        if (backrowUnlocked) {

            EnablePortals(true, false);
            EnablePortals(false, true);

        } else {

            EnablePortals(false, false);
            EnablePortals(true, true);

        }

        musicFlow.Wave();

        while (true) {
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

				if(checkspawn) {
                    backrowUnlocked = true;
                    enemyManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, (spawn + 1));
				} else {
					enemyManager.SpawnEnemy(eData.name.ToString(), eData.health, eData.speed, spawn);
				}
			} else {
				waveData.spawningEnemies = false;
                OnEnemiesSpawned();
				StartCoroutine("SpawnTimer");
				break;
			}

		}

        EnablePortals(false, false);
        EnablePortals(true, false);
        musicFlow.Wait();

    }

}