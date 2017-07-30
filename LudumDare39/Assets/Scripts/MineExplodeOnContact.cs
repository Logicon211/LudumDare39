using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplodeOnContact : MonoBehaviour {
    private GameObject player;
    private float playerDist;
    private Rigidbody2D RB;
    public GameObject deathEffect;
    public float blastRadius = 6.0f;
    public int damageValue = 10;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        playerDist = Vector3.Distance(RB.transform.position, (player.transform.position));
        if (playerDist < blastRadius)
        {
            IDamagable scriptin = player.GetComponent<IDamagable>();
            scriptin.damage(damageValue);
        }
        foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Enemy"))
        {

            float currentDist = Vector3.Distance(RB.transform.position, (thing.transform.position));
            if (currentDist < blastRadius)
            {
                IDamagable scriptin = thing.GetComponent<IDamagable>();
                scriptin.damage(damageValue);
            }

        }
        Instantiate(deathEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);




    }
}
