using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class FuzzyGUIMembershipFunction : FuzzyGUIElementMov {
	
	FuzzyMembershipFunction mf;
	LineRenderer line;
	Dictionary<string, FuzzyGUISlider> sliders = new Dictionary<string, FuzzyGUISlider>();
	
		
	// Use this for initialization
	override public void Awake () {
		base.Awake();
		// Don't create one from scratch because you also need to add the right material et cetera, better to just load the asset!
		/*GameObject textGo = new GameObject("Label");
		textGo.transform.parent = transform;
		textGo.transform.localPosition = Vector3.zero;
		this.textMesh = textGo.AddComponent<TextMesh>();
		textGo.AddComponent<MeshRenderer>();*/
		
		GUIcollider.size = new Vector3(100,20,1);
		GUIcollider.center = new Vector3(50,10,0);
		
	}
	
	int nSlider = 0;
	public void addSlider(string name, Val val){
		
		GameObject sliderGo = Instantiate(Resources.Load("GUISliderPrefab") as GameObject) as GameObject;
		FuzzyGUISlider tmpSlider = sliderGo.GetComponentInChildren<FuzzyGUISlider>();
		sliderGo.transform.parent = transform;
		tmpSlider.setTarget(Vector3.right*200 + (-10)*nSlider*Vector3.up );
		//tmpSlider.setReference(mf);
		tmpSlider.setReference(mf,val);
		nSlider++;
	}
	
	
	public void setMf(FuzzyMembershipFunction mf){
		this.mf = mf;
		line = gameObject.AddComponent<LineRenderer>();
		line.SetVertexCount(mf.N);
		line.useWorldSpace = false;
		textMesh.text = mf.label;
		
		
		addSlider("Origin",mf.originVal);
		addSlider("Slope",mf.slopeVal);
		addSlider("Width",mf.widthVal);
	}
	
	
	// Update is called once per frame
	private static int XSCALE = 10;
	private static int YSCALE = 20;
	
	
	override public void Update () {
		base.Update();
		Debug.Log("DRAWING MF");
		for (int i = 0; i<mf.x.Length; i++){
			line.SetPosition(i,new Vector3(mf.x[i]*XSCALE,mf.y[i]*YSCALE,0));	
		}
	}
}
