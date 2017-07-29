using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplodeOnImpact : MonoBehaviour {
    private GameObject player;
    public GameObject deathEffect;
    private Rigidbody2D RB;
    public float blastRadius = 8.0f;
    public bool hurtsPlayer = false;
    public int damageValue = -10;

    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Rocket")
        {
            IProjectile projectile = (IProjectile)col.gameObject.GetComponent(typeof(IProjectile));
            if (projectile != null)
            {
                projectile.OnActorHit();

                    Instantiate(deathEffect, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Enemy"))
                {

                    float currentDist = Vector3.Distance(RB.transform.position, (thing.transform.position));
                    if (currentDist < blastRadius)
                    {
                        IDamagable scriptin = thing.GetComponent<IDamagable>();
                        scriptin.damage(damageValue);
                    }
                    
                }
                float playerDistance = Vector3.Distance(RB.transform.position, (player.transform.position));
                {
                    if (playerDistance < blastRadius)
                    {
                        IDamagable scriptin = player.GetComponent<IDamagable>();
                        scriptin.damage(damageValue);
                    }
                }

            }

        }
        //Check for player collision, blow him up and he loses or whatever
        //		if (col.gameObject.tag == "Player")
        //		{
        //			ship = col.gameObject.GetComponent<Ship>();
        //			ship.takeHit();
        //			kill();
        //
        //		}
    }
}
