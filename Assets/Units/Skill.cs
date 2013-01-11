using UnityEngine;
using System.Collections;

// A generic skill, i.e. something a Unit can do

public class Skill : Attribute {
	
	public Skill(string name, string description):base(name,description){
		
	}
	
	private int level;	// Level needed to acquire the skill
	
	
	virtual public void doEffect(){
		// Do the effect of the skill
	}
}
