﻿using UnityEngine;
using System.Collections;

public class Tower:Building {	
	public enum stoneSell{
		Price1 = 0,
		Price2 = 0,
		Price3 = 10,
		Price4 = 10,
		Price5 = 210,
		Price6 = 500
	};
	
	public enum woodSell{
		Price1 = 10,
		Price2 = 50,
		Price3 = 150,
		Price4 = 300,
		Price5 = 600,
		Price6 = 1000
	};

	private enum woodCostPerUpgrade {
		Upgrade2 = 0,
		Upgrade3 = 300,
		Upgrade4 = 500,
		Upgrade5 = 3000
	};
	
	private enum stoneCostPerUpgrade {
		Upgrade2 = 250,
		Upgrade3 = 600,
		Upgrade4 = 1200,
		Upgrade5 = 3000
	};

	public static string name = "Tower";
	public static string path = "Prefabs/Test/Tower/Tower";
	
	void Start () {
		base.Start();

		woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade2;
		stoneCostForNextLevel= (int)stoneCostPerUpgrade.Upgrade2;
		woodSellPrice = (int)woodSell.Price1;
		stoneSellPrice = (int)stoneSell.Price1;
		
		interactable = false;
		currentLevel = Upgrade.one;
		
		UpdateArt(path);
	}
	
	public void SwitchLevel(Upgrade level) {
		if(level > maxLevel)
			return;
		
		currentLevel = level;
		
		switch(level){
		case Upgrade.one:
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade2;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade2;
			woodSellPrice = (int)woodSell.Price1;
			stoneSellPrice = (int)stoneSell.Price1;
			
			break;
		case Upgrade.two:
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade3;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade3;
			woodSellPrice = (int)woodSell.Price2;
			stoneSellPrice = (int)stoneSell.Price2;
			
			break;
		case Upgrade.three:
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade4;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade4;
			woodSellPrice = (int)woodSell.Price3;
			stoneSellPrice = (int)stoneSell.Price3;
			
			break;
		case Upgrade.four:
			woodCostForNextLevel = (int)woodCostPerUpgrade.Upgrade5;
			stoneCostForNextLevel = (int)stoneCostPerUpgrade.Upgrade5;
			woodSellPrice = (int)woodSell.Price4;
			stoneSellPrice = (int)stoneSell.Price4;
			
			break;
		case Upgrade.five:
			woodCostForNextLevel = int.MaxValue;
			stoneCostForNextLevel = int.MaxValue;
			woodSellPrice = (int)woodSell.Price5;
			stoneSellPrice = (int)stoneSell.Price5;
			
			break;
		}
		
		UpdateArt(path);
	}
}