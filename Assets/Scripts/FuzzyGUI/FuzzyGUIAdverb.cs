using UnityEngine;
using System.Collections;

public class FuzzyGUIAdverb : FuzzyGUIElementMov {
	
	override public void Awake(){
		base.Awake();	
		
		typeId = GUIElementType.ADVERB;
		canAttachTo[(int)GUIElementType.ADJECTIVE] = true;
		
		textMesh.text = vName;
		
	}
}
