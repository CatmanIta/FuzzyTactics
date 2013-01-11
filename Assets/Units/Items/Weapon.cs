using UnityEngine;
using System.Collections;

// A generic weapon, Equipment for the hands

public enum WeaponType{SWORD, BOW, SPEAR, AXE, CLAWS, NATURAL, CROSSBOW, POLEARM};

public class Weapon : Equipment {
	
	public int range{get; protected set;}
	public int damage{get; protected set;}
	public WeaponType type{get; protected set;}
	
	//public Enchantment enchantment;
	
	public Weapon(	string name, 
					string description,
					int range,
					int damage,
					WeaponType type):base(name,description){
		this.range = range;
		this.damage = damage;
		this.type = type;
	}
	
	
}
