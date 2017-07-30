using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour {
    private GameObject player;
    bool spawnRight = false;
    public GameObject bear;
    public GameObject ghost;
    public float spawnDistance1 = 66.0f;
    private float initialPosition;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = player.transform.position.x;
    }


	void Update() {
        GameObject[] pickupCheck = GameObject.FindGameObjectsWithTag("Energy");

        if (pickupCheck.Length != 0)
        {
            Debug.Log("I'm still running!");
        }
        else
        {

            if (player.transform.position.x < (initialPosition + 35))
            {
                Vector3 newPosition = transform.position;
                newPosition.x += spawnDistance1;
                Instantiate(bear, newPosition, Quaternion.identity);
                newPosition.x += 2.0f;
                newPosition.y += 16.0f;
                Instantiate(ghost, newPosition, Quaternion.identity);

                spawnRight = false;
            }
            else
            {
                Vector3 newPosition = transform.position;
                Instantiate(bear, newPosition, Quaternion.identity);
                newPosition.x -= 2.0f;
                newPosition.y += 16.0f;
                Instantiate(ghost, newPosition, Quaternion.identity);
                spawnRight = true;
            }
        }
    }
}
