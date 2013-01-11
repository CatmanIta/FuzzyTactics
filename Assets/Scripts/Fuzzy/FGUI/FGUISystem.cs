using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// GUI of a fuzzy system
// Two visualizations
// - Compact: shows the system with its name
// - Expanded: shows the system with its rules laid out
//
// v1.0
// Copyright Michele Pirovano 2012-2013


public class FGUISystem : FGUIElement {
	
	private FuzzySystem system;
	
	public List<FGUIRule> listFGUIRules;
	
	override public void Awake(){
		base.Awake();
		
		this.textMesh.transform.localPosition = new Vector3(-1,1,0);
	}
	
	public void setSystem(FuzzySystem system){
		this.system = system;
		this.name = system.name;
	}
		
	override public void Update () {
		base.Update();
	}
	
	
	
	public void compress(){ 
		this.compression = FGUICompression.COMPRESSED;
		
		textMesh.text = system.ToString();
		
		compressAllChildren();
	}
	
	public void extend(){ 
		
		if (this.compression == FGUICompression.COMPRESSED){
			// First extension
			this.compression = FGUICompression.EXTENDED;
		
			textMesh.text = system.name;
			
			if (listFGUIRules.Count == 0) createChildren();
				
			
		} else {
			// Second extension: extend children	
		
			//extendAllChildren();
		}
		
	}
	
	public void createChildren(){
		// Rules
		for(int i=0; i<system.getRulesNumber(); i++){
			FGUIRule gui = FGUIGlobals.createFGUIRule(system.getRule(i));
			gui.transform.parent = transform;	
			gui.transform.localPosition = -Vector3.up*i*2;
			
			listFGUIRules.Add(gui);
		}		
	}
	
	
	public void extendAllChildren(){
		foreach (FGUIRule r_gui in listFGUIRules){
			r_gui.extend();
		}
	}
	
	public void compressAllChildren(){
		foreach (FGUIRule r_gui in listFGUIRules){
			r_gui.compress();
		}
	}
}
