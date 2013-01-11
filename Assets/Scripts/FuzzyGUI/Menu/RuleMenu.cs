using UnityEngine;
using System.Collections;

public class RuleMenu : Menu {
	
	FuzzyRule rule;
	
	FuzzyGUIRepoMenu repository;

	int sp = 10;	// Span between different parts
	
	// Size factors
	float titleH = 0.1f;
	float contentH;
	
	float ruleTextH = 0.2f;
	float ruleBuildH;
	
	float ruleReposH = 0.4f;
	float ruleBuild2H;
	

	public void Awake(){
		
		contentH = 1-titleH;
		ruleBuildH = 1-ruleTextH;
		ruleBuild2H = 1- ruleReposH;
	
		
		// TEST
		rule = new FuzzyRule("AA",Random.Range(1,10));
		
		GameObject repoGo = new GameObject("Repository");
		repoGo.transform.parent = transform;
		repoGo.transform.position = transform.position;
		
		repository = (FuzzyGUIRepoMenu)repoGo.AddComponent("FuzzyGUIRepoMenu");
		
	}
	
	
	override public void showGUI(){
		base.showGUI();

		// Gets all screen
		beginGUILayer(0,0,0,Screen.width,Screen.height);
		
			// Menu title
			GUI.Label(new Rect(0,0, cw[0], ch[0]*titleH), "Build the rule...");

			// Menu contents
			beginGUILayer(1,0,ch[0]*titleH,cw[0],ch[0]*contentH);
			
				// On top: the text of the current rule
				beginGUILayer(2,0,0,cw[1],ch[1]*ruleTextH, sp);
					//rule.showGUItext(new Rect(0,0,cw[2],ch[2]), false);
				endGUILayer();
		
				// Down: the rule-building screen
				beginGUILayer(2,0,ch[1]*ruleTextH,cw[1],ch[1]*ruleBuildH, sp);
					
					// On the top: the actual building screen
					beginGUILayer(3,0,0,cw[2],ch[2]*ruleBuild2H, sp);
						//GUI.Box(new Rect(0,0, cw[3],ch[3]),"RULE BUILDING");
		
						// Show the current rule structure
						//rule.showGUIstructure(new Rect(0,0,cw[3],ch[3]));
					endGUILayer();			
		
					// On the bottom: the repository of building blocks
					beginGUILayer(3,0,ch[2]*ruleBuild2H,cw[2],ch[2]*ruleReposH, sp);
						//GUI.Box(new Rect(0,0, cw[3],ch[3]),"RULE REPOS");		
		
						repository.showGUI(new Rect(0,0, cw[3],ch[3]));
					endGUILayer();
		
				endGUILayer();
				
				
				
		
			endGUILayer();
			
			
		endGUILayer();
	}
	
}
