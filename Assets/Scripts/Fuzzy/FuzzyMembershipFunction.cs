using UnityEngine;
using System.Collections;

/*
 * A membership function for Fuzzy logic.
 * Maps a continuous input for a variable [min,max] into a Fuzzy degree [0,1] and gives it a label.
 * 
 * V 1.0 - Trapezoid FMF
 * 
 * Copyright Michele Pirovano 2012-2013
 */

public class FuzzyMembershipFunction {
	
	// The label of this FMF
	public string label{get; protected set;}
	
	// The Fuzzy variable this FMF is tied to
	private FuzzyVariable fuzzy_var;
	
	
	// The values that determine the shape of this trapezoid FMF
	public Val slopeVal;
	public Val originVal;
	public Val widthVal;
	
	
	// The FMF is discretized into a set of values
	// It is thus sampled on X and Y
	// This is the same as a Response Curve (See AI Games Wisdom)
	public float[] x{get; protected set;}
	public float[] y{get; protected set;}
	
	// The resolution of the FMF, used for sampling
	static float resolution = 0.1f;
	
	// The number of points for this FMF
	public int N{get; protected set;}
	
	
	// Constructor
	// A FMF is defined for a particular Fuzzy Variable and for a specific label
	public FuzzyMembershipFunction(string label, FuzzyVariable fuzzy_var):this(label,fuzzy_var,1,0,0){}
	public FuzzyMembershipFunction(string label, FuzzyVariable fuzzy_var, float slope, float origin, float width){
		this.label = label;
		this.fuzzy_var = fuzzy_var;
		
		// Initialisation
		
		// The number of points for this FMF is determined by the resolution and the range of values for the variable
		N = Mathf.FloorToInt((fuzzy_var.max-fuzzy_var.min)/resolution)+1;
		this.x = new float[N];
		this.y = new float[N];
		
		// Default
		slopeVal = new Val(slope);
		originVal = new Val(origin);
		widthVal = new Val(width);
		recompute();
		
		//Debug.Log(this);
		//startRecomputing();
	}
	
	/*****************
	 *  Getters
	 *****************/
	
	// The value is interpolated over the samples
	public float getFuzzyMembership(float crisp){
		// Clamp
		if (crisp <= x[0]) return y[0];
		if (crisp > x[N-1]) return y[N-1];
		for(int i=0; i<N-1; i++){
			if (x[i] < crisp && crisp >= x[i+1]){
				return Mathf.Lerp(y[i],y[i+1],(x[i+1]-x[i])/(crisp-x[i]));
			}
		}
		return 0;
	}
	
	
	/*****************
	 *  Setters
	 *****************/
	
	public float getOrigin(){ return originVal.v; }
	public float getSlope(){ return slopeVal.v; }
	public float getWidth(){ return widthVal.v; }
	
	public void setSlope(float s){
		slopeVal.v = s;	
		recompute();
	}
	public void setOrigin(float o){
		if (!(x[0] <= o  &&  o <= x[N-1])) return;	
		originVal.v = o;
		recompute();
	}
	public void setWidth(float w){
		widthVal.v = w;	
		recompute();
	}
	
	// Compute the FMF, i.e. build it from the shape values
	public void  recompute(){
		//Debug.Log("RECOMPUTED MF with N " + N);
		for (int i=0; i<N; i++){
			x[i] = fuzzy_var.min + i*resolution;
			if (x[i] < originVal.v){
				y[i] = slopeVal.v*x[i] + (1-slopeVal.v*originVal.v) + widthVal.v/2;
			} else {
				y[i] = -slopeVal.v*x[i] + (1+slopeVal.v*originVal.v) + widthVal.v/2;
			}
			
			// Clamp all values to 0 and 1
			y[i] = Mathf.Clamp01(y[i]);
		}	
	}
	
	
	
	
	override public string ToString(){
		string s = this.label;
		s +=  " - " + this.N + " values ";
		s += " from " + this.x[0] + " to " + this.x[N-1];
		s += " - Origin: " + this.getOrigin() + "  Slope: " + this.getSlope() + "  Width: " + this.getWidth();
		return s;
	}
	
	
}
