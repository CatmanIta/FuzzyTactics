using UnityEngine;
using System.Collections;

// A template for creating a fuzzy GUI element (instantiate its gameobject to create it)

public class FuzzyGUITemplate : FuzzyGUIElement {
	
	private GameObject elementGo;
	private string elName;
	
	public void setElementPrefab(GameObject go){
		elementGo = go;
	}
	
	
	public FuzzyGUIElement createElement(){
		if (elementGo){
			FuzzyGUIElement element = ((GameObject)Instantiate(elementGo,transform.position,Quaternion.identity)).GetComponent<FuzzyGUIElement>();
			element.setName(elName);
			return element;
			
		} else return null;
	}
	
	// Set the name of the usable element
	public void setElementName(string name){
		this.elName = name;
		this.vName = "+"+name;
		updateName();	
	}
	
	
	public void createAndDragElement(){
		FuzzyGUIElement newElement = createElement();
		(newElement as FuzzyGUIElementMov).startDragging();	
	}
	
	void OnMouseDown(){
		createAndDragElement();
	}
	
}
