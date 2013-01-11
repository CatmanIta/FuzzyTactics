using UnityEngine;
using System.Collections;

public class FuzzyGUIAdjective : FuzzyGUIElementMov {
	
	override public void Awake(){
		base.Awake();	
		
		typeId = GUIElementType.ADJECTIVE;
		canAttachTo[(int)GUIElementType.SUBJECT] = true;
		
		textMesh.text = vName;
		
	}
}
