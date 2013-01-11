using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// World time controller

public class TimeController : MonoBehaviour {
	
	static private List<Timer> timers;
	static private List<GameUnit> units;
	
	public int turn{get; private set;}
	public int step{get; private set;}
	
	
	public bool newTurn = false;
	public bool newStep = false;
	
	public static int maxTurns = 1000;
	
	
	public static float stepLength = 0.1f; // Length of a minute in seconds
	
	public float totTime = 0.0f; // Always advancing
	public float turnTime = 0.0f;
	public float stepTime = 0.0f;
	
	public int currentUnit = 0; // Keeps track of current acting unit
	
	
	void Awake(){
		timers = new List<Timer>();	
		units = new List<GameUnit>();
	}
	
	void Update(){
	}

	// All functions in time with this must be put in FixedUpdate!
	void  FixedUpdate (){
	
		if( Application.isEditor) {
			if (Input.GetKey("space")) Time.timeScale = 10;
			else Time.timeScale = 1;     
	  	} else {
	  		if (Input.GetKey("space")) Time.timeScale = 5;  
			else Time.timeScale = 1;      
	
	    }
	    
		float dt = Time.fixedDeltaTime;
		
		totTime += dt;  
		turnTime += dt;
		stepTime += dt;
		
		// All units acting together
		if (stepTime >= stepLength){	
			stepTime -= stepLength;
			currentUnit = 0;
			foreach (GameUnit u in units){
				u.doStep();	
			}
			newStep = true;
		}
		
		//newTurn = false;
		 
		/*
		// New minute
		if (minuteTime >= minuteLength){
			minuteTime = 0;
			minute++;
			newMinute = true;
			foreach (Timer t in timers) t.increase();	
		} */  
		
		
	} 
	
	 
	public void registerUnit(GameUnit u){
		units.Add(u);
	}
	
	void OnGUI(){
		if (Application.isEditor){
			
		/*	GUI.Label(new Rect(300,300,100,50),
			          "H " + hour + ": M " + minute  
			          +"\n"+(day+1)+"/"+(month+1)+"/"+year
			          +"\n"+season);
			 */
		}
		
	}
	
	
	public static Timer addTimer(int max){
		Timer t = new Timer(max);
		timers.Add(t);
		return t;
	}
	  
	public static void removeTimer(Timer t){
		timers.Remove(t);
	}
	  

}