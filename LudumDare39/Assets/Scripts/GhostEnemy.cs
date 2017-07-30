using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour, IDamagable {

	private GameObject player;
	private Rigidbody2D RB;

	public float speed = 10f;
	public float distanceToKeepFromPlayer = 4f;
	public int health = 3;
	public float projectileSpeed = 20f;

	public float maxDistance = 150f;

	public float attackCooldown = 3f;
	public float attackTimer = 0f;
	public bool onCooldown = false;

	public bool firing = false;
	public bool inFiringAnimation = false;

	private float playerDist;

	public GameObject deathEffect;
	public GameObject projectile;

	private AudioSource AS;
	private Animator anim;

	public AudioClip shootSound;
	public AudioClip hurtSound;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		RB = GetComponent<Rigidbody2D>();
		AS = GetComponent<AudioSource>();
	}

	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerDist= Vector3.Distance(RB.transform.position, player.transform.position);
		if (playerDist <= maxDistance) {
			Vector2 playerPosition = new Vector2 (player.transform.position.x, player.transform.position.y);
			Vector2 ghostPosition = new Vector2 (this.transform.position.x, this.transform.position.y);
		
			if (distanceToKeepFromPlayer + 5f < playerDist) {
				Vector2 heading = (playerPosition - ghostPosition) / playerDist;
				RB.velocity = heading * speed;
			} else if (distanceToKeepFromPlayer > playerDist) {
				Vector2 heading = (ghostPosition - playerPosition) / playerDist;
				RB.velocity = heading * speed;
			}

			if (onCooldown) {
				attackTimer += Time.fixedDeltaTime;
				if (attackTimer > attackCooldown) {
					onCooldown = false;
					attackTimer = 0f;
				}
			} else {
				attackTimer = 0f;
			}

			if (firing && !inFiringAnimation) {
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("GhostAttack")) {
					inFiringAnimation = true;
				}
			} else if (firing) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("GhostAttack")) {
					//Attack animation finished, fire bullet
					Vector2 heading = (playerPosition - ghostPosition) / playerDist;

					Vector2 direction = playerPosition - ghostPosition;
					float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
					angle -= 90f;
					Quaternion actualRotate = Quaternion.AngleAxis (angle, Vector3.forward);

					//We need 2 rotations stored so that we can display them correctly when the model is flipped left, but also need the actual rotate for when we spawn new rockets down below

					GameObject projectileLaunched = Instantiate (projectile, this.transform.position, actualRotate) as GameObject;
					projectileLaunched.GetComponent<Rigidbody2D> ().velocity = heading * projectileSpeed;

					inFiringAnimation = false;
					firing = false;

					AS.PlayOneShot (shootSound);
				}
			}

			//Check if in range to fire
			if (playerDist <= distanceToKeepFromPlayer + 10f) {

				if (!onCooldown) {
					anim.SetTrigger ("fire");
					firing = true;
					onCooldown = true;
				}
			}
		}

	}

	void OnCollisionEnter2D(Collision2D col) {
		//This isn't calling for some reason
		//Test for projectile collision

		if (col.gameObject.tag == "Rocket") {
			IProjectile projectile = (IProjectile)col.gameObject.GetComponent (typeof(IProjectile));
			if (projectile != null) {
				projectile.OnActorHit ();
				damage (projectile.getDamageValue ());

				//AS.Play ();
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

	public void damage(int damage) {
		health -= damage;
		if(health < 0){
			Instantiate (deathEffect, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
		//AS.Play ();
	}
}
