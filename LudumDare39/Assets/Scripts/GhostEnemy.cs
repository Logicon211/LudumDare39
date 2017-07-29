using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour {

	private GameObject player;
	private Rigidbody2D RB;

	public float speed = 4f;
	public float distanceToKeepFromPlayer = 4f;

	public float maxDistance = 100f;

	private float playerDist;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		RB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerDist= Vector3.Distance(RB.transform.position, player.transform.position);
		if (distanceToKeepFromPlayer < playerDist) {

			Vector2 playerPosition = new Vector2 (player.transform.position.x, player.transform.position.y);
			Vector2 ghostPosition = new Vector2 (this.transform.position.x, this.transform.position.y);

			Vector2 direction = Vector2.MoveTowards (ghostPosition, playerPosition, maxDistance);
			direction.Normalize ();
			Debug.Log (direction);
			RB.velocity = direction * speed;
		}

	}
}
