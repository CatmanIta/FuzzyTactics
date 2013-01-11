using UnityEngine;
using System.Collections;


public class FuzzyGUISubject : FuzzyGUIElementMov {
	
	override public void Awake(){
		base.Awake();
		
		typeId = GUIElementType.SUBJECT;
		canAttachTo[(int)GUIElementType.COMPLEMENT] = true;
		
	}
	
	override public void Start(){
		base.Start();
		
		textMesh.text = vName;
	}
	
}
