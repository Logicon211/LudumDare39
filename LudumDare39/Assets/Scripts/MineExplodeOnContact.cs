using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplodeOnContact : MonoBehaviour {
    private GameObject player;
    public GameObject deathEffect;
    public float blastRadius = 6.0f;
    public int damageValue = 10;
	bool blewUp;
	// Use this for initialization
    void Start () {
		blewUp = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
		if (!(col.gameObject.tag == "ground")) {
			blewUp = true;
        
			if (Vector3.Distance (transform.position, (player.transform.position)) < blastRadius) {
				IDamagable scriptin = player.GetComponent<IDamagable> ();
				scriptin.damage (damageValue);

			}
			foreach (GameObject thing in GameObject.FindGameObjectsWithTag("Enemy")) {

				if (Vector3.Distance (transform.position, (thing.transform.position)) < blastRadius) {
					IDamagable scriptin = thing.GetComponent<IDamagable> ();
					scriptin.damage (damageValue);
				}

			}

			Instantiate (deathEffect, this.transform.position, Quaternion.identity);
        
			//destroy tiles now
			Level level = Level.getLevel ();
			Vector2i mapPosition = new Vector2i ((int)transform.position.x, (int)transform.position.y);

			DestroyMapTileRadius (level, mapPosition.x, mapPosition.y, (int)blastRadius);
			//refresh colliders
			level.refreshCollidersOnOuterTiles ();

			if (col.gameObject.tag == "Rocket") {
				IProjectile projectile = (IProjectile)col.gameObject.GetComponent (typeof(IProjectile));
				if (projectile != null) {
					projectile.OnActorHit ();
				}

			}

			Destroy (this.gameObject);
		}
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
