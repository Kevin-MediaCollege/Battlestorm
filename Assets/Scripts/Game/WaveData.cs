using UnityEngine;
using System.Collections;

public class WaveData:MonoBehaviour {
	[System.Serializable]
	public class Row {
		public EnemyData[] enemies;	
	};

	[System.Serializable]
	public class EnemyData {
		public enum SpawnPosition {
			Left,
			Right,
			Middle
		};

		public enum EnemyTypes {
			Normal,
			Dasher
		};

		public SpawnPosition spawn;
		public EnemyTypes name;

		public float speed;
		public float health;
	};

	public Row[] waveArray = new Row[3];
}
