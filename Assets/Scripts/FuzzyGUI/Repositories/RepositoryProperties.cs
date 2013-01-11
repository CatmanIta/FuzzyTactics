using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepositoryProperties : FuzzyGUIRepository {
	

	override public void Awake(){
		base.Awake();
		
		setName("Properties");
		
	}
	
	override public void Start(){
		base.Start();
		
		// Add the templates
		addTemplate(FuzzyGUISystem.propertyPf,"Health");
		addTemplate(FuzzyGUISystem.propertyPf,"Distance");
		addTemplate(FuzzyGUISystem.propertyPf,"Range");

	}
	
	
}
