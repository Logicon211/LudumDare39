using UnityEngine;
using System.Collections;

public class OrcBoss: MonoBehaviour, IDamagable
{
	public GameObject deathEffect;
    public LayerMask ground;
    private Vector3 targetPosition;
    public float speed=10f;
    public bool lookRight = false;
	GameObject player;
	float playerDist;
    private Animator animator;
    //public float maxDistance;
    public float attackRange = 20.0f;
	public int giantHealth;
    public float decayRadius = 20.0f;
    Unit scriptin;
    public float decayMultiplier = 10.0f;
    public float decayOriginal;
    private float speedOriginal;
    private bool decayIncreased = false;


    void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
        scriptin = player.GetComponent<Unit>();
        decayOriginal = scriptin.energyDepletionRate;
	    targetPosition = transform.position;
	    animator = GetComponent<Animator>();
        speedOriginal = speed;
	}
	

	void FixedUpdate () {

		playerDist = Vector3.Distance (this.transform.position, (player.transform.position));


        /*If the player gets close to the boss, they'll start losing energy at an accelerated rate, which ends when they get away*/
        if (playerDist < decayRadius && decayIncreased == false)
        {
            scriptin.energyDepletionRate = scriptin.energyDepletionRate*decayMultiplier;
            decayIncreased = true;
        }
        else if (playerDist >= decayRadius && decayIncreased == true)
        {
            scriptin.energyDepletionRate = decayOriginal;
            decayIncreased = false;
        }

        /*If not in an attack animation and player is in range, start one*/
			if (!this.animator.GetCurrentAnimatorStateInfo (0).IsName ("attack") && playerDist < attackRange && 
            (((player.transform.position.x < this.transform.position.x) && lookRight == true) || ((player.transform.position.x > this.transform.position.x) && lookRight == false))) {
                speed = 0.0f;
				animator.SetTrigger ("attack");
			}
            /*If not in an attack animation and player is out of range, get over there!*/
            else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("attack") )//&& playerDist > attackRange)
            {
                speed = speedOriginal;
                animator.SetTrigger("idle");         
            /*Movement split depending on whether the player is to their right or left, with the boss flipping their sprite and reversing
            their speed*/
                if (player.transform.position.x < this.transform.position.x)
                {
                    if (lookRight == false)
                    {
                        Vector3 theScale = transform.localScale;
                        theScale.x *= -1;
                        transform.localScale = theScale;
                        lookRight = true;
                    }
                    transform.position = transform.position + (new Vector3(-speed, 0f, 0f) * Time.deltaTime);
                    var p = transform.position;
                    animator.SetFloat("speed", (transform.position - p).magnitude / Time.deltaTime);
                
                }
                else
                {
                    if (lookRight == true)
                    {
                        Vector3 theScale = transform.localScale;
                        theScale.x *= -1;
                        transform.localScale = theScale;
                        lookRight = false;
                    }
                    transform.position = transform.position + (new Vector3(speed, 0f, 0f) * Time.deltaTime);
                    var p = transform.position;
                    animator.SetFloat("speed", (transform.position - p).magnitude / Time.deltaTime);
                }
            }

   
            
	}

	public void damage(int damageIn){
		//giantHealth -=damageIn;
		/*if (giantHealth < 0) {
			Instantiate (deathEffect, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}*/
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Rocket") {
			IProjectile projectile = (IProjectile)col.gameObject.GetComponent (typeof(IProjectile));
			if (projectile != null) {
				projectile.OnActorHit ();
				//damage (projectile.getDamageValue ());
			}
	}

	          
}
}
