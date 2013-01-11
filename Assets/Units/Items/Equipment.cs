using UnityEngine;
using System.Collections;

// An equipment is an item that can be attached to a Unit

public class Equipment : Item {
	
	//public Enchantment enchantment;
	
	public Equipment(string name, 
					string description):base(name,description){

	}
	
	// Level needed to use this equipment
	private int level;	
	
}
