using UnityEngine;
using System.Collections;

public class Item : Attribute {
	
	// The economic value of the item
	public int ivalue{get; protected set;}
	
	public Item(string name, 
				string description
				):base(name,description){
	}
		
}
