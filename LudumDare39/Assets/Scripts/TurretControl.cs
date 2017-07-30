using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour, IDamagable
{
    private GameObject player;
    private Rigidbody2D RB;

    bool onCooldown = false;
    float attackTimer = 0.0f;
    public float attackCooldown = 4.0f;
    public float firingRange = 10.0f;

    public bool firing = false;
    public bool inFiringAnimation = false;

    public GameObject deathEffect;
    public GameObject projectile;

    public int health = 5;
    public float projectileSpeed = 10f;

    private Animator anim;

    private float playerDist;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDist = Vector3.Distance(RB.transform.position, player.transform.position);
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 ghostPosition = new Vector2(this.transform.position.x, this.transform.position.y);

        if (onCooldown)
        {
            attackTimer += Time.fixedDeltaTime;
            if (attackTimer > attackCooldown)
            {
                onCooldown = false;
                attackTimer = 0f;
            }
        }
        else
        {
            attackTimer = 0f;
        }

        if (firing && !inFiringAnimation)
        {
            //Don't know about this animation stuff 
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("TurretAttack"))
            if (true)
            {
                inFiringAnimation = true;
            }
        }
        else if (firing)
        {
            if (true)
                //Again don't know
            //if (!anim.GetCurrentAnimatorStateInfo(0).IsName("GhostAttack"))
            {
                //Attack animation finished, fire bullet
                Vector2 heading = (playerPosition - ghostPosition) / playerDist;

                Vector2 direction = playerPosition - ghostPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 0f;
                Quaternion actualRotate = Quaternion.AngleAxis(angle, Vector3.forward);

                //We need 2 rotations stored so that we can display them correctly when the model is flipped left, but also need the actual rotate for when we spawn new rockets down below

                GameObject projectileLaunched = Instantiate(projectile, this.transform.position, actualRotate) as GameObject;
                projectileLaunched.GetComponent<Rigidbody2D>().velocity = heading * projectileSpeed;

                inFiringAnimation = false;
                firing = false;
            }
        }

        //Check if in range to fire
        if (playerDist <= firingRange)
        {

            if (!onCooldown)
            {
                //No turret animation
                //anim.SetTrigger("fire");
                firing = true;
                onCooldown = true;
            }
        }
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
                damage(projectile.getDamageValue());
            }

        }
    }
    public void damage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
