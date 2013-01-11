using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * A Fuzzy engine used in Fuzzy logic for inference.
 * Given a set of rules, evaluates them.
 *
 * V 1.0
 * 
 * Copyright Michele Pirovano 2012-2013
 */

public enum FuzzificationMethod{WEIGHTED,MAX,MIN};
public enum DefuzzificationMethod{WEIGHTED,MAX,MIN};
//public enum FuzzyWeightingMethod{WEIGHTED,MAX,MIN};
//public enum FuzzyCombinationMethod{WEIGHTED,MAX,MIN};


public class Pair<T, U> {
    public Pair() {
    }

    public Pair(T first, U second) {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};
        

public class FuzzyInferenceEngine {
	
	const FuzzificationMethod fuzzMethod = FuzzificationMethod.MAX;
	const DefuzzificationMethod defuzzMethod = DefuzzificationMethod.MIN;
	protected bool verbose = false;
	
	public FuzzyInferenceEngine(){  	
		this.verbose = true;
	}
	
	
	public void evaluate(FuzzyRule[] rules){ 
		Dictionary<FuzzyRule,float> ruleResults = new Dictionary<FuzzyRule,float>();
		
		// Evaluating rules
		// Mamdani approach
		if(this.verbose) Debug.Log("Evaluating rules:");
		// For each Rule
        foreach(FuzzyRule rule in rules){
            float[] fuzzyVals = new float[rule.getInputsNumber()]; 
			
            
            if(this.verbose) Debug.Log("Checking rule " + rule.name);
            
            // Input matching of all antecedents
			for (int i=0; i<rule.getInputsNumber(); i++){
				fuzzyVals[i] = rule.getFuzzyValueIn(i);
				if(this.verbose) Debug.Log("Input " + rule.getFuzzyVariableIn(i).name + " has value " + rule.getCrispValueIn(i) + " and thus is " + rule.getFuzzyLabelIn(i) + " at " + fuzzyVals[i]);
			}   
			            
            // Combination of matching degrees // Using MIN
            float combinationVal = Mathf.Min(fuzzyVals);
            if(this.verbose) Debug.Log("Combining with MIN: " + combinationVal);
            
            // Combination with rule's weight // Using MIN
            combinationVal = Mathf.Min(combinationVal,rule.weight);
            if(this.verbose) Debug.Log("Weighting with MIN: " + combinationVal);
            
        	// Save the output result
            ruleResults[rule] = combinationVal;
			if(this.verbose) Debug.Log("Result: " + combinationVal);
  		}
    
		// Output aggregation // Using MAX
		if(this.verbose) Debug.Log("Aggregating outputs:");
		
		Dictionary<Pair<FuzzyVariable,string>,float> outDictionary = new Dictionary<Pair<FuzzyVariable,string>,float>();
        foreach(FuzzyRule rule in rules){
			Pair<FuzzyVariable,string> outPair = new Pair<FuzzyVariable,string>();
            outPair.First = rule.getFuzzyVariableOut(0);
			outPair.Second = rule.getFuzzyLabelOut(0);
				
            if (!(outDictionary.ContainsKey(outPair))){ 
                outDictionary[outPair] = ruleResults[rule];
			} else {
                outDictionary[outPair] = Mathf.Max(outDictionary[outPair],ruleResults[rule]); 
            }
			if (this.verbose) Debug.Log("Key " + outPair + " is " + outDictionary[outPair]);
		}               

        
			
        // Defuzzification
		if(this.verbose) Debug.Log("Defuzzification:");  
		
        Dictionary<FuzzyVariable,float> defuzzifiedResults = new Dictionary<FuzzyVariable,float>();
        Dictionary<FuzzyVariable,float> defuzzifiedDenominator = new Dictionary<FuzzyVariable,float>();

		foreach(KeyValuePair<Pair<FuzzyVariable,string>,float> kvp in outDictionary){
			FuzzyVariable variable = kvp.Key.First;
			string label = kvp.Key.Second;
			float result = kvp.Value;

            if(this.verbose) Debug.Log("Output " + variable + "-" + label + " is " + result);
            
			if (!(defuzzifiedResults.ContainsKey(variable))){
				defuzzifiedResults[variable] = 0;
				defuzzifiedDenominator[variable] = 0;
			}
			
            if (defuzzMethod == DefuzzificationMethod.WEIGHTED) {
                // Weighted Media
                defuzzifiedResults[variable] += variable.getMf(label).getOrigin()*result;
                defuzzifiedDenominator[variable] += result;
			} else if (defuzzMethod == DefuzzificationMethod.MAX) {     
                if (result >= defuzzifiedResults[variable]) {   
                    defuzzifiedResults[variable] = Mathf.Max(result,defuzzifiedResults[variable]);
                    //result = variable.mfs[label].getOrigin();
				}
			}
		}
        
		foreach(KeyValuePair<FuzzyVariable,float> kvp in defuzzifiedResults){
			FuzzyVariable variable = kvp.Key;
			float result = kvp.Value;	
				
			if (defuzzMethod == DefuzzificationMethod.WEIGHTED){ 
	            if (result != 0) result = 0;
	            else result = defuzzifiedResults[variable]/defuzzifiedDenominator[variable];
				
				defuzzifiedResults[variable] = result;
			}	
			if(this.verbose) Debug.Log("Output " + variable.name + " is " + result);
			
		}
        
        
        
				
	}
	
	// Old version. Just needs a name, input and output to define the rule.
	// I now use a custom rule version for this game
	/*public FuzzyRule(string name, VariableIn input, VariableOut output){
		this.name = name;
		this.input = input;
		this.output = output;
	}*/
	
	
	// TODO Check the validity of the rule
	void check(){
		
	}
	

	
}