using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Holds the representation of a Fuzzy system as a GUI

// There should be a separation of the actual FUZZY SYSTEM from the GUI. Only this GUI element can handle the system's variables and create GUI accordingly.s

public class FuzzyGUISystem : MonoBehaviour {
	
	
	static public GameObject subjectPf;
	static public GameObject complementPf;
	static public GameObject adjectivePf;
	static public GameObject adverbPf;
	static public GameObject propertyPf;
	static public GameObject templatePf;
	
	
	static private TextMesh textMesh;
	
	void Awake(){
		GameObject textGo = Instantiate(Resources.Load("3DTextPrefab") as GameObject) as GameObject;
		textMesh = textGo.GetComponent<TextMesh>();
		textMesh.transform.parent = transform;
		textMesh.transform.localPosition = Vector3.zero;
		textMesh.characterSize = 20;
		
		
		textMesh.text = "If...";
		
		// Static load of prefabs
		subjectPf = (GameObject)Resources.Load("FuzzyGUISubjectPrefab");
		complementPf = (GameObject)Resources.Load("FuzzyGUIComplementPrefab");
		adjectivePf = (GameObject)Resources.Load("FuzzyGUIAdjectivePrefab");
		adverbPf = (GameObject)Resources.Load("FuzzyGUIAdverbPrefab");
		propertyPf = (GameObject)Resources.Load("FuzzyGUIPropertyPrefab");
		templatePf = (GameObject)Resources.Load("FuzzyGUITemplatePrefab");
		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	static private List<FuzzyGUIConnection> connections = new List<FuzzyGUIConnection>();
	
	// Connect two variables to create parts of rules
	// TODO: now this should be done by the FuzzyGUIRule
	static public void addConnection(FuzzyGUIElement a, FuzzyGUIElement b){
		if (b.canAttachTo[(int)a.typeId]) {
			GameObject connectionGo = new GameObject();
			connectionGo.name = "Connection";
			FuzzyGUIConnection c = connectionGo.AddComponent<FuzzyGUIConnection>();
			c.connect(a,b);
			connections.Add(c);
			
			
			// Build the text of the rule!
			if (textMesh.text == "If..."){
				textMesh.text = "If " + a.vName;
			}
			
			if (b is FuzzyGUIComplement) {
				textMesh.text += "'s " + b.vName + " is ";
			} else if (b is FuzzyGUIAdjective) {
				textMesh.text += b.vName;
			}	
			else if (b is FuzzyGUIAdverb){
				textMesh.text += b.vName;	
			}

		}
	}
}
