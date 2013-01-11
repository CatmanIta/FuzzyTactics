using UnityEngine;
using System.Collections;

public class SpriteObject : MonoBehaviour {
	
	public LinkedSpriteManager spriteManager;
	
	// Use this for initialization
	void Start () {
		spriteManager.AddSprite(gameObject,100,100,0,0,10,10,false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
