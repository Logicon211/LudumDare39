using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour {
    private GameObject player;
    bool spawnRight = false;
    public GameObject bear;
    public GameObject ghost;
    public float spawnDistance = 180.0f;
    private float initialPosition;
    private bool lastSpawnedWasBear = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = player.transform.position.x;
    }


	void Update() {
        GameObject[] pickupCheck = GameObject.FindGameObjectsWithTag("Enemy");

        if (pickupCheck.Length >= 2)
        {
            
        }
        else
        {
            if (lastSpawnedWasBear == true)
            {

                if (player.transform.position.x < (initialPosition + 100))
                {
                    Vector3 newPosition = transform.position;
                    newPosition.x += spawnDistance;
                    newPosition.y += 20.0f;
                    Instantiate(ghost, newPosition, Quaternion.identity);
                    lastSpawnedWasBear = false;
                }
                else
                {
                    Vector3 newPosition = transform.position;
                    newPosition.y += 20.0f;
                    Instantiate(ghost, newPosition, Quaternion.identity);
                    lastSpawnedWasBear = false;
                }
            }
            else
            {
                if (player.transform.position.x < (initialPosition + 100))
                {
                    Vector3 newPosition = transform.position;
                    newPosition.x += spawnDistance;
                    Instantiate(ghost, newPosition, Quaternion.identity);
                    lastSpawnedWasBear = true;
                }
                else
                {
                    Vector3 newPosition = transform.position;
                    Instantiate(ghost, newPosition, Quaternion.identity);
                    lastSpawnedWasBear = true;
                }
            }
        }
    }
}
