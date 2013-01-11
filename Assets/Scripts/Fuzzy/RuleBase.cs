using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//namespace Fuzzy{


	/*
	 * A rule base used in Fuzzy logic, i.e. a set of Rules
	 *
	 * V 1.0
	 * 
	 * Copyright Michele Pirovano 2012-2013
	 */
		
	public class RuleBase  {
		
		//int ownerId = 1;
		List<FuzzyRule> rules;
		
		public RuleBase(){
			rules = new List<FuzzyRule>();
		}
		
		public void addRule(FuzzyRule rule){
			rules.Add(rule);
			//rules.Sort();// TODO sort according to priority
		}
		
		/*public void setOwner(int id){
			ownerId = id;	
		}
		
		float ruleH = 0.1f;
		public void showGUI(Rect area){
			float yy=0;
			
			// Rules header
			GUI.Label(new Rect (0,yy,area.width,area.height*ruleH), "Rules for unit " + ownerId);
			yy+=area.height*ruleH;
				
			// Rules
			for(int i=0;i<rules.Count;i++){
				//rules[i].showGUI(new Rect(0,yy,area.width,area.height*ruleH), true);
				yy+=area.height*ruleH;
			}
			
			// New rule button
			GUI.Button(new Rect(0,yy,area.width,area.height*ruleH),"New rule...");
		}*/
		
	}
//}