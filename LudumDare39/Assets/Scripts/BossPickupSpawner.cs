using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPickupSpawner : MonoBehaviour {

    public GameObject energyPack;

    public float spawnDistance = 50.0f;
    public float spawnerCurrent = 0;
    public bool reversing = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

        GameObject[] pickupCheck = GameObject.FindGameObjectsWithTag("Energy");

        if (pickupCheck.Length != 0) {
			
		} 
    else {

            if (spawnerCurrent <= 150 && reversing == false)
            {
                spawnerCurrent += spawnDistance;
            }
            else if (spawnerCurrent > 0 && reversing == true)
            {
                spawnerCurrent -= spawnDistance;
            }
            else
            {
                reversing = !reversing;
            }
                Vector3 newPosition = transform.position;
                newPosition.x += spawnerCurrent;
                Instantiate(energyPack, newPosition, Quaternion.identity);
            }
		}
	}
