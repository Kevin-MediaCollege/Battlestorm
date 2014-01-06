using UnityEngine;
using System.Collections;

public class WaveData : MonoBehaviour {
	[System.Serializable]
	public class Row{
		public EnemyData[] enemies;	
	};
	[System.Serializable]
	public class EnemyData{
		public enum spawnPosition{
			Left,
			Right,
			Middle
		}
		public enum EnemyTypes{
			Enemy,
			Dasher
		}
		public EnemyTypes name;
		public spawnPosition spawn;
		public float speed;
		public float health;
	};

	public Row[] waveArray = new Row[3];

}
