using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {

	private bool floatUp;
	float driftAmount;

	// Use this for initialization
	void Start () {
		floatUp = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (floatUp) {
			
			this.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.025f, 0);
			driftAmount += 0.05f;
			if (driftAmount > 1) {
				floatUp = false;
				driftAmount = 0;
			}
		}
		else{
			this.transform.position = new Vector3 (transform.position.x, transform.position.y - 0.025f, 0);
			driftAmount += 0.05f;
			if (driftAmount > 1) {
				floatUp = true;
				driftAmount = 0;
			}
		}


	}

	void OnCollisionEnter2D(Collision2D col) {
		//This isn't calling for some reason
		//Test for projectile collision
		Debug.Log ("collided with something");
		if (col.gameObject.tag == "Player") {
			Debug.Log ("collided with player");
			if (this.tag == "PowerUp") {
				TrackMouse scriptin = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<TrackMouse> ();
				scriptin.upgradeWeapon ();
			} else if (this.tag == "Energy") {
				Unit scriptin = GameObject.FindGameObjectWithTag ("Player").GetComponent<Unit> ();
				scriptin.playerEnergyChange (40f);
			} else if (this.tag == "Health") {
				Unit scriptin = GameObject.FindGameObjectWithTag ("Player").GetComponent<Unit> ();
				scriptin.playerHealthChange (40);
			}
			Destroy (this.gameObject);
		}



	}

}
