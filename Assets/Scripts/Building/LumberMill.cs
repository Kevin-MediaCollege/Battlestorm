using UnityEngine;
using System.Collections;

public class LumberMill:Building {
	public enum resourcesPerUpgrade {
		Upgrade1 = 10,
		Upgrade2 = 15,
		Upgrade3 = 25,
		Upgrade4 = 40,
		Upgrade5 = 60
	};

	public enum stoneSell {
		Price1 = 0,
		Price2 = 0,
		Price3 = 10,
		Price4 = 10,
		Price5 = 210,
		Price6 = 500
	};

	public enum woodSell {
		Price1 = 10,
		Price2 = 50,
		Price3 = 150,
		Price4 = 300,
		Price5 = 600,
		Price6 = 1000
	};

	private enum woodCostPerUpgrade {
		Upgrade2 = 250,
		Upgrade3 = 600,
		Upgrade4 = 1200,
		Upgrade5 = 3000
	};
	
	private enum stoneCostPerUpgrade {
		Upgrade2 = 0,
		Upgrade3 = 0,
		Upgrade4 = 200,
		Upgrade5 = 3000
	};

	public static string name = "Lumber";
	public static string path = "Prefabs/Test/LumberMill/Mill";
	
	public resourcesPerUpgrade resourcesPerTick;

	void Start() {
		base.Start();

		timePerTick = 3;
		
		woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade2;
		stoneCostForNextLevel= (int)stoneCostPerUpgrade.Upgrade2;
		resourcesPerTick = resourcesPerUpgrade.Upgrade1;
		woodSellPrice = (int)woodSell.Price1;
		stoneSellPrice = (int)stoneSell.Price1;
		
		interactable = false;
		currentLevel = Upgrade.one;
		
		UpdateArt(path);
		StartCoroutine("tick");
	}
	
	IEnumerator tick() {
		while(true) {
			yield return new WaitForSeconds(timePerTick);
			
			GameObject popupText = Instantiate(Resources.Load("Prefabs/WoodResourceText"), transform.position, Quaternion.identity) as GameObject;
			TextMesh textPopup = popupText.GetComponent<TextMesh>();
			
			playerData.woodAmount += resourcesPerTick;
			
			textPopup.text = "" + (int)resourcesPerTick;
			textPopup.color = new Color(0.6f,0.2f,0);
			textPopup.transform.parent = this.transform;
		}
	}

	public void SwitchLevel(Upgrade level) {
		if(level > maxLevel)
			return;
		
		currentLevel = level;
		
		switch(level){
		case Upgrade.one:
			resourcesPerTick = resourcesPerUpgrade.Upgrade1;
			
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade2;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade2;
			woodSellPrice = (int)woodSell.Price1;
			stoneSellPrice = (int)stoneSell.Price1;
			
			break;
		case Upgrade.two:
			resourcesPerTick = resourcesPerUpgrade.Upgrade2;
			
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade3;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade3;
			woodSellPrice = (int)woodSell.Price2;
			stoneSellPrice = (int)stoneSell.Price2;
			
			break;
		case Upgrade.three:
			resourcesPerTick = resourcesPerUpgrade.Upgrade3;
			
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade4;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade4;
			woodSellPrice = (int)woodSell.Price3;
			stoneSellPrice = (int)stoneSell.Price3;
			
			break;
		case Upgrade.four:
			resourcesPerTick = resourcesPerUpgrade.Upgrade4;
			
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade5;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade5;
			woodSellPrice = (int)woodSell.Price4;
			stoneSellPrice = (int)stoneSell.Price4;
			
			break;
		case Upgrade.five:
			resourcesPerTick = resourcesPerUpgrade.Upgrade5;
			
			woodCostForNextLevel = int.MaxValue;
			stoneCostForNextLevel = int.MaxValue;
			woodSellPrice = (int)woodSell.Price5;
			stoneSellPrice = (int)stoneSell.Price5;
			
			break;
		}
		
		UpdateArt(path);
	}
}
