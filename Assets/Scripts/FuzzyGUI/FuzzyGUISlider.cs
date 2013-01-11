using UnityEngine;
using System.Collections;

public class FuzzyGUISlider : FuzzyGUIElementMov {
	
	public float min;
	public float max;		
	
	private Val reference;
	private float oldValue = 0;
	
	private FuzzyMembershipFunction mf;
	private FuzzyGUISliderHandle handle;
	
	override public void Awake(){
		base.Awake();
		
		handle = transform.Find("Handle").GetComponent<FuzzyGUISliderHandle>();
	}	
	
	public void setReference(FuzzyMembershipFunction mf, Val reference){
		handle.setReference(reference);
		this.reference = reference;
		this.mf = mf;
	}
	
	override public void Update(){
		base.Update();
		
		if (reference != null){
			if (reference.v != oldValue) mf.recompute(); // Needed for the MF to update
			oldValue = reference.v;
		}
	}

	
}
