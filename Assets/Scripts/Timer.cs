using UnityEngine;
using System.Collections;

// Timer based on game minutes

public class Timer  {
	public int speed{get; private set;} 
	public int max{get; private set;} 
	public int time{get; private set;}  
	
	public bool on{get; private set;} 
	public bool triggered{get;private set;}
	
	public Timer(int max, int speed){ 
		this.max = max;
		this.speed = speed;
		reset();
	}
	public Timer(int max):this(max,1){}
	 
	
	public void reset(){
		time = 0;
		on = false;
		triggered = false;
	}
	
	
	 
	public void restart(){
		reset();
		on = true;
	} 
	
	public void pause(){
		on = false;	
	}
	
	public void resume(){
		on = true;	
	}
	
	/*public void update () {
		if(on){
			time += speed*Time.deltaTime;
			if (time >= max) trigger();
		}
	}*/
	
	public void increase () {
		if(on){
			time += speed;
			if (time == max) trigger();			
		}
	}
	
	public void  trigger(){
		//Debug.Log("Trigger shot!");    
		on = false;
		triggered = true;
	}

}
