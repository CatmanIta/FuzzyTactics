using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * A Fuzzy variable used in Fuzzy logic.
 * Holds the variable value and a set of FMFs the value is to be compared to.
 * 
 * V 1.0
 * 
 * Copyright Michele Pirovano 2012-2013
 */

public class FuzzyVariable {
	
	// Properties
	public string name{get; protected set;}	// Name given to the variable
	public float min{get; protected set;}	// Minimum value for the variable
	public float max{get; protected set;}	// Maximum value for the variable
	public float crisp{get; protected set;}	// The current crisp value of the variable
	
	//static int MAX_FMFS = 7;	// Maximum number of FMFs for a Fuzzy Variable

	public Dictionary<string,FuzzyMembershipFunction> mfs = new Dictionary<string,FuzzyMembershipFunction>();
	
	
	public FuzzyVariable(string name, float min, float max){
		this.name = name;
		this.min = min;
		this.max = max;
		
		mfs = new Dictionary<string, FuzzyMembershipFunction>();
		
		//addMf("Short");
	}
	
	// Adds a FMF to this FV
	public FuzzyMembershipFunction addMf(string label){
		FuzzyMembershipFunction mf = new FuzzyMembershipFunction(label,this);
		mfs[label] = mf;
		return mf;
    }
	
	public FuzzyMembershipFunction addMf(string label,float slope,float origin,float width){
		FuzzyMembershipFunction mf = new FuzzyMembershipFunction(label,this,slope,origin,width);
		mfs[label] = mf;
		return mf;
    }
	// Set the value of this variable
	public void setCrispValue(float crisp){
		this.crisp = Mathf.Clamp(crisp,min,max);	
	}
	
	public float getCrispValue(){
		return this.crisp;
	}
	
	// Evaluate the variable with its current crisp value for the chosen label. Returns the membership value for the corresponding MFS.
	public float getFuzzyValue(string label){
		return mfs[label].getFuzzyMembership(this.crisp);
	}
	
	public FuzzyMembershipFunction getMf(string label){
		return mfs[label];	
	}
	
	override public string ToString(){
		string s = this.name;
		foreach(FuzzyMembershipFunction mf in this.mfs.Values){
			s+= " \n " + mf;	
		}
		return s;
	}
}
