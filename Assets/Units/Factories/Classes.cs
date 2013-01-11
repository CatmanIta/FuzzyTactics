using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Singleton that has all classes

public class Classes : MonoBehaviour {
	
	public static Classes instance;
	
	//List<Class> classes;
	
	void Awake () {
		
		// Singleton
		if (instance) {
			Destroy (this.gameObject);	
			return;
		}
		instance = this;
		
		
		/*classes = new List<Class>();
		
		Stats st = new Stats();
		classes.Add(new Class("Warrior","A basic meelee unit, able to wield many different weapons"));
		classes[0].stats = st;
		
		classes.Add(new Class("Mage","A magic user with deep understanding of the arcane arts"));
		
		classes.Add(new Class("Archer","A basic ranged unit, able to use bows with high accuracy"));
		*/
		
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
		/*
		foreach (Class cl in classes){
			Debug.Log (cl.name + ": " +cl.description);	
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	
	/*static public Class getClass(int i){
		return instance.classes[i];	
	}
	
	static public int size(){
		return instance.classes.Count;
	}
	*/
}
