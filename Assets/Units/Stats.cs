using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Stats determine the characteristics of a Unit
public class Stats {
	
	// Basic stats
	public Dictionary<string,int> basics;
	
	// Extension stats
	public Dictionary<string,int> extens;

	public Stats(){
		basics = new Dictionary<string, int>();
		extens = new Dictionary<string, int>();
	}
	
  	public static Stats operator +(Stats a, Stats b)
	{
		Stats c = new Stats();
		foreach (string key in c.basics.Keys){
			c.basics[key] = a.basics[key]+b.basics[key];
		}
		foreach (string key in c.extens.Keys){
			c.extens[key] = a.extens[key]+b.extens[key];
		}
	    return c;
	}
	
}