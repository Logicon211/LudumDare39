using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe1 : MonoBehaviour {
	GameObject player;
    public GameObject Orc;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Unit scriptin = player.GetComponent<Unit> ();
			scriptin.playerHealthChange (-10);
		}
	}
}