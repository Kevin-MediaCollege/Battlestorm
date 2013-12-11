using UnityEngine;
using System.Collections;

public class Building:MonoBehaviour {
	public enum Upgrade {
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Level5 = 5
	};

	public string prefabPath = "Prefabs/Buildings/";
	
	public bool interactable;
	public int tickDelay;

	public int stoneCost;
	public int stoneSell;

	public int woodCost;
	public int woodSell;

	public Upgrade currentLevel;
	public Upgrade maxLevel;

	protected GameObject art;
	
	public virtual void SwitchLevel(Upgrade newLevel) { }

	protected virtual IEnumerator Tick() { return null; }

	protected void UpdateArt() {
		if(art != null)
			Destroy(art);

		art = Instantiate(Resources.Load(prefabPath + (int)currentLevel), transform.position, Quaternion.identity) as GameObject;
		art.transform.parent = this.transform;
	}
}
