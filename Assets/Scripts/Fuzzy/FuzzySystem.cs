using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * A Fuzzy system used in Fuzzy logic.
 * Has rules and uses an inference engine to reason.
 *
 * V 1.0
 * 
 * Copyright Michele Pirovano 2012-2013
 */

public class FuzzySystem {
	public string name;
	
	private FuzzyInferenceEngine engine;
	public List<FuzzyRule> rules;
	
	public FuzzySystem(string name){
		this.rules = new List<FuzzyRule>();
		this.name = name;
	}
	
	
	public void setEngine(FuzzyInferenceEngine engine){
		this.engine = engine;
	}
	
	public void addRule(FuzzyRule rule){
		this.rules.Add(rule);	
	}
	
	public FuzzyRule getRule(int index){
		return rules[index];
	}
	
	public int getRulesNumber(){
		return rules.Count;	
	}
	
	public void infer(){
		this.engine.evaluate(rules.ToArray());	
	}
	
	

}