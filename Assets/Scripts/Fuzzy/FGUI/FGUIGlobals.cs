using UnityEngine;
using System.Collections;

public class FGUIGlobals : MonoBehaviour {

	public static GameObject textMeshPrefab;
	
	public static GameObject prefabFGUIMembershipFunction;
	public static GameObject prefabFGUIVariable;
	public static GameObject prefabFGUIRule;
	public static GameObject prefabFGUISystem;
	
	void Awake () {
		textMeshPrefab = (GameObject)Resources.Load("3DTextPrefab");
		prefabFGUIMembershipFunction = (GameObject)Resources.Load("FGUIMembershipFunction");
		prefabFGUIVariable = (GameObject)Resources.Load("FGUIVariable");
		prefabFGUIRule = (GameObject)Resources.Load("FGUIRule");
		prefabFGUISystem = (GameObject)Resources.Load("FGUISystem"); 
	}
	
	
	
	// Factories
	
	public static FGUIMembershipFunction createFGUIMembershipFunction(FuzzyMembershipFunction mf){
		GameObject go = (GameObject)Instantiate(prefabFGUIMembershipFunction);
		FGUIMembershipFunction gui = go.GetComponent<FGUIMembershipFunction>();
		gui.setMf(mf);
		return gui;
	}
	
	public static FGUIVariable createFGUIVariable(FuzzyVariable v){
		GameObject go = (GameObject)Instantiate(prefabFGUIVariable);
		FGUIVariable gui = go.GetComponent<FGUIVariable>();
		gui.setVariable(v);
		return gui;
	}
	
	public static FGUIRule createFGUIRule(FuzzyRule r){
		GameObject go = (GameObject)Instantiate(prefabFGUIRule);
		FGUIRule gui = go.GetComponent<FGUIRule>();
		gui.setRule(r);
		return gui;
	}
	
	public static FGUISystem createFGUISystem(FuzzySystem system){
		GameObject go = (GameObject)Instantiate(prefabFGUISystem);
		FGUISystem gui = go.GetComponent<FGUISystem>();
		gui.setSystem(system);
		return gui;
	}
}
