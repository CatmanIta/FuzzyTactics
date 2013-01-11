using UnityEngine;
using System.Collections;

// Has the fuzzy form of a rule
// Can connect elements to form the rule!

// V 1.1 - TODO should be dynamic!!! 
// V 1.0

public class FuzzyGUIRule : FuzzyGUIElement {
	
	public int ruleNumber = 1;	// To be specified for each rule
	
	private FuzzyGUISlot inSubSlot;
	private FuzzyGUISlot inAdjSlot;
	private FuzzyGUISlot outSubSlot;	
	private FuzzyGUISlot outAdjSlot;

	private static int X_SPAN = 30;
	
	public void Awake(){
		base.Awake();
		
		//textMesh.characterSize = 12;
		//textMesh.transform.localPosition = new Vector3(0,20,0);
		
		
		GameObject slotPrefab = Resources.Load("GUISlotPrefab") as GameObject;
		
		
		// INPUT
		
		// The subject
		GameObject slotGo;
		slotGo = Instantiate(slotPrefab) as GameObject;
		slotGo.transform.parent = transform;
		slotGo.transform.localPosition = new Vector3(0,0,0);
		inSubSlot = slotGo.GetComponent<FuzzyGUISlot>();
		inSubSlot.addListener(0,this);
		inSubSlot.setAttachTo(GUIElementType.SUBJECT);
		inSubSlot.setName("InSub");
		
		// The adjective
		slotGo = Instantiate(slotPrefab) as GameObject;
		slotGo.transform.parent = transform;
		slotGo.transform.localPosition = new Vector3(X_SPAN*1,0,0);
		inAdjSlot = slotGo.GetComponent<FuzzyGUISlot>();
		inAdjSlot.addListener(1,this);
		inAdjSlot.setAttachTo(GUIElementType.ADJECTIVE);
		inAdjSlot.setName("InAdj");
		
		
		
		// OUTPUT
		
		// The subject
		slotGo = Instantiate(slotPrefab) as GameObject;
		slotGo.transform.parent = transform;
		slotGo.transform.localPosition = new Vector3(X_SPAN*2,0,0);
		outSubSlot = slotGo.GetComponent<FuzzyGUISlot>();
		outSubSlot.addListener(2,this);
		outSubSlot.setAttachTo(GUIElementType.SUBJECT);
		outSubSlot.setName("OutSub");
		
		// The adjective
		slotGo = Instantiate(slotPrefab) as GameObject;
		slotGo.transform.parent = transform;
		slotGo.transform.localPosition = new Vector3(X_SPAN*3,0,0);
		outAdjSlot = slotGo.GetComponent<FuzzyGUISlot>();
		outAdjSlot.addListener(3,this);
		outAdjSlot.setAttachTo(GUIElementType.ADJECTIVE);
		outAdjSlot.setName("OutAdj");
	
		parseRule();
	}
	
	
	public string getParsedRuleText(){
		string txt = "Order " + ruleNumber;	// The rule number
		
		// The subject of the rule
		if (inSubSlot.isFull()){
			txt += " "+inSubSlot.getElement().vName;
		} else {
			txt += " ...";
			return txt;
		}
		txt += " is";
		
		// The adjective
		if (inAdjSlot.isFull()){
			txt += " "+inAdjSlot.getElement().vName;
		} else {
			txt += " ...";
			return txt;
		}
		
		txt += " then";
		
		// The target
		if (outSubSlot.isFull()){
			txt += " "+outSubSlot.getElement().vName;
		} else {
			txt += " ...";
			return txt;
		}
		
		txt += " is";
		
		// The target
		if (outAdjSlot.isFull()){
			txt += " "+outAdjSlot.getElement().vName;
		} else {
			txt += " ...";
			return txt;
		}
		
		return txt;
	}
	
	// Parse the elements in the slots to determine the rule
	public void parseRule(){
		setName(getParsedRuleText());
	}
	
	
	// Listen for slot events
	 public void slotWasFilled(int slotId, FuzzyGUIElement element){
		parseRule();	
	}
	
	
	
	// 2D GUI
	public void showGUI(){
		
	}
	
	
	
		
	// Priority determines the rule's weight
	int priority = 1;
	void setPriority(int pr){
		priority = pr;
	}
	
	
	
	// Show the rule as text
	float ruleButtonW = 0.15f;
	public void showGUItext(Rect area, bool editable){
		if (editable){
			GUI.Button(new Rect(area.x,area.y,area.width*ruleButtonW,area.height), ">"+priority);
			
		} else {
			GUI.Label(new Rect(area.x,area.y,area.width*ruleButtonW,area.height), ">"+priority);
		}	
		GUI.Label(new Rect (area.x+area.width*ruleButtonW,area.y,area.width*(1-ruleButtonW),area.height), "If Derp is Sad then Attack Enemy");
	}
	
	
	
	// Slot 
	FuzzyGUISlot slot;
	
	// Show the building blocks of the rule
	public void showGUIstructure(Rect area){
		
		// Show the first slot
		//slot.showGUI();
	}
	
}
