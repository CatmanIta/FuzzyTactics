using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepositorySubjects : FuzzyGUIRepository {
	

	override public void Awake(){		
		base.Awake();
		
		setName("Subjects");
	}
	
	override public void Start(){
		base.Start();
		
		// Fill the slots
		addTemplate(FuzzyGUISystem.subjectPf,"Unit");	
		addTemplate(FuzzyGUISystem.subjectPf,"Enemy");	
		addTemplate(FuzzyGUISystem.subjectPf,"Object");	
		
	}
	
	
}
