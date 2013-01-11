using UnityEngine;
using System.Collections;

public class FuzzyGUIOutput : FuzzyGUIVariable {
	
	public float min;
	public float max;
	
	// Use this for initialization
	void Start () {
		variable = new FuzzyVariable(name,min,max);	
		
		renderer.material.color = new Color(0,1,0);
	}
	
}
