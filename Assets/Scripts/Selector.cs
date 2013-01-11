using UnityEngine;
using System.Collections;

/*
 * Selector of game elements
 */

public class Selector : MonoBehaviour{
	
	public GameElement selectedElement{get; private set;}
	
	
	public void Start(){
		selectElement(null);
	}	
	
	public void  selectElement( GameElement newSelectedElement  ){
		if (this.selectedElement) this.selectedElement.deselectElement();
		if (newSelectedElement == null) {
			this.selectedElement = null;
			return;	 
		} 
		this.selectedElement = newSelectedElement;	
	} 
	
	
	public void  point ( GameElement pointedElement  ){
		// Send the selected element to the pointed element
		// and vice-versa!
		if (selectedElement) {
			pointedElement.sendSelection(selectedElement);
			selectedElement.sendSelection(pointedElement);
		}
	}
	
}
