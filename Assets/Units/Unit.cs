using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Defines a single unit in data terms

public class Unit {
	private string name;
	private string surname;
	private string title = "";
	
	// The categories this Unit belongs to
	private Dictionary<string,UnitCategoryInstance> categories;
	
	private int level;
	
	private Stats stats;
	private Stats statsIncrement;
	
	
	private FuzzyRule[] rules;
	
	
	private Inventory equips;
	
	public void init(UnitCategoryFactory ucf){
		
		categories = new Dictionary<string, UnitCategoryInstance>();
		categories["Class"] = ucf.getRandomInstance("Class");
		categories["Race"] = ucf.getRandomInstance("Race");
		
		level = 0;
		stats = new Stats();
		
		// Initial stats
		stats = categories["Class"].stats + categories["Race"].stats;
		
		
		name = "Lamba";
		surname = "Baran";
		title = "Sir";
		
		printInfo();
		
		
		//rules = new FuzzyRule[10]; // Behaviour rules
		
		
		//statsIncrement = categories["class"].stats;
		
		
		//levelUp();
		//levelUp();
		//printInfo();
		
		//equips = new Inventory();
		//equips.weapon_main = new Weapon("Sword","A simple sword",1,4,WeaponType.SWORD);

	}
	
	
		
	override public string ToString(){
		return name + " " + surname;
	}
	
	void printInfo(){
		Debug.Log (title + " " + name + " " + surname + " the " + categories["Race"].name + " " + categories["Class"].name);
		Debug.Log (stats);
	}
	
	
	void levelUp(){
		stats += statsIncrement;
	}
	

	// Adds a behaviour rule
	void addRule(){
		
	}
	
	private float selfTime = 0.0f;
	void Update () {
		
	}
	

	
	
}