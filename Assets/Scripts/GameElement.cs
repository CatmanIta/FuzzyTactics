using UnityEngine;
using System.Collections;

public class GameElement : MonoBehaviour {

	protected bool selected = false;
	
	public string vName;
	
	
	
	// Mouse control 
	virtual public void  mouseOver (){}
	virtual public void  mouseClick (){
		selectElement();
	}  	
	virtual public void  mouseRightClick (){
		Global.selector.point(this);
	}

	
	// Selection
	virtual public void selectElement(){
		Global.selector.selectElement(this);
		this.selected = true;
	}
	virtual public void deselectElement(){
		this.selected = false;
	}
	public virtual bool sendSelection (GameElement selection){
		return true;
	}
	

	
}
