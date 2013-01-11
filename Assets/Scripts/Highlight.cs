using UnityEngine;
using System.Collections;

public class Highlight : GameElement {
	
	private ActionType action; // Action to be perfomed when clicking this
	
	void Start (){
	
	}
		
	void Update (){
	
	}
	
	public void setColor(Color col){
		renderer.material.color = col;
	}
	
	public void setAction(ActionType action){
		this.action = action;
	}
	
	// Send a unit here
	override public void selectElement(){
		if (Global.selector.selectedElement is GameUnit){
			//Debug.Log("Sending " + Global.selector.selectedElement.name + " to a new cell");
			(Global.selector.selectedElement as GameUnit).setTarget(this,action);
			action = ActionType.IDLE;
		}
	}
}

