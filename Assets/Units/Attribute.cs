using UnityEngine;
using System.Collections;

// An attribute has a name and a description. It is anything that can be applied to an object in the game.
// Examples are items, tags, equipments.

public class Attribute  {
	
	public string name{get; protected set;}
	public string description{get; protected set;}
	
	public Attribute(string name, string description){
		this.name = name;
		this.description = description;
	}
	
	override public string ToString(){
		return name + ": " + description;	
	}
}
