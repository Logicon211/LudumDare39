using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour, IProjectile {

	int damageToTile = 1;
	int damageToEnemies = 1;
	int damageToPlayer = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getDamageValue() {
		return damageToEnemies;
	}

	public void OnActorHit() {
		//Destory the projectile
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.GetComponent<HealthController>() != null) {
			Debug.Log(col.gameObject.GetComponent<HealthController>().health);
			col.gameObject.GetComponent<HealthController>().takeDamage(1);
		}
	}

	public void OnMapTileHit (MapTile mapTile) {
		Level level = mapTile.level;
		//Start destroying a radius

		level.mapTiles [mapTile.x, mapTile.y].TakeDamage (damageToTile);

		//refresh colliders this is probably to slow to put in every hit
		//level.refreshCollidersOnOuterTiles();

		//Destory the projectile
		Destroy (this.gameObject);
	}
}
