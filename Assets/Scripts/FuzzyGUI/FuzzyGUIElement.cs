using UnityEngine;
using System.Collections;

// A generic Fuzzy GUI element

public enum GUIElementType { SUBJECT, COMPLEMENT, PROPERTY, VARIABLE, ADJECTIVE, ADVERB}

public class FuzzyGUIElement : GameElement {
	
	public GUIElementType typeId{protected set; get;}

	protected TextMesh textMesh;

	protected BoxCollider GUIcollider;
	
	public bool[] canAttachTo = new bool[5];
	
	// Returns true if this element can attach to an element of type "type"
	virtual public void setAttachTo(GUIElementType type){
		canAttachTo[(int)type] = true;	
	}
	
	virtual public void Awake(){
		
		
		// TODO do the resource load only once!
		GameObject textGo = Instantiate(Resources.Load("3DTextPrefab") as GameObject) as GameObject;
		this.textMesh = textGo.GetComponent<TextMesh>();
		this.textMesh.transform.parent = transform;
		this.textMesh.transform.localPosition = Vector3.zero;
		this.textMesh.characterSize = 5;
		this.textMesh.alignment = TextAlignment.Left;
		this.textMesh.anchor = TextAnchor.UpperLeft;
		
		
		GUIcollider = gameObject.GetComponent<BoxCollider>();
		if (!GUIcollider) GUIcollider = gameObject.AddComponent<BoxCollider>();
		
		for (int i= 0; i < canAttachTo.Length; i++) canAttachTo[i] = false;
		
	}
	
	virtual public void Start(){}
	
	virtual public void Update () {
	}
	
	override public void  mouseRightClick (){
		FuzzyGUISystem.addConnection(Global.selector.selectedElement as FuzzyGUIElement,this);
	}
	
	// Update the text name
	virtual public void updateName(){
		this.textMesh.text = this.vName;	
	}
	
	virtual public void setName(string newName){
		this.vName = newName;
		updateName();
	}
	
	virtual public void slotWasFilled(int slotId, FuzzyGUIElement element){
		// Abstract
	}
	
	
	
	virtual public void show(){
		if (this.textMesh) this.textMesh.gameObject.active = true;	
	}
	
	virtual public void hide(){
		if (this.textMesh) this.textMesh.gameObject.active = false;
	}
	
}
