using UnityEngine;
using System.Collections;

// Equipment for the body

public enum ArmorType{SWORD, BOW, SPEAR, AXE, CLAWS, NATURAL, CROSSBOW, POLEARM};


public class Armor : Equipment {
	
	public int range{get; protected set;}
	public int damage{get; protected set;}
	public WeaponType type{get; protected set;}
		
	public Armor(	string name, 
					string description):base(name,description){
	}
	

}
