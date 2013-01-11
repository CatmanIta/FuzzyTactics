using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour {
	public static TerrainUtils terrainUtils;
	public static Selector selector;
	public static InputController input;
	public static TimeController time;
	
	public static GUISkin commanderSkin;
	
	void Awake () {
		if (GameObject.Find("Terrain")) terrainUtils = GameObject.Find ("Terrain").GetComponent<TerrainUtils>();
		selector = gameObject.AddComponent<Selector>();
		input = gameObject.AddComponent<InputController>();
		time = gameObject.AddComponent<TimeController>();
		
		
		commanderSkin = Resources.Load("CommanderGUI") as GUISkin;
	}
	
	void Update () {
	
	}
	
	
	// Fast print
	public static void p(string str){
		Debug.Log(str);
	}
}
