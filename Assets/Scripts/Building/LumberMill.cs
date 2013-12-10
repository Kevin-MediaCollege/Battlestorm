using UnityEngine;
using System.Collections;

public class LumberMill:Building {
	public enum ResourcesPerUpgrade {
		Upgrade1 = 10,
		Upgrade2 = 15,
		Upgrade3 = 25,
		Upgrade4 = 40,
		Upgrade5 = 60
	};

	public enum StoneSell {
		Price1 = 0,
		Price2 = 0,
		Price3 = 10,
		Price4 = 10,
		Price5 = 210,
		Price6 = 500
	};

	public enum WoodSell {
		Price1 = 10,
		Price2 = 50,
		Price3 = 150,
		Price4 = 300,
		Price5 = 600,
		Price6 = 1000
	};

	private enum WoodCostPerUpgrade {
		Upgrade2 = 250,
		Upgrade3 = 600,
		Upgrade4 = 1200,
		Upgrade5 = 3000
	};
	
	private enum StoneCostPerUpgrade {
		Upgrade2 = 0,
		Upgrade3 = 0,
		Upgrade4 = 200,
		Upgrade5 = 3000
	};

	public static string name = "Lumber Mill";
	
	private ResourcesPerUpgrade resourcesPerTick;

	public ResourcesPerUpgrade ResourcesPerTick { get { return resourcesPerTick; } }

	void Start() {
		base.Start();

		path += "LumberMill/LumberMill";
		
		woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade2;
		stoneCostForNextLevel= (int)StoneCostPerUpgrade.Upgrade2;
		resourcesPerTick = ResourcesPerUpgrade.Upgrade1;
		woodSellPrice = (int)WoodSell.Price1;
		stoneSellPrice = (int)StoneSell.Price1;
		
		interactable = false;
		currentLevel = Upgrade.one;
		
		UpdateArt();
		StartCoroutine("tick");
	}
	
	IEnumerator tick() {
		while(true) {
			yield return new WaitForSeconds(timePerTick);
			
			GameObject popupText = Instantiate(Resources.Load("Prefabs/Text/WoodResourceText"), transform.position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();
			
			playerData.woodAmount += resourcesPerTick;
			
			textPopup.text = "" + (int)resourcesPerTick;
			textPopup.color = new Color(0.6f, 0.2f, 0);
			textPopup.transform.parent = this.transform;
		}
	}

	public override void SwitchLevel(Upgrade level) {
		if(level > maxLevel)
			return;

		currentLevel = level;

		switch(level){
		case Upgrade.one:
			resourcesPerTick = ResourcesPerUpgrade.Upgrade1;
			
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade2;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade2;
			woodSellPrice = (int)WoodSell.Price1;
			stoneSellPrice = (int)StoneSell.Price1;
			
			break;
		case Upgrade.two:
			resourcesPerTick = ResourcesPerUpgrade.Upgrade2;
			
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade3;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade3;
			woodSellPrice = (int)WoodSell.Price2;
			stoneSellPrice = (int)StoneSell.Price2;
			
			break;
		case Upgrade.three:
			resourcesPerTick = ResourcesPerUpgrade.Upgrade3;
			
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade4;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade4;
			woodSellPrice = (int)WoodSell.Price3;
			stoneSellPrice = (int)StoneSell.Price3;
			
			break;
		case Upgrade.four:
			resourcesPerTick = ResourcesPerUpgrade.Upgrade4;
			
			woodCostForNextLevel = (int)WoodCostPerUpgrade.Upgrade5;
			stoneCostForNextLevel = (int)StoneCostPerUpgrade.Upgrade5;
			woodSellPrice = (int)WoodSell.Price4;
			stoneSellPrice = (int)StoneSell.Price4;
			
			break;
		case Upgrade.five:
			resourcesPerTick = ResourcesPerUpgrade.Upgrade5;
			
			woodCostForNextLevel = int.MaxValue;
			stoneCostForNextLevel = int.MaxValue;
			woodSellPrice = (int)WoodSell.Price5;
			stoneSellPrice = (int)StoneSell.Price5;
			
			break;
		}
		
		UpdateArt();
	}
}
