using UnityEngine;
using System.Collections;

public class WaveData:MonoBehaviour {
	private static WaveData instance = null;

	public int currentWave = 0; // Current wave your playing

	public int waveTimer = 0; // the wave Timer.

	public bool spawningEnemies = false; // Check whether enemies are spawning.
	
	public static WaveData Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType(typeof(WaveData)) as WaveData;
			}
			
			if(instance == null) {
				GameObject go = new GameObject("WaveData");
				instance = go.AddComponent(typeof(WaveData)) as WaveData;
			}
			
			return instance;
		}
	}
	
	void OnApplicationQuit() {
		instance = null;
	}

	[System.Serializable]
	public class Row {
		public float spawnDelay;
		public bool hasWarning;
		public EnemyData[] enemies;	
	};

	[System.Serializable]
	public class EnemyData {
		public enum SpawnPosition {
			Left,
			Right,
			Middle,
		};

		public enum EnemyTypes {
			Tank,
			Dasher,
			TNT
		};

		public SpawnPosition spawn; // Spawn position of the enemy.

		public EnemyTypes name; //Type of enemy.

		public float speed; // Enemy Speed.

		public float health; // Enemy health.

	};

	public Row[] waveArray = new Row[3];
}
