using UnityEngine;
using System.Collections;

// An instance of a Unit Category Type

public class UnitCategoryInstance : Attribute  {
	
	public UnitCategoryInstance(string name, 
				string description):base(name,description){
	}
	
	// Belonging to this category gives the unit bonus stats
	public Stats stats;
	
	// Belonging to this category gives the unit access to skills
	public Skill[] skills; 
}
