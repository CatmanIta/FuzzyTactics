using UnityEngine;
using System.Collections;


// A Fuzzy GUI element that can be dragged around
// V1.1 - uses UnityGUI now
// V1.0

public class FuzzyGUIElementMov : FuzzyGUIElement {
	
	protected Vector3 targetPos = Vector3.zero;
	
	public bool dragged = false;
	
	private FuzzyGUISlot slot;	// The slot it is inserted into
	
	override public void Awake(){
		base.Awake();
		targetPos = transform.localPosition;			
	}
	
	override public void Update () {
		base.Update();
		
		// If dragged
		if (dragged) {

			
			// Follow the position of the mouse
			targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (transform.parent != null) targetPos -= transform.parent.position;
			targetPos.z = transform.localPosition.z;
			//	Debug.Log("Target pos: " + targetPos);
			
			// Release at any time after starting draggin by removing the mouse
			//if (Input.GetMouseButtonUp(0)) stopDragging();

		}
		
		// Converge towards the position
		transform.localPosition = Vector3.Lerp(transform.localPosition,targetPos,0.1f);
		
	}
	
	// Mouse control
	// NOTE: This won't work if the GUI is on! In that case, RepeatButton is used!
	void OnMouseDown(){startDragging();}
	void OnMouseUp(){stopDragging();}
	
	
	// Functions for dragging around the block
	public void startDragging(){
		this.dragged = true;
		selectElement();	
	}
	public void stopDragging(){
		this.dragged = false;
		deselectElement();	
	}
	public void toggleDragging(){
		if (this.dragged) stopDragging();
		else startDragging();
	}
	
	
	public void setTarget(Vector3 target){
		this.dragged = false; // Override mouse
		this.targetPos = target;
	}
	
	// When passing over a slot, make it the slot this will return to when releasing the mouse
	public void onSlot(FuzzyGUISlot slot){
		if (this.slot) this.slot.removeElement(); // Notice the last slot that this is removed
		this.slot = slot;	
		this.transform.parent = slot.transform;	// Parent to the slot!
		if (!dragged && slot) setTarget(Vector3.zero); // setTarget(slot.transform.position);
	}
	
	public void OnGUI(){
		GUI.depth = -10;	// Always on front
		
		int size = 50;
		//print("CAMERA: " + Camera.main);
		//print("TRANSFORM: " + transform);
		
		Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		screenPos.y = Screen.height-screenPos.y; // Must invert
		
		// Show the box at the correct position
		// When the user clicks on it, it gets dragged
		//GUI.Box(new Rect(screenPos.x-size/2,screenPos.y-size/2,size,size),vName);
		if (GUI.Button(new Rect(screenPos.x-size/2,screenPos.y-size/2,size,size),vName)) toggleDragging();
		//TODO: Make the button be released also when clicking outside of it!!!
	}
	

}
