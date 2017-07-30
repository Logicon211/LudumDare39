using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe1 : MonoBehaviour {
	GameObject player;
    public GameObject Orc;
    public float cooldownTimer = 0.0f;
    public float cooldownTime = 3.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer += Time.fixedDeltaTime;

    }



	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player" && cooldownTimer > cooldownTime) {
			Unit scriptin = player.GetComponent<Unit> ();
			scriptin.playerHealthChange (-10);
            cooldownTimer = 0.0f;
		}

    }
}