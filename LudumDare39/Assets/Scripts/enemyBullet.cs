using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour, IProjectile {

	public int damageToTile = 10;
	public int damageToEnemies = 1;
	public int damageToPlayer = 5;
	public int damage = 20;

	public GameObject explosion;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public int getDamageValue() {
		return damageToPlayer;
	}

	public void OnActorHit() {
		//Destory the projectile
		Instantiate (explosion, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.GetComponent<Unit>() != null) {
			col.gameObject.GetComponent<Unit>().playerHealthChange(-damageToPlayer);
		}
	}

	public void OnMapTileHit (MapTile mapTile) {
		Level level = mapTile.level;
		//Start destroying a radius
		DestroyMapTileRadius(level, mapTile.x, mapTile.y, 8);

		//if (level.mapTiles [mapTile.x, mapTile.y] != null) {
		//	level.mapTiles [mapTile.x, mapTile.y].TakeDamage (damageToTile, false);
		//}

		Instantiate (explosion, this.transform.position, Quaternion.identity);
		//refresh colliders this is probably to slow to put in every hit
		level.refreshCollidersOnOuterTiles();

		//Destory the projectile
		Destroy (this.gameObject);
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
						int damageToTile = Mathf.RoundToInt(damage * dmgMod);
						level.mapTiles [centerX + x, centerY + y].TakeDamage (damageToTile, false);
					}
				}
			}
		}
	}
}
