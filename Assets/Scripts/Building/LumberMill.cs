using UnityEngine;
using System.Collections;

public class LumberMill:Building {
	public enum ResourcesPerTick {
		Level1 = 10,
		Level2 = 15,
		Level3 = 25,
		Level4 = 40,
		Level5 = 60
	};

	private enum GoldCost {
		Level1 = 0,
		Level2 = 100,
		Level3 = 250,
		Level4 = 500,
		Level5 = 1000
	};

	private enum StoneCost {
		Level1 = 0,
		Level2 = 250,
		Level3 = 600,
		Level4 = 1200,
		Level5 = 3000
	};

	private enum WoodCost {
		Level1 = 0,
		Level2 = 150,
		Level3 = 300,
		Level4 = 500,
		Level5 = 3000
	};

	private enum GoldSell {
		Level1 = 50,
		Level2 = 100,
		Level3 = 250,
		Level4 = 500,
		Level5 = 1000
	};

	private enum StoneSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};
	
	private enum WoodSell {
		Level1 = 0,
		Level2 = 10,
		Level3 = 10,
		Level4 = 210,
		Level5 = 500
	};

	private ResourcesPerTick resourcesPerTick;

	void Start() {
		resourcesPerTick = ResourcesPerTick.Level1;
		currentLevel = Upgrade.Level1;

		goldCost = (int)GoldCost.Level2;
		stoneCost = (int)StoneCost.Level2;
		woodCost = (int)WoodCost.Level2;

		goldSell = (int)GoldSell.Level1;
		woodSell = (int)WoodSell.Level1;
		stoneSell = (int)StoneSell.Level1;

		UpdateArt();
		StartCoroutine("Tick");
	}
	
	protected override IEnumerator Tick() {
		while(true) {
			yield return new WaitForSeconds(tickDelay);
			
			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), transform.position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();
			
			PlayerData.Instance.woodAmount += resourcesPerTick;
			
			textPopup.text = "" + (int)resourcesPerTick;
			textPopup.color = new Color(0.6f, 0.2f, 0);
			textPopup.transform.parent = this.transform;
		}
	}

	public override void SwitchLevel(Upgrade newLevel) {
		if(newLevel > maxLevel)
			return;
			switch(newLevel){
				case Upgrade.Level1:
					stoneCost = (int)StoneCost.Level2;
					stoneSell = (int)StoneSell.Level1;
			
					woodCost = (int)WoodCost.Level2;
					woodSell = (int)WoodSell.Level1;
			
					resourcesPerTick = ResourcesPerTick.Level1;
				break;
				case Upgrade.Level2:
					stoneCost = (int)StoneCost.Level3;
					stoneSell = (int)StoneSell.Level2;
			
					woodCost = (int)WoodCost.Level3;
					woodSell = (int)WoodSell.Level2;
			
					resourcesPerTick = ResourcesPerTick.Level2;
				break;
				case Upgrade.Level3:
					stoneCost = (int)StoneCost.Level4;
					stoneSell = (int)StoneSell.Level3;
			
					woodCost = (int)WoodCost.Level4;
					woodSell = (int)WoodSell.Level3;
			
					resourcesPerTick = ResourcesPerTick.Level3;
				break;
				case Upgrade.Level4:
					stoneCost = (int)StoneCost.Level4;
					stoneSell = (int)StoneSell.Level5;
			
					woodCost = (int)WoodCost.Level5;
					woodSell = (int)WoodSell.Level4;
			
					resourcesPerTick = ResourcesPerTick.Level4;
				break;
				case Upgrade.Level5:
					stoneCost = (int)StoneCost.Level5;
					stoneSell = (int)StoneSell.Level5;
			
					woodCost = (int)WoodCost.Level5;
					woodSell = (int)WoodSell.Level5;
				
					resourcesPerTick = ResourcesPerTick.Level5;
				break;
			}
		currentLevel = newLevel;
		
		UpdateArt();
	}
}
