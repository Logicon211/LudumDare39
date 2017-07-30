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
				//DestroyMapTileArea (mapPosition.x, mapPosition.y, level, (int)blastRadius);

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

	public void DestroyMapTileArea(int x, int y, Level level, int radius) {
		if (level.mapTiles[x,y] != null) {
			level.mapTiles[x,y].DestroyTile (false);
		}
		if (radius > 0) {
			if (y > 0) {
				DestroyMapTileArea (x, y - 1, level, radius - 1);
			}
			if (x < level.mapTiles.GetLength (0) - 1) {
				DestroyMapTileArea (x + 1, y, level, radius - 1);
			}
			if (y < level.mapTiles.GetLength (1) - 1) {
				DestroyMapTileArea (x, y + 1, level, radius - 1);
			}
			if (x > 0) {
				DestroyMapTileArea (x - 1, y, level, radius - 1);
			}
		}
	}

	public void DestroyMapTileRadiusAlgorithm(Level level, int centerX, int centerY, int radius)
	{
		//This is an implementation of Midpoint Circle Algorithm here: http://rosettacode.org/wiki/Bitmap/Midpoint_circle_algorithm
		//It doesn't effectively fill in the circle, Many other solutions to do that are here: http://stackoverflow.com/questions/10878209/midpoint-circle-algorithm-for-filled-circles

		if (radius == 0) {
			if (level.mapTiles [centerX, centerY] != null) {
				level.mapTiles [centerX, centerY].DestroyTile (false);
			}
			return;
		}

		int d = (5 - radius * 4) / 4;
		int x = 0;
		int y = radius;

		do
		{
			// ensure index is in range before setting (depends on your image implementation)
			// in this case we check if the pixel location is within the bounds of the image before setting the pixel

			if ((centerX + x < level.mapTiles.GetLength (0) - 1) && (centerY + y < level.mapTiles.GetLength (1) - 1)) {
				if (level.mapTiles[centerX + x,centerY + y] != null) {
					level.mapTiles[centerX + x,centerY + y].DestroyTile (false);
				}
			}
			if ((centerX + x < level.mapTiles.GetLength (0) - 1) && (centerY - y > 0)) {
				if (level.mapTiles[centerX + x,centerY - y] != null) {
					level.mapTiles[centerX + x,centerY - y].DestroyTile (false);
				}
			}
			if ((centerX - x > 0) && (centerY + y < level.mapTiles.GetLength (1) - 1)) {
				if (level.mapTiles[centerX - x,centerY + y] != null) {
					level.mapTiles[centerX - x,centerY + y].DestroyTile (false);
				}
			}
			if ((centerX - x > 0) && (centerY - y > 0)) {
				if (level.mapTiles[centerX - x,centerY - y] != null) {
					level.mapTiles[centerX - x,centerY - y].DestroyTile (false);
				}
			}
			if ((centerX + y < level.mapTiles.GetLength (0) - 1) && (centerY + x < level.mapTiles.GetLength (1) - 1)) {
				if (level.mapTiles[centerX + y,centerY + x] != null) {
					level.mapTiles[centerX + y,centerY + x].DestroyTile (false);
				}
			}
			if ((centerX + y < level.mapTiles.GetLength (0) - 1) && (centerY - x > 0)) {
				if (level.mapTiles[centerX + y,centerY - x] != null) {
					level.mapTiles[centerX + y,centerY - x].DestroyTile (false);
				}
			}
			if ((centerX - y > 0) && (centerY + x < level.mapTiles.GetLength (1) - 1)) {
				if (level.mapTiles[centerX - y,centerY + x] != null) {
					level.mapTiles[centerX - y,centerY + x].DestroyTile (false);
				}
			}
			if ((centerX - y > 0) && (centerY - x > 0)) {
				if (level.mapTiles[centerX - y,centerY - x] != null) {
					level.mapTiles[centerX - y,centerY - x].DestroyTile (false);
				}
			}

			if (d < 0)
			{
				d += 2 * x + 1;
			}
			else
			{
				d += 2 * (x - y) + 1;
				y--;
			}
			x++;
		} while (x <= y);

		DestroyMapTileRadius (level, centerX, centerY, radius - 1);
	}

	public void DestroyMapTileRadius(Level level, int centerX, int centerY, int radius) {
		//This is a more brute force algorithm. Would want to figure out distance to center for damage fall off
		for (int y = -radius; y <= radius; y++) {
			for (int x = -radius; x <= radius; x++) {
				if ((x * x + y * y <= radius * radius) && centerX + x >= 0 && centerX + x < level.mapTiles.GetLength (0) - 1 && centerY + y >= 0 && centerY + y < level.mapTiles.GetLength (1) - 1) {
					if (level.mapTiles[centerX + x,centerY + y] != null) {
						float dmgMod = 1 - ((float)(x * x + y * y) / (float)(radius*radius));
						int damageToTile = Mathf.RoundToInt(damageValue * dmgMod);
						level.mapTiles [centerX + x, centerY + y].TakeDamage (damageToTile);
					}
				}
			}
		}
	}
}
