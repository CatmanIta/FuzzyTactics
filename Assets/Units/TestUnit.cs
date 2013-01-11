using UnityEngine;
using System.Collections;

public class TestUnit : MonoBehaviour {
	
	private Unit unit;
	public UnitCategoryFactory ucf;
	
	// Use this for initialization
	void Start () {
		
		unit = new Unit();
		unit.init(ucf);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
