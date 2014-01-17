using UnityEngine;
using System.Collections;

public class WaveData:MonoBehaviour {
	private static WaveData instance = null;

	public int currentWave = 0;
	public int waveTimer = 0;
	public bool spawningEnemies = false;
	
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
		public EnemyData[] enemies;	
	};

	[System.Serializable]
	public class EnemyData {
		public enum SpawnPosition {
			Left,
			Right,
			Middle,
			FrontLeft,
			FrontMiddle,
			FrontRight
		};

		public enum EnemyTypes {
			Tank,
			Dasher,
			TNT
		};

		public SpawnPosition spawn;
		public EnemyTypes name;

		public float speed;
		public float health;
	};

	public Row[] waveArray = new Row[3];
}
