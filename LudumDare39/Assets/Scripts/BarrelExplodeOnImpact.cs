using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplodeOnImpact : MonoBehaviour {

    public GameObject deathEffect;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        //This isn't calling for some reason
        //Test for projectile collision

        if (col.gameObject.tag == "Rocket")
        {
            IProjectile projectile = (IProjectile)col.gameObject.GetComponent(typeof(IProjectile));
            if (projectile != null)
            {
                projectile.OnActorHit();

                    Instantiate(deathEffect, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
              
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
