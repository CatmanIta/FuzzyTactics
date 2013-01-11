using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * A Fuzzy rule used in Fuzzy logic.
 * Has a set of inputs and an output, with corresponding labels.
 * It works in the form (IF input IS labelIn THEN output IS labelOut)
 *
 * V 1.0
 * 
 * Copyright Michele Pirovano 2012-2013
 */

public class FuzzyRule {
	public string name;
	
	//static int MAX_INPUTS = 3;
	//static int MAX_OUTPUTS = 1;
	
	List<FuzzyVariable> inputs;
	List<string> labelsIn;
	List<FuzzyVariable> outputs;	// TODO: put variables and labels in Pair instances
	List<string> labelsOut;
	public float weight{get; protected set;}
	
	
	public FuzzyRule(string name):this(name,1){}
	public FuzzyRule(string name, float weight){
		this.name = name;
		this.weight = weight;
		
		inputs = new List<FuzzyVariable>();
		outputs = new List<FuzzyVariable>();
		
		labelsIn = new List<string>();
		labelsOut = new List<string>();
	}
	
	// If V is L ...
	public void addAntecedent(FuzzyVariable v, string l){
		this.inputs.Add(v);
		this.labelsIn.Add(l);
	}
	
	// .. then V is L.
	public void addConsequent(FuzzyVariable v, string l){
		this.outputs.Add(v);
		this.labelsOut.Add(l);
	}
	
	// TODO Check the validity of the rule
	void check(){
		
	}
	
	public string getFuzzyLabelIn(int index){
		return labelsIn[index];	
	}
	
	public string getFuzzyLabelOut(int index){
		return labelsOut[index];	
	}
	
	public float getFuzzyValueIn(int index){
		return inputs[index].getFuzzyValue(labelsIn[index]);	
	}
	
	public float getFuzzyValueOut(int index){
		return outputs[index].getFuzzyValue(labelsOut[index]);	
	}
	
	public float getCrispValueIn(int index){
		return inputs[index].getCrispValue();	
	}
	
	public float getCrispValueOut(int index){
		return outputs[index].getCrispValue();	
	}
	
	public FuzzyVariable getFuzzyVariableIn(int index){
		return inputs[index];	
	}
	
	public FuzzyVariable getFuzzyVariableOut(int index){
		return outputs[index];	
	}
	
	
	public int getInputsNumber(){
		return inputs.Count;	
	}
	
	public int getOutputsNumber(){
		return outputs.Count;
	}
	
	
	override public string ToString(){
		string s = this.name + " - IF ";
		for(int i=0; i<this.inputs.Count; i++){
			s += inputs[i].name + " is " + labelsIn[i];
			if (i != this.inputs.Count-1) s+= " AND ";
		}
		s += " THEN ";
		for(int i=0; i<this.outputs.Count; i++){
			s += outputs[i].name + " is " + labelsOut[i];
			if (i != this.outputs.Count-1) s+= " AND ";
		}	
		return s;
	}
	
}