using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplodeOnContact : MonoBehaviour {
    private GameObject player;
    private float playerDist;
    private Rigidbody2D RB;
    public GameObject deathEffect;
    public float triggerRadius = 3.0f;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        playerDist = Vector3.Distance(RB.transform.position, (player.transform.position));
        Debug.Log(playerDist);
        if (playerDist < triggerRadius)
        {
            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Unit scriptin = player.GetComponent<Unit>();
            scriptin.playerHealthChange(-10);
            Destroy(this.gameObject);
        }
    }
}
