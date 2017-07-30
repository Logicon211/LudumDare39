using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndistructableTile : MapTile {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void TakeDamage(int damage, bool refreshColliderIfDestroyed) {
		//Don't take damage
		int test = 2;
	}
}
