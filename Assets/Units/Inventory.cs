using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The Inventory is the collection of items held by a Unit 

public class Inventory {
	
	// Equipment
	public Weapon weapon_main {get; set;}	// Main hand
	public Weapon weapon_off {get; set;} 	// Off hand
	public Armor armor{get; set;}
	
	// Items
	public List<Item> items;
	
	// Parameters
	private int capacity = 10;
	
	public Inventory(){
		items = new List<Item>();
	}
	
	public Inventory(int capacity):this(){
		this.capacity = capacity;
	}
	
	public bool addItem(Item i){
		if (items.Count == capacity) return false;
		items.Add(i);
		return true;	
	}
	
}
