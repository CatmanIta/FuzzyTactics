using UnityEngine;
using System.Collections;

// A generic Fuzzy GUI element

//public enum FGUIElementType { SUBJECT, COMPLEMENT, PROPERTY, VARIABLE, ADJECTIVE, ADVERB}

public enum FGUICompression {
	COMPRESSED = 0,			// Small description 
	EXTENDED = 1,			// Show what is inside
}


	

public class FGUIElement : GameElement {
	
	public FGUICompression compression = FGUICompression.COMPRESSED;
	
	protected TextMesh textMesh;

	virtual public void Awake(){
		
		GameObject textGo = (GameObject)Instantiate((GameObject)FGUIGlobals.textMeshPrefab);
		this.textMesh = textGo.GetComponent<TextMesh>();
		this.textMesh.transform.parent = transform;
		this.textMesh.transform.localPosition = Vector3.zero;
		this.textMesh.characterSize = 0.5f;
		this.textMesh.alignment = TextAlignment.Left;
		this.textMesh.anchor = TextAnchor.UpperLeft;
		
		
		//GUIcollider = gameObject.GetComponent<BoxCollider>();
		//if (!GUIcollider) GUIcollider = gameObject.AddComponent<BoxCollider>();
		
		//for (int i= 0; i < canAttachTo.Length; i++) canAttachTo[i] = false;
		
	}
	
	virtual public void Update(){
		
	}
}
