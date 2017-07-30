using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour, IDamagable
{
	public GameObject deathEffect;
    public LayerMask ground;
    private Vector3 targetPosition;
    public float speed=10f;
    public bool lookRight = true;
	GameObject player;
	float playerDist;
    private Animator animator;
	public float maxDistance;
	private bool chase;

	public int giantHealth;



	void Start ()
	{
		chase = false;
		player = GameObject.FindGameObjectWithTag ("Player");
	    targetPosition = transform.position;
	    animator = GetComponent<Animator>();
	}
	

	void Update () {

		playerDist = Vector3.Distance (this.transform.position, (player.transform.position));
		if (playerDist <= maxDistance) {
			chase = true;
		}

		if(chase){
			if (!this.animator.GetCurrentAnimatorStateInfo (0).IsName ("attack")) {
				animator.SetTrigger ("attack");
			}

			transform.position = transform.position +  (new Vector3 (-speed, 0f, 0f) *Time.deltaTime);
				var p = transform.position;
				animator.SetFloat("speed", (transform.position - p).magnitude/Time.deltaTime);

		}
	}

	public void damage(int damageIn){
		giantHealth -=damageIn;
		if (giantHealth < 0) {
			Instantiate (deathEffect, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Rocket") {
			IProjectile projectile = (IProjectile)col.gameObject.GetComponent (typeof(IProjectile));
			if (projectile != null) {
				projectile.OnActorHit ();
				damage (projectile.getDamageValue ());
			}
	}

	          
}
}
