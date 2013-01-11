using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Presents an array of Fuzzy GUI elements to choose from

// V1.1 - no slots now, directly templates. has a GUI.
// V1.0 - has slots with templates in them. when a template is clicked, a GUI block is instantiated.

public class FuzzyGUIRepository : FuzzyGUIElement {
	
	protected List<FuzzyGUITemplate> templates = new List<FuzzyGUITemplate>();
	
	
	override public void Awake(){
		base.Awake();
		
		Destroy(textMesh);	// No text
		//textMesh.transform.localPosition = new Vector3(-20,30,0);
		//textMesh.characterSize = 12;
		
		
		/*GameObject slotPrefab = Resources.Load("GUISlotPrefab") as GameObject;
		
		for (int i=0; i<MAX_SLOTS; i++){
			GameObject slotGo = Instantiate(slotPrefab) as GameObject;
			slotGo.transform.parent = transform;
			slotGo.transform.localPosition = new Vector3(X_START,Y_START+Y_SPAN*i,0);
			FuzzyGUISlot slot = slotGo.GetComponent<FuzzyGUISlot>();
			slot.vName = i+"";
			slot.addListener(i,this);
			
			slots.Add(slot);
		}*/
		
		
	}
	
	// Adds a template to the repository
	public void addTemplate(GameObject prefab, string name){
		FuzzyGUITemplate template = ((GameObject)Instantiate(FuzzyGUISystem.templatePf)).GetComponent<FuzzyGUITemplate>();
		template.setElementPrefab(prefab);
		template.setElementName(name);
		template.transform.parent = transform;
		templates.Add(template);
	}
	
	override public void updateName(){
		textMesh.text = this.vName + ":";	
	}
	

	// Visibility
	/*override public void show(){
		base.show();
		foreach (FuzzyGUISlot slot in slots){
			slot.gameObject.SetActiveRecursively(true);
		}
	}
	
	override public void hide(){
		base.hide();
		foreach (FuzzyGUISlot slot in slots){
			slot.gameObject.SetActiveRecursively(false);
		}
	}*/
	
	
	
	// GUI
	int selectedBox = 0;
	int lastSelectedBox = 0;
	string[] selStrings;

	public void showGUI(Rect area){
		selStrings = new string[templates.Count];
		for(int i=0;i<templates.Count;i++){
			selStrings[i] = templates[i].vName;
		}	
		
		// Show the different boxes that can be used
		selectedBox = GUI.SelectionGrid (new Rect (area.x, area.y, area.width,area.height), selectedBox, selStrings, 4);
		
		// When a box is clicked, create a building block of that type
		if (selectedBox != lastSelectedBox){
			templates[selectedBox].createAndDragElement();	
		}
			
		lastSelectedBox = selectedBox;		
	}
}
