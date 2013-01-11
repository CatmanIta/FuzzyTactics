using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Singleton that has all races

public class Races : MonoBehaviour {
	
	public static Races instance;
	
	//List<Race> races;
	
	void Awake () {
		
		// Singleton
		if (instance) {
			Destroy (this.gameObject);	
			return;
		}
		instance = this;
		
		
		/*races = new List<Race>();
		
		
		races.Add(new Race("Human","Common inhabitant of the plains"));
		
		races.Add(new Race("Beast","Fearsome creature of the wild"));
		
		races.Add(new Race("Undead","The walking dead"));
		*/
	}
	
	void print(){
		/*foreach (Race rc in races){
			Debug.Log (rc);	
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	/*
	static public Race getRace(int i){
		return instance.races[i];	
	}
	
	static public int size(){
		return instance.races.Count;
	}
	*/
}
