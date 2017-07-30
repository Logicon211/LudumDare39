using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPickupSpawner : MonoBehaviour {

    public GameObject energyPack;

    bool spawnRight = true;
    public float spawnDistance = 65.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

        GameObject[] pickupCheck = GameObject.FindGameObjectsWithTag("Energy");

        if (pickupCheck.Length != 0) {
			
		} else {

            if (spawnRight == true)
            {
                Vector3 newPosition = transform.position;
                newPosition.x += spawnDistance;
                Instantiate(energyPack, newPosition, Quaternion.identity);
                spawnRight = false;
            }
            else
            {
                Instantiate(energyPack, transform.position, Quaternion.identity);
                spawnRight = true;
            }
		}
	}
}
