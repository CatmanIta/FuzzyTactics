using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.IO;

public class FuzzyTEST : MonoBehaviour {	
	private FuzzySystem system;	
	private Dictionary<string,FuzzyVariable> variables; // TODO: maybe put this in the Fuzzy system?
	
	
	public void Start(){
		this.system = new FuzzySystem("TestFuzzySystem");
		this.variables = new Dictionary<string, FuzzyVariable>();
		
		testRead();
		
		testGUI();
		
		//testFuzzy();
		
		//testWrite();
		
		
	}
	
	private void testRead(){
		// Read a fuzzy system from Fuzzy Tactics Data files
		Dictionary<string,object> dictVariables =  Json.loadJsonFromFolder("Assets/Scripts/Fuzzy/json/variables");
		Dictionary<string,object> dictRules = Json.loadJsonFromFolder("Assets/Scripts/Fuzzy/json/rules");
		
		
		// Inference engine
		this.system.setEngine(new FuzzyInferenceEngine());
		
		// Variables
	 	//((List<object>) dict["array"])[0]);
		List<object> listVariables = ((List<object>) dictVariables["variables"]); 

		foreach (Dictionary<string,object> variable in listVariables){
			float min = (float)((double)variable["min"]);
			float max = (float)((double)variable["max"]);
			
			FuzzyVariable newVar = new FuzzyVariable((string)variable["name"],min,max);
			
			List<object> mfs = ((List<object>) variable["mfs"]); 
			foreach (Dictionary<string,object> mf in mfs){
				float origin = (float) ((double)mf["origin"]);
				float slope = (float) ((double)mf["slope"]);
				float width = (float) ((double)mf["width"]);
				newVar.addMf((string)mf["label"],slope,origin,width);
			}
			
			this.variables[newVar.name] = newVar;
			
			print("Added new variable " + newVar);
			
		}
		
		// Rules
		List<object> listRules = ((List<object>) dictRules["rules"]); 
		
		foreach (Dictionary<string,object> rule in listRules){
			float weight = (float)((double)rule["weight"]);
			
			FuzzyRule newRule = new FuzzyRule((string)rule["name"], weight);
			
			List<object> antecedents = ((List<object>) rule["antecedents"]);
			foreach (Dictionary<string,object> antecedent in antecedents){
				newRule.addAntecedent(this.variables[(string)antecedent["variable"]], (string)antecedent["label"]);	
			}
				
			List<object> consequents = ((List<object>) rule["consequents"]);
			foreach (Dictionary<string,object> consequent in consequents){
				newRule.addConsequent(this.variables[(string)consequent["variable"]], (string)consequent["label"]);	
			}	
				
			this.system.addRule(newRule);
			
			Debug.Log("Added new rule: " + newRule);
		}
		
		// Inferral
		//this.system.infer();
		
		
		
	}

	private void testGUI(){
		int i=0;
		
		
		// Create a suitable GUI for the loaded system
		FGUISystem gui = FGUIGlobals.createFGUISystem(this.system);
		gui.transform.position = Vector3.zero;
		gui.transform.parent = transform;
		gui.extend();
		gui.extendAllChildren();
		
		
		/*
		foreach(FuzzyRule r in this.system.rules){
			FGUIRule gui = FGUIGlobals.createFGUIRule(r);
			gui.transform.position = Vector3.up*i;
			gui.transform.parent = transform;	
			//gui.setCompact();
			
			gui.extend();
			
			gui.extend();
			
		}*/
		
		/*foreach(FuzzyVariable v in this.variables.Values){
			FGUIVariable gui = FGUIGlobals.createFGUIVariable(v);
			gui.transform.position = Vector3.up*i;
			gui.transform.parent = transform;	
			
			gui.setExtended();
		*/	
			/*
			foreach(FuzzyMembershipFunction mf in v.mfs.Values){
				FGUIMembershipFunction gui = FGUIGlobals.createFGUIMembershipFunction(mf);
				gui.transform.position = Vector3.up*i*1;				
				i++;
			}*/
		
		/*i+=4;
		}*/
		
	}

	
	private void testWrite(){
		
	}
	
	
	
	
	private void testFuzzy(){
		// Creates a working Fuzzy system via code
		
		// Inference engine
		this.system.setEngine(new FuzzyInferenceEngine());
		
		// Variables
		FuzzyVariable vHealth = new FuzzyVariable("Health",0.0f,10.0f);
		vHealth.addMf("Low");	// Should also be able to specify slope and origin
		vHealth.addMf("Mid");
		vHealth.addMf("High");
		
		FuzzyVariable vDistance = new FuzzyVariable("Distance",0.0f,10.0f);
		vDistance.addMf("Low");
		vDistance.addMf("Mid");
		vDistance.addMf("High");
		
		FuzzyVariable vFear = new FuzzyVariable("Fear",0.0f,10.0f);
		vFear.addMf("Low");
		vFear.addMf("Mid");
		vFear.addMf("High");
		
		
		// Rules
		FuzzyRule rule = new FuzzyRule("Test1");		
		rule.addAntecedent(vHealth,"Low");
		rule.addAntecedent(vDistance,"Low");
		rule.addConsequent(vFear,"High");
		this.system.addRule(rule);
		
		// Inferral
		this.system.infer();
	}
	
	
	
}