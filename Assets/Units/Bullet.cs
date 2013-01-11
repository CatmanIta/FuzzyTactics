using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	Vector3 dir = new Vector3(1,4,1);
	int force = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void init(Vector3 dir, int force){
		this.dir = dir;
		this.force = force;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		// Move bullet
		transform.position += force*dir*Time.fixedDeltaTime*TerrainUtils.CHUNK_SIZE;
		
		// Gravity
		dir.y -= 2*Time.fixedDeltaTime;
	}
}
