using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

// Generates category instances

public class UnitCategoryFactory : MonoBehaviour {
	
	//List<List<UnitCategoryType>> categories;
	Dictionary<string,UnitCategoryType> unitCategories;
	
	void Awake () {
		
		unitCategories = new Dictionary<string,UnitCategoryType>();
		
		
		// Load the categories
		Dictionary<string,object> dictUnitCategories =  Json.loadJsonFromResources("unit_categories");
		List<object> listUnitCategories = ((List<object>) dictUnitCategories["unit_categories"]); 
		
		foreach (Dictionary<string,object> cat in listUnitCategories){
			
			string cat_name = (string)cat["name"];
			string cat_description = (string)cat["description"];
			
			UnitCategoryType uct = new UnitCategoryType(cat_name,cat_description);
			//Debug.Log(uct);
			
			unitCategories[cat_name] = uct;
		
			// For each category, load the possible choices
			Dictionary<string,object> tmpDict =  Json.loadJsonFromResources(cat_name);
			List<object> tmpList = ((List<object>) tmpDict[cat_name]);
			foreach(Dictionary<string,object> choice in tmpList){
				string choice_name = (string)choice["name"];
				string choice_description = (string)choice["description"];
			
				UnitCategoryInstance uci = new UnitCategoryInstance(choice_name, choice_description);
				//Debug.Log("  " + uci);	
				
				uct.instances.Add(uci);
			}			
		
			
		}
		
		// Test: print all
		print();
		
		
		// Test: extract a random everything
		foreach (UnitCategoryType uct in unitCategories.Values){
			Debug.Log(getRandomInstance(uct.name));	
		}
		
		/*
		"Barbarian"
		"Paladin"
		"Berserker"
		"Hoplite"
		"Gladiator"
		"Heavy Infantry"
		"Knight"
		"Assassin"
		"Thief"
		"Ninja"
		"Swordmaster"
		"Gambler"
		"Acrobat"
		"Fusilier"
		"Gunman"
		"Sniper"
		
		"Alchemist"
		"Pyromancer"
		"Idromancer"
		"Geomancer"
		"Druid"
		"Draconic"
		"Blood Mage"
		"Warlock"
		"Battlemage"
		"Bishop"
		"Cleric"
		"Guard"
		"Spartan"
		"Butcher" <-- trait?
		""
		*/
	}
	
	void print(){
		foreach (UnitCategoryType uct in unitCategories.Values){
			Debug.Log (uct);	
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public UnitCategoryInstance getInstance(string type, int i){
		return unitCategories[type].instances[i];
	}
	
	public UnitCategoryInstance getRandomInstance(string type){
		return getInstance(type, Random.Range(0,getSize(type)));	
	}
	
	public int getSize(string type){
		return unitCategories[type].instances.Count;
	}
}
