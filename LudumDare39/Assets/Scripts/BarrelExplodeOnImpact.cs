using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplodeOnImpact : MonoBehaviour {
    private GameObject player;
    public GameObject deathEffect;
    private Rigidbody2D RB;
    public float blastRadius = 8.0f;
    public bool hurtsPlayer = false;
    public int damageValue = 10;

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

				//destroy tiles now
				Level level = Level.getLevel();
				Vector2i mapPosition = new Vector2i ((int)transform.position.x, (int)transform.position.y);

				DestroyMapTileRadius (level, mapPosition.x, mapPosition.y, (int)blastRadius);

				//refresh colliders
				level.refreshCollidersOnOuterTiles();

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

	public void DestroyMapTileRadius(Level level, int centerX, int centerY, int radius) {
		//This is a more brute force algorithm. Would want to figure out distance to center for damage fall off
		for (int y = -radius; y <= radius; y++) {
			for (int x = -radius; x <= radius; x++) {
				if ((x * x + y * y <= radius * radius) && centerX + x >= 0 && centerX + x < level.mapTiles.GetLength (0) - 1 && centerY + y >= 0 && centerY + y < level.mapTiles.GetLength (1) - 1) {
					if (level.mapTiles[centerX + x,centerY + y] != null) {
						float dmgMod = 1 - ((float)(x * x + y * y) / (float)(radius*radius));
						int damageToTile = Mathf.RoundToInt(damageValue * dmgMod);
						level.mapTiles [centerX + x, centerY + y].TakeDamage (damageToTile, false);
					}
				}
			}
		}
	}
}
