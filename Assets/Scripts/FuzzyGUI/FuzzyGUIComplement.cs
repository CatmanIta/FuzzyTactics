using UnityEngine;
using System.Collections;

public class FuzzyGUIComplement : FuzzyGUIElementMov {
	
	override public void Awake(){
		base.Awake();	
		
		typeId = GUIElementType.COMPLEMENT;
		canAttachTo[(int)GUIElementType.SUBJECT] = true;
		
		textMesh.text = vName;
		
	}
}
