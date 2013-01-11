using UnityEngine;
using System.Collections;

// Controller of mouse input
 
public class InputController : MonoBehaviour {	
	
	private Ray ray;
	private RaycastHit[] hits;
	private RaycastHit hit;
	public bool noPenetration= false; // With no penetration, only the first hit is processed 
	private static bool avoidGUIpenetration = true; // Avoids penetration when a GUI element is around
	
	private float lastClick = 0;
	private float doubleClickSensitivity = 0.3f;
	
	public void Start(){
		
	}
	
  
	
	void leftClick(RaycastHit hit){
		if (Input.GetMouseButtonDown(0)) hit.transform.SendMessage("mouseClick",hit,SendMessageOptions.DontRequireReceiver);
	}
	void leftDoubleClick(RaycastHit hit){
		if (Input.GetMouseButtonDown(0)){
			if ((Time.time - lastClick) < doubleClickSensitivity){
				hit.transform.SendMessage("mouseDoubleClick",hit,SendMessageOptions.DontRequireReceiver);
	        	lastClick = -1;
			} else lastClick = Time.time;	  
		}
		
	}
	void rightClick(RaycastHit hit){
		if (Input.GetMouseButtonDown(1)) hit.transform.SendMessage("mouseRightClick",hit,SendMessageOptions.DontRequireReceiver);
	}
	void mouseOver(RaycastHit hit){
		hit.transform.SendMessage("mouseOver",hit,SendMessageOptions.DontRequireReceiver);
	}
	
	
	void processHit(RaycastHit hit){
		
		Debug.DrawLine (ray.origin, hit.point);
		
		//print("Hitting:" + hit.transform);
	
		mouseOver(hit);
		
//		if (!(avoidGUIpenetration && Global.mouseOverGUI)) { // No raycast over GUI
			leftClick(hit);
			rightClick(hit);			
//		}
					
	}
	
	void  Update (){
		
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		

		if (noPenetration) {
			// Raycast up to 10000 meters down
			if (Physics.Raycast (ray, out hit, 10000.0f)) {		
				processHit(hit);
			}   
				
		} else {
			hits = Physics.RaycastAll (ray, 10000.0f);
			if (hits.Length > 0) Debug.DrawLine (ray.origin, hits[0].point);
			
			// Sorts the hits from the closest to the farthest
			System.Array.Sort(hits, new RaycastSorter());
			System.Array.Reverse(hits);

			for (int i = 0; i < hits.Length; i++){
				processHit(hits[i]);
			}
		
		}
	
	
	
	}
}

// http://answers.unity3d.com/questions/22261/sorting-builtin-arrays.html
class RaycastSorter : IComparer{
    public int Compare( System.Object a,  System.Object b)  {
       // if ( !(a as RaycastHit) || !(b as RaycastHit)) return;
        RaycastHit raycastHitA = (RaycastHit) a;
        RaycastHit raycastHitB = (RaycastHit) b;

        return raycastHitA.distance.CompareTo(raycastHitB.distance);
    }
}

/*
class RaycastSorter : IComparer{
    int Compare ( System.Object a ,   System.Object b  ){
        if ( ( a as RaycastHit == null)  || ( b as RaycastHit == null)) return;
        RaycastHit raycastHitA = (RaycastHit)a;
        RaycastHit raycastHitB = b;     
 
        return raycastHitA.distance.CompareTo(raycastHitB.distance);
    }
}
 */