using UnityEngine;
using System.Collections;

public class UnitsMenu : Menu {
	
	RuleBase[] ruleBases;
	
	// Parameters
	int maxRules = 5;
	int maxUnits = 20;
	
	int sp = 10;	// Span between different parts
	
	// Size factors
	float titleH = 0.1f;
	float contentH;
	
	float unitsW = 0.4f;	// Factor
	
	
	float ruleH = 0.1f;
	float ruleButtonW = 0.15f;
	float ruleTextW;
	
	int selGridInt = 0;
	string[] selStrings;
	
	public void Awake(){
		selStrings = new string[maxUnits];
		for(int i=0;i<maxUnits;i++){
			selStrings[i] = "unit"+i;
		}	
		
		contentH = 1-titleH;
		ruleTextW = 1-ruleButtonW;
		
		
		//unitCommands = new UnitOrders[maxUnits];
		ruleBases = new RuleBase[maxUnits];
		for(int i=0;i<maxUnits;i++){
			ruleBases[i] = new RuleBase();
			//ruleBases[i].setOwner(i);
			ruleBases[i].addRule(new FuzzyRule("AA",Random.Range(1,10)));
			ruleBases[i].addRule(new FuzzyRule("BB",Random.Range(1,10)));
			ruleBases[i].addRule(new FuzzyRule("CC",Random.Range(1,10)));
		} 
		/*rules = new Rule[maxRules];
		for(int i=0;i<maxRules;i++){
			rules[i] = new Rule("aa");
		}*/	
	}

	
	override public void showGUI(){
		base.showGUI();
		
		
		// Gets all screen
		beginGUILayer(0,0,0,Screen.width,Screen.height);
		
			// Menu title
			GUI.Label(new Rect(0,0,cw[0],ch[0]*titleH), "Command your units!");
		
			// Menu contents
			beginGUILayer(1,0,ch[0]*titleH,cw[0],ch[0]*contentH);
				
				// On the left: the units
				beginGUILayer(2,0,0,cw[1]*unitsW,ch[1], sp);
					selGridInt = GUI.SelectionGrid (new Rect (0, 0, cw[2], ch[2]), selGridInt, selStrings, 4);
				endGUILayer();
		
				// On the right: the unit's rules
				beginGUILayer(2,cw[1]*unitsW,0,cw[1]*(1-unitsW),ch[1], sp);
		
					//ruleBases[selGridInt].showGUI(new Rect(cw[1]*unitsW+sp,0+sp,cw[2] = (int)(cw[1]*(1-unitsW)-sp),ch[2] =ch[1]-sp));
	
				endGUILayer();
			
		
			endGUILayer();
		
		endGUILayer();
		
	}
}
