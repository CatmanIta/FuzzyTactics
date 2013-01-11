using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionType{ IDLE, MOVE, ATTACK};

public class GameUnit : GameElement {
	
	private string nname;
	private string surname;
	private string title = "";
	
	// The categories this Unit belongs to
	private Dictionary<string,UnitCategoryType> categories;
	
	private int level;
	
	private Stats stats;
	private Stats statsIncrement;
	
	
	private FuzzyRule[] rules;
	
	
	private Inventory equips;
	
	
	//private int rangeMove = 3;
	//private int rangeAttack = 1;

	 
	private Vector2 facing = new Vector2(0,0);
	
	
	void Start () {
		level = 0;
		stats = new Stats();
		
		
		rules = new FuzzyRule[10]; // Behaviour rules
		
		
		// Initialise categories
		//categories = new Dictionary<string,UnitCategoryType>();
		//categories["class"] = Classes.getClass(Random.Range(0,Classes.size()));
		//categories["race"] = Races.getRace(Random.Range(0,Races.size()));
		
		// Select class
		//_class = Classes.getClass(Random.Range(0,Classes.size()));
		
		// Select race
		//_race = Races.getRace(Random.Range(0,Races.size()));

		
		nname = "Lamba";
		surname = "Baran";
		title = "Sir";
		
		// Initial stats
		//stats = categories["class"].stats + categories["race"].stats;
		//statsIncrement = categories["class"].stats;
		
		printInfo();
		
		levelUp();
		levelUp();
		
		printInfo();
		
		
		Global.time.registerUnit(this);
	
		Global.terrainUtils.snapToChunk(transform);
		
		equips = new Inventory();
		equips.weapon_main = new Weapon("Sword","A simple sword",1,4,WeaponType.SWORD);

	}
	
	public GameObject bulletPrefab;
	void shootBullet(GameElement target){
		Vector3 dist = target.transform.position - transform.position;
		if (dist.x >0) facing.x = 1;
		if (dist.x <0) facing.x = -1;
		if (dist.x == 0) facing.x = 0; 
		if (dist.z >0) facing.y = 1;
		if (dist.z <0) facing.y = -1; 
		if (dist.z == 0) facing.y = 0;
		
		GameObject bulletObj = Instantiate (bulletPrefab) as GameObject;
		bulletObj.transform.position = transform.position;
		bulletObj.GetComponent<Bullet>().init(new Vector3(facing.x,1,facing.y),equips.weapon_main.range); // TODO not always full weapon range when attacking
	}
	
	string toString(){
		return name + " " + surname;
	}
	
	void printInfo(){
		Debug.Log (title + " " + nname + " " + surname + " the " + categories["race"].name + " " + categories["class"].name);
		Debug.Log (stats);
	}
	
	
	void levelUp(){
		stats += statsIncrement;
	}
	

	// Adds a behaviour rule
	void addRule(){
		
	}
	
	private float selfTime = 0.0f;
	void Update () {
		
		selfTime += Time.deltaTime;
		
		
		if (selfTime > 1.0f) {
			selfTime -= 1.0f;
			
			// Test movement
			//transform.position += Vector3.forward*1.0f;
		

			
		}
		
		// Movement
		//transform.position += (new Vector3(1,0,0))*Time.deltaTime;
	
		
		if (Input.GetKeyDown("w")){
			move(0,1);	
		}
		if (Input.GetKeyDown ("s")){
			move(0,-1);
		}
		if (Input.GetKeyDown ("a")){
			move(-1,0);
		}
		if (Input.GetKeyDown ("d")){
			move(1,0);
		}
		
		if (Input.GetKeyDown ("l")){
			Global.terrainUtils.highlightZone(transform,1,ActionType.MOVE);	
		}
		else if (Input.GetKeyUp ("l")){
			Global.terrainUtils.resetHighlights();	
		}
		
		if (Input.GetKeyDown ("k")){
			Global.terrainUtils.highlightZone(transform,3,ActionType.MOVE);
		}
		else if (Input.GetKeyUp ("k")){
			Global.terrainUtils.resetHighlights();	
		}
	}
	
	
	void move(int dx, int dz){
		if (dx >0) facing.x = 1;
		if (dx <0) facing.x = -1;
		if (dx == 0) facing.x = 0;
		if (dz >0) facing.y = 1;
		if (dz <0) facing.y = -1; 
		if (dz == 0) facing.y = 0;
		
		transform.position += (new Vector3(dx,0,dz))*32;
		Global.terrainUtils.snapToChunk(transform);
	}
	
	// Do a single action step
	public void doStep(){
		
		//Debug.Log("DOING STEP");
		
		switch(action){			
		case ActionType.MOVE:
			
			// For now, just move towards the target(direct jump)
			int distX = (int) (target.transform.position.x - transform.position.x);
			int dirX = 0;
			if (distX != 0) dirX = distX/Mathf.Abs(distX);
			
			int distZ = (int) (target.transform.position.z - transform.position.z);
			int dirZ = 0;
			if (distZ != 0) dirZ = distZ/Mathf.Abs(distZ);
			
			//Debug.Log("DistX: " + distX + " dirX " + dirX + " DistZ: " + distZ + " dirZ " + dirZ);
			
			if (dirX == 0 && dirZ == 0){
				action = ActionType.IDLE;
				
				// After moving, attack
				Global.terrainUtils.highlightZone(transform,equips.weapon_main.range,ActionType.ATTACK);
				
				//endedTurn = true;
				//endTurn();
			}
			else if (Mathf.Abs(distX) > Mathf.Abs(distZ)){	// We are farther on the X axis
				move(dirX,0);		
			} else {
				move(0,dirZ);
			}
			
			break;
			
		case ActionType.ATTACK:
			// Shoot a bullet at the target!
			shootBullet(target);
			action = ActionType.IDLE;
			break;
			
		case ActionType.IDLE:
			break;
		}
	}
	
	private GameElement target;
	private ActionType action = ActionType.IDLE;
	public void setTarget(GameElement target, ActionType newAction){
		this.target = target;	
		if(target is Highlight){
			this.action = newAction;	
			Global.terrainUtils.resetHighlights();	
		}
	}
	
	override public void selectElement(){
		base.selectElement();
		Global.terrainUtils.highlightZone(transform,stats.basics["move"],ActionType.MOVE);
	}
	
	
	private bool endedTurn = true;
	// TODO
	// Keep acting until the end of its turn
	public void processTurn(){
		while(!endedTurn){
			doStep ();	
		}
	}
	
	void endTurn(){	
	}
	
	
	
}