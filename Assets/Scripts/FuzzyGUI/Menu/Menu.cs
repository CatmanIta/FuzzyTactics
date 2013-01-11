using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	// Current values in group layers
	protected float[] cw = new float[5];	// Current width
	protected float[] ch = new float[5]; // Current height

	public void OnGUI(){
		showGUI();
	}
	
	virtual public void showGUI(){
		GUI.skin = Global.commanderSkin;
		
		int w = Screen.width;
		int h = Screen.height;

	}
	
	
	protected void beginGUILayer(int level, float x, float y, float w, float h){
		beginGUILayer(level,x,y,w,h,0);
	}
	protected void beginGUILayer(int level, float x, float y, float w, float h, float sp){
		GUI.BeginGroup(new Rect(x+sp,y+sp,cw[level]=w-2*sp,ch[level]=h-2*sp));
		GUI.Box(new Rect(0,0,w-2*sp,h-2*sp),"");
	}
	protected void endGUILayer(){
		GUI.EndGroup();	
	}
}
