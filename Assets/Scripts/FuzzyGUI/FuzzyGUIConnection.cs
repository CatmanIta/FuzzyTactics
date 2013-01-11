using UnityEngine;
using System.Collections;

public class FuzzyGUIConnection : MonoBehaviour {
	
	private LineRenderer line;
	
	private FuzzyGUIElement a;
	private FuzzyGUIElement b;
	
	public void connect(FuzzyGUIElement a, FuzzyGUIElement b){
		line = gameObject.AddComponent<LineRenderer>();
		line.SetVertexCount(2);
		this.a = a;
		this.b = b;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (line){
			line.SetPosition(0,a.transform.position);
			line.SetPosition(1,b.transform.position);
		}
	}
	


}
