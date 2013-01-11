using UnityEngine;
using System.Collections;

public class FuzzyGUISliderHandle : FuzzyGUIElement {
	
	public float min;
	public float max;
	
	private Val reference;
	
	override public void Awake(){
		base.Awake();
		
	}	
	
	public void setReference(Val reference){
		this.reference = reference;	
		print("added reference " + reference + " with val " + reference.v);
	}
	
	/*
	public void setReference(ref float val){
		reference = val;	
	}*/

	override public void Update(){
		base.Update();
		Vector3 tmpPos = transform.localPosition;
		tmpPos.y = 0;
		if (tmpPos.x < -50) tmpPos.x = -50;
		if (tmpPos.x > 50) tmpPos.x = 50;
		transform.localPosition = tmpPos;
		
		float val = (50+tmpPos.x)/100.0f; // [0,1]
		
//		if (reference != null)		print("Ref: " + reference.v);

		if (reference != null) reference.v = val*10;
		//if (mf != null) mf.setOrigin(val*10); // TODO depends on the variable
	}
	
}
