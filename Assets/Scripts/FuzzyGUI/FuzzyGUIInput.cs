using UnityEngine;
using System.Collections;

public class FuzzyGUIInput : FuzzyGUIVariable {
	
	public float min = 0;
	public float max = 10;
		
	// Use this for initialization
	void Start () {
		variable = new FuzzyVariable(name,min,max);	
		
		renderer.material.color = new Color(1,0,0);
		
		addMf("Short");
		
		FuzzyMembershipFunction mf = addMf("Long");
		mf.setOrigin(5);
		mf.setSlope(2);
		mf.setWidth(4);
		
		mf = addMf("Absurd");
		mf.setOrigin(8);
		mf.setSlope(5);
		mf.setWidth(1);
		
	}
	
	override public void selectElement(){
		base.selectElement();
		showMfs();
	}
	
	override public void deselectElement(){
		base.deselectElement();
		//hideMfs();	
	}
	
	
	// TODO these should be shared with all elements
	static float VERTICAL_SPAN = 20.0f;
	static float HORIZONTAL_SPAN = 5.0f;
	
	public void showMfs(){
		int i = 0;
		foreach(Transform child in transform){
//			Debug.Log("Showing MF " + i);
			if (child.gameObject.GetComponent<FuzzyGUIMembershipFunction>()) child.gameObject.GetComponent<FuzzyGUIMembershipFunction>().setTarget( Vector3.right*HORIZONTAL_SPAN- Vector3.up*i*VERTICAL_SPAN);	
			i++;
		}
	}
	
	public FuzzyMembershipFunction addMf(string label){
		FuzzyMembershipFunction mf = variable.addMf(label);
		
		//mfs[mfsCount] = new MembershipFunction(label,this);
		
		GameObject mfGo = new GameObject("MF");
		mfGo.transform.parent = transform;
		//mfGo.transform.localPosition = Vector3.zero;
		FuzzyGUIMembershipFunction mfGUI =  mfGo.AddComponent<FuzzyGUIMembershipFunction>();
		mfGUI.setMf(mf);
		mfGUI.setTarget(Vector3.zero);
		//mfGo.AddComponent<LineRenderer>();
		
		return mf;
	}
	
	
}
