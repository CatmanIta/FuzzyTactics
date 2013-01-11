using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// GUI of a fuzzy variable
// Two visualizations
// - Compact: shows the variable as a cube
// - Expanded: shows the variable with its mfs
//
// v1.0
// Copyright Michele Pirovano 2012-2013

public class FGUIVariable : FGUIElement {
	
	
	private FuzzyVariable v;
	
	public List<FGUIMembershipFunction> listFGUIMembershipFunctions;

	
	override public void Awake(){
		base.Awake();
	}
	
	public void setVariable(FuzzyVariable v){
		this.v = v;
		this.name = v.name;
		textMesh.text = v.name;
	}
		
	override public void Update () {
		base.Update();
		if (v == null) return;
		
	}
	
	
	
	public void compress(){ 
		this.compression = FGUICompression.COMPRESSED;
	
		compressAllChildren();
	}
	
	public void extend(){ 
		this.compression = FGUICompression.EXTENDED;
		
		if (listFGUIMembershipFunctions.Count == 0) createChildren();
		extendAllChildren();
		
	}
	
	public void createChildren(){
		// MFs
		int i=0;
		foreach(FuzzyMembershipFunction mf in v.mfs.Values){
			FGUIMembershipFunction gui = FGUIGlobals.createFGUIMembershipFunction(mf);
			gui.transform.parent = transform;
			gui.transform.localPosition = Vector3.zero;//-Vector3.up*i*2;
			i++;
		}		
	}
	
	public void extendAllChildren(){
		foreach (FGUIMembershipFunction mf_gui in listFGUIMembershipFunctions){
			mf_gui.extend();
		}
	}
	
	public void compressAllChildren(){
		foreach (FGUIMembershipFunction mf_gui in listFGUIMembershipFunctions){
			mf_gui.compress();
		}
	}
	
	
}
