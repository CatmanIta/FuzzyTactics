using UnityEngine;
using System.Collections;

// GUI of a membership function
// v1.0 - Visualization through a line renderer

public class FGUIMembershipFunction : FGUIElement {
	
	private static int MAX_X = 5; // Length of the visualization
	private static int MAX_Y = 1; // Height of the visualization
	
	private FuzzyMembershipFunction mf;
	public LineRenderer line;
	
	override public void Awake(){
		base.Awake();
		line.useWorldSpace = false;
	}
	
	public void setMf(FuzzyMembershipFunction mf){
		setMf(mf,FGUICompression.COMPRESSED);		
	}
	public void setMf(FuzzyMembershipFunction mf, FGUICompression compression){
		this.mf = mf;
		this.name = mf.label;
		this.compression = compression;
		line.SetVertexCount(mf.N);
		textMesh.text = mf.label;
		
	}
	
	public void compress(){ 
		this.compression = FGUICompression.COMPRESSED;
		textMesh.text = mf.ToString();
	}
	
	public void extend(){
		this.compression = FGUICompression.EXTENDED;
		
		int N = mf.x.Length;
		for (int i = 0; i<N; i++){
			line.SetPosition(i,new Vector3(mf.x[i]*MAX_X/mf.x[N-1],mf.y[i]*MAX_Y,0));	
		}
	}
	
		
	override public void Update () {
		base.Update();
		
		if (mf == null) return;
		
	}
}
