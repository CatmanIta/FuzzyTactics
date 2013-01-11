using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// GUI of a fuzzy rule
// Two visualizations
// - Compact: shows the rule as its text
// - Expanded: shows the rule with its variables
//
// v1.0
// Copyright Michele Pirovano 2012-2013


public class FGUIRule : FGUIElement {
	
	private FuzzyRule rule;
	
	public List<FGUIVariable> listFGUIVariables;
	
	override public void Awake(){
		base.Awake();
		
		this.textMesh.transform.localPosition = new Vector3(-1,1,0);
	}
	
	
	public void setRule(FuzzyRule rule){
		setRule(rule,FGUICompression.COMPRESSED);	
	}
	
	public void setRule(FuzzyRule rule, FGUICompression compression){
		this.rule = rule;
		this.name = rule.name;
		this.setCompression(compression);
	}
		
	override public void Update () {
		base.Update();
		if (rule == null) return;
		
	}
	
	public void setCompression(FGUICompression compression){
		this.compression = compression;
		
	}
	
	
	public void compress(){ 
		this.compression = FGUICompression.COMPRESSED;
		textMesh.text = rule.ToString();
		compressAllChildren();
	}
	
	public void extend(){ 
		
		if (this.compression == FGUICompression.COMPRESSED){
			// First extension
			textMesh.text = rule.name;
			
			if (listFGUIVariables.Count == 0){
				createChildren();
			}
			
			
		} else {
			// Second extension: extend children	
		
			// DEPRECATED
			//extendAllChildren();
		}
		
		
		setCompression(FGUICompression.EXTENDED);
		
		
	}
	
	public void createChildren(){
		// Inputs
		for(int i=0; i<rule.getInputsNumber(); i++){
			FGUIVariable gui = FGUIGlobals.createFGUIVariable(rule.getFuzzyVariableIn(i));
			gui.transform.parent = transform;
			gui.transform.localPosition = -Vector3.up*i*2;
			listFGUIVariables.Add(gui);
		}
		
		// Outputs
		for(int i=0; i<rule.getOutputsNumber(); i++){
			FGUIVariable gui = FGUIGlobals.createFGUIVariable(rule.getFuzzyVariableOut(i));
			gui.transform.parent = transform;
			gui.transform.localPosition = Vector3.right*5 - Vector3.up*i*2;
			listFGUIVariables.Add(gui);
		}		
	}
	
	public void extendAllChildren(){
		foreach (FGUIVariable v_gui in listFGUIVariables){
			v_gui.extend();
		}
	}
	
	public void compressAllChildren(){
		foreach (FGUIVariable v_gui in listFGUIVariables){
			v_gui.compress();
		}
	}
}
