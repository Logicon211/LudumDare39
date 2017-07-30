using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject bear;
	public GameObject betterBear;
	public GameObject ghost;
	public GameObject barrel;
	public GameObject mine;

	public int bearRate;
	public int betterBeartRate;
	public int ghostRate;
	public int barrelRate;
	public int mineRate;

	//Out of 100
	public int spawnChance = 10;

	public float spawnTime = 40f;
	public float timer = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (timer < spawnTime) {
			timer += Time.fixedDeltaTime;
		} else {
			timer = 0;
			int spawnRoll = Random.Range (1, 100);

			if (spawnRoll <= spawnChance) {
				int bearRoll= Random.Range (1, 100);
				int betterBeartRoll= Random.Range (1, 100);
				int ghostRoll= Random.Range (1, 100);
				int barrelRoll= Random.Range (1, 100);
				int mineRoll= Random.Range (1, 100);

				if (bearRoll <= bearRate) {
					Instantiate (bear, transform.position, Quaternion.identity);
				}
				if (betterBeartRoll <= betterBeartRate) {
					Instantiate (betterBear, transform.position, Quaternion.identity);
				}
				if (ghostRoll <= ghostRate) {
					Instantiate (ghost, transform.position, Quaternion.identity);
				}
				if (barrelRoll <= barrelRate) {
					Instantiate (barrel, transform.position, Quaternion.identity);
				}
				if (mineRoll <= mineRate) {
					Instantiate (mine, transform.position, Quaternion.identity);
				}
			}
		}
	}
}
