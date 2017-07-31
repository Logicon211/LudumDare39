using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour, IProjectile {

	int damageToTile = 1;
	int damageToEnemies = 1;
	int damageToPlayer = 1;
	Renderer VisibleChecker;
	// Use this for initialization

	public GameObject hitEffect;

	void Start () {
		VisibleChecker= GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!VisibleChecker.isVisible) {
			Destroy (this.gameObject);
		}
	}

	public int getDamageValue() {
		return damageToEnemies;
	}

	public void OnActorHit() {
		//Destory the projectile
		Instantiate (hitEffect, this.transform.position, Quaternion.Inverse(Quaternion.identity));//Vector2.down);//Quaternion.identity);//Quaternion.Euler(Quaternion.identity));
		Destroy (this.gameObject);
	}

	void OnCollisionEnter2D (Collision2D col) 
	{
		if(col.gameObject.GetComponent<HealthController>() != null) {
			Debug.Log(col.gameObject.GetComponent<HealthController>().health);
			col.gameObject.GetComponent<HealthController>().takeDamage(1);
		}
		if(col.gameObject.tag.Equals("EnemyBullet")) {
			Destroy (this.gameObject);
		}
	}

	public void OnMapTileHit (MapTile mapTile) {
		Level level = mapTile.level;

		Instantiate (hitEffect, this.transform.position, Quaternion.Inverse(Quaternion.identity));

		if (level.mapTiles [mapTile.x, mapTile.y] != null) {
			level.mapTiles [mapTile.x, mapTile.y].TakeDamage (damageToTile, true);
		}

		//refresh colliders this is probably to slow to put in every hit
		//level.refreshCollidersOnOuterTiles();

		//Destory the projectile
		Destroy (this.gameObject);
	}
}
