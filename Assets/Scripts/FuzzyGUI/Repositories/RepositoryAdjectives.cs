using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepositoryAdjectives : FuzzyGUIRepository {

	override public void Awake(){		
		base.Awake();
		
		setName("Adjectives");
	}
	
	override public void Start(){
		base.Start();
		
		// Fill the slots
		addTemplate(FuzzyGUISystem.adjectivePf,"Far");	
		addTemplate(FuzzyGUISystem.adjectivePf,"Near");	
		addTemplate(FuzzyGUISystem.adjectivePf,"Dead");
		addTemplate(FuzzyGUISystem.adjectivePf,"Short");
		addTemplate(FuzzyGUISystem.adjectivePf,"Long");
		addTemplate(FuzzyGUISystem.adjectivePf,"High");
		addTemplate(FuzzyGUISystem.adjectivePf,"Low");
	

	
	}
	
	
}
