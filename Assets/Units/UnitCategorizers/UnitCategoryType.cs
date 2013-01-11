using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// An abstract categorization

public class UnitCategoryType : Attribute  {
	
	public List<UnitCategoryInstance> instances;
	
	public UnitCategoryType(string name, 
				string description):base(name,description){
		
		instances = new List<UnitCategoryInstance>();
	}
	
	override public string ToString(){
		string s = base.ToString();
		foreach(UnitCategoryInstance uci in instances){
			s+= "\n  " + uci;	
		}
		return s;
	}
}
