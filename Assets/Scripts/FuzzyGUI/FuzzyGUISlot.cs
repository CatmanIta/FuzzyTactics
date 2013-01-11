using UnityEngine;
using System.Collections;

// A slot where a moving fuzzy element can be placed

public class FuzzyGUISlot : FuzzyGUIElement {
	
	FuzzyGUIElement element;
	int id;						// The ID of the slot in a sequence of slots
	FuzzyGUIElement listener;	// The listener will be notified when the slot gets filled
	
	
	override public void Awake(){
		base.Awake();	
				
	}
	
	
	
	/*void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<FuzzyGUIElementMov>()) {
			other.gameObject.GetComponent<FuzzyGUIElementMov>().setTarget(transform.position);
		}
	}*/
	
	void OnTriggerStay(Collider other){
		if (element) return;	
		
		// Snap the element to this
		FuzzyGUIElementMov tmpEl = other.gameObject.GetComponent<FuzzyGUIElementMov>(); 
		if (tmpEl && tmpEl.dragged == false) {
		
			// Check that the element can be placed here		
			if (!canAttachTo[(int)tmpEl.typeId]) return;
			
			// If it can be placed here, place it here!
			tmpEl.onSlot(this);
			//if (tmpEl.dropped == true) tmpEl.setTarget(transform.position);	
		//	snap(other.transform);
			fillSlot(tmpEl);
		}
	}
	
	/*void OnTriggerExit(Collider other){
		FuzzyGUIElementMov tmpEl = other.gameObject.GetComponent<FuzzyGUIElementMov>(); 
		if (tmpEl) {
			tmpEl.onSlot(null);
			//if (tmpEl.dropped == true) tmpEl.setTarget(transform.position);	
		//	snap(other.transform);
			element = null;
		}
	}*/
	
	public void removeElement(){
		this.element = null;
	}
	
	public FuzzyGUIElement getElement(){
		return this.element;	
	}
	
	public void fillSlot(FuzzyGUIElement el){
		el.transform.parent = transform;
		el.transform.position = transform.position; // Place on the slot (this goes against snapping?)
		
		this.element = el;	
		this.setName("");
		//updateName();
		
		if (listener) listener.slotWasFilled(id,el); // This was here to notice the rule that its slot has been filled! MUST FIND A BETTER WAY!
	
	
		print("Slot filled with element " + element.vName);
	}
	
	override public void updateName(){
		if (element) this.vName = element.vName;
		base.updateName();
	}
	
	// When this slot is filled, this will notice the listener
	public void addListener(int id, FuzzyGUIElement listener){
		this.listener = listener;
		this.id = id;
	}
	
	void snap(Transform other){
		other.position = this.transform.position;	
	}
	
	
	public bool isEmpty(){
		return this.element == null;
	}
	
	public bool isFull(){
		return !isEmpty();	
	}
	

}
