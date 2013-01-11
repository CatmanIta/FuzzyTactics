using UnityEngine;
using System.Collections;

public class FuzzyGUIProperty : FuzzyGUIElementMov {
	
	override public void Awake(){
		base.Awake();	
		
		typeId = GUIElementType.PROPERTY;
		canAttachTo[(int)GUIElementType.SUBJECT] = true;
		
		textMesh.text = vName;
		
	}
}
