using UnityEngine;
using System.Collections;

public class Mine:Building {
	public enum ResourcesPerTick {
		Level1 = 10,
		Level2 = 15,
		Level3 = 25,
		Level4 = 40,
		Level5 = 60
	};
	
	public enum StoneCost {
		Level1 = 0,
		Level2 = 250,
		Level3 = 600,
		Level4 = 1200,
		Level5 = 3000
	};
	
	public enum StoneSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};
	
	private enum WoodCost {
		Level1 = 0,
		Level2 = 150,
		Level3 = 300,
		Level4 = 500,
		Level5 = 3000
	};
	
	public enum WoodSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};

	private ResourcesPerTick resourcesPerTick;

	void Start () {
		currentLevel = Upgrade.Level1;

		stoneCost = (int)StoneCost.Level2;
		stoneSell = (int)StoneSell.Level1;
		
		woodCost = (int)WoodCost.Level2;
		woodSell = (int)WoodSell.Level1;
		
		resourcesPerTick = ResourcesPerTick.Level1;

		UpdateArt();
		StartCoroutine("Tick");
	}
	
	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);

			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/StoneResourceText"), transform.position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();

			PlayerData.Instance.stoneAmount += resourcesPerTick;

			textPopup.text = "" + (int)resourcesPerTick;
			textPopup.color = Color.gray;
			textPopup.transform.parent = this.transform;
		}
	}
	
	public override void SwitchLevel(Upgrade newLevel) {
		if(newLevel > maxLevel)
			return;
		
		currentLevel = newLevel;
		
		stoneCost++;
		stoneSell++;
		
		woodCost++;
		woodSell++;
		
		resourcesPerTick++;
		
		UpdateArt();
	}
}
