using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject bear;
	public GameObject betterBear;
	public GameObject ghost;
	public GameObject barrel;
	public GameObject mine;

	public GameObject energyPickup;
	public GameObject healthPickup;

	public int bearRate;
	public int betterBeartRate;
	public int ghostRate;
	public int barrelRate;
	public int mineRate;

	public int energyPickupRate;
	public int healthPickupRate;

	//Out of 100
	public int spawnChance = 10;

	public float spawnTime = 40f;
	public float timer = 0f;

	public float spawnDistanceEnabled = 200f;

	public bool spawnAfterPlayerPasses = false;
	public bool enabled = false;

	public float ampUpTime = 0f;

	private GameObject player;

	// Use this for initialization
	void Start () {
		if (spawnDistanceEnabled <= 0 && !spawnAfterPlayerPasses) {
			enabled = true;
		}
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		float playerDist = Vector3.Distance (this.transform.position, (player.transform.position));
		if (enabled == true && playerDist <= spawnDistanceEnabled) {
			if (timer < spawnTime) {
				timer += Time.fixedDeltaTime;
			} else {
				timer = 0;
				int spawnRoll = Random.Range (1, 100);

				if (spawnRoll <= spawnChance) {
					int bearRoll = Random.Range (1, 100);
					int betterBeartRoll = Random.Range (1, 100);
					int ghostRoll = Random.Range (1, 100);
					int barrelRoll = Random.Range (1, 100);
					int mineRoll = Random.Range (1, 100);
					int energyPickupRoll = Random.Range (1, 100);
					int healthPickupRoll = Random.Range (1, 100);

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
					if (energyPickupRoll <= energyPickupRate) {
						Instantiate (mine, transform.position, Quaternion.identity);
					}
					if (healthPickupRoll <= healthPickupRate) {
						Instantiate (mine, transform.position, Quaternion.identity);
					}
				}

				if (ampUpTime > 0f) {
					spawnTime -= ampUpTime;
					if (spawnTime < 6f) {
						spawnTime = 6f;
					}
				}
			}
		} else {
			//playerDist = Vector3.Distance (this.transform.position, (player.transform.position));

			if (!spawnAfterPlayerPasses) {
				if (playerDist <= spawnDistanceEnabled) {
					enabled = true;
				}
			} else {
				if (player.transform.position.x >= this.transform.position.x) {
					enabled = true;
				}
			}
		}
	}
}
