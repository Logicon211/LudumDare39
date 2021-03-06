﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//This Script is intended for demoing and testing animations only.


public class bearController : MonoBehaviour, IDamagable {
	public bool jump = false;

	public float speed = 10f;
	public float jumpSpeed = 40f;
	public int bearVitality;
	//private float maxVertHSpeed = 20f;
	private bool facingRight = true;
	private float moveXInput;
	private bool dead;

	public GameObject HealthPickup;
	public GameObject EnergyPickup;
    //Used for flipping Character Direction
	public static Vector3 theScale;

	//Jumping Stuff
	public LayerMask whatIsGround;
	private bool grounded = false;
	private AudioSource runningSound;
	private AudioSource PunchNoise;
	private AudioSource AS;
	private Animator anim;
	private Rigidbody2D RB;

	private Transform groundCheck1;
	private Transform groundCheck2;
	private Transform unstuckGroundCheck;
	public float groundTimer = 0f;

	public static float MAX_GROUNDED_TIMER = 2f;
	public float pathTimer;

	public int maxJumpHeight = 6;
	public int width = 3;
	public int height = 6;

	private int originalWidth;

	public float maxDistance = 100f;

	public int bearDamage = 10;

	public GameObject deathEffect;
	private GameObject player;

	public enum BotState
	{
		None = 0,
		MoveTo,
		Punch
	}

	private BotState mCurrentBotState;
	private bool[] mInputs;
	private bool[] mPrevInputs;
	private int mCurrentNodeId;

	public enum KeyInput
	{
		GoLeft = 0,
		GoRight,
		GoDown,
		Jump,
		Count
	}

	private Level mLevel;
	private List<Vector2i> mPath;
	private int mFramesOfJumping;
	private Vector2 mOldPosition;
	private int mStuckFrames;
	public const int cMaxStuckFrames = 20;
	private LineRenderer lineRenderer;
	private float playerDist;
	private float punchCooldown;

	// Use this for initialization
	void Awake ()
	{
//		startTime = Time.time;
		anim = GetComponent<Animator> ();
	}

	void Start () {
		dead = false;
		GameObject child = transform.Find("RunningSound").gameObject;
		runningSound = child.GetComponent<AudioSource>();
		GameObject child2 = transform.Find("PunchNoise").gameObject;
		PunchNoise = child2.GetComponent<AudioSource>();

		player = GameObject.FindGameObjectWithTag ("Player");
		lineRenderer = GetComponent<LineRenderer> ();
		punchCooldown = 0;
		mLevel = Level.getLevel ();
		RB = GetComponent<Rigidbody2D>();
		groundCheck1 = transform.Find ("groundCheck1");
		groundCheck2 = transform.Find ("groundCheck2");
		unstuckGroundCheck = transform.Find ("unstuckGroundCheck");
		mCurrentBotState = BotState.None;
		mFramesOfJumping = 0;
		mStuckFrames = 0;
		mOldPosition = new Vector2 (transform.position.x, transform.position.y);
		mInputs = new bool[(int)KeyInput.Count];
		mPrevInputs = new bool[(int)KeyInput.Count];
		mPath = new List<Vector2i> ();
		pathTimer = 0;
		AS = GetComponent<AudioSource>();

		originalWidth = width;
	}


	void FixedUpdate ()
	{
		pathTimer -= 1;
		punchCooldown -= 1;
		//Jump checks
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		bool grounded1 = Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground")); 
		bool grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
		//Check in between the 2 ground checks
		bool grounded3 = Physics2D.Linecast(groundCheck1.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));

		grounded = grounded1 || grounded2 || grounded3;
		if (!grounded) {
			groundTimer += Time.fixedDeltaTime;
			if (groundTimer < MAX_GROUNDED_TIMER) {
				grounded = true;
			}
		} else {
			groundTimer = 0f;
		}

		//unstuck if jammed in the ground
		bool stuckInGround = Physics2D.Linecast(transform.position, unstuckGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (stuckInGround) {
			this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 3f);
		}

		//grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("ground", grounded);


		anim = GetComponent<Animator> ();
		float move = 0;

		bool continueJumping = false;

		//Bot control reading
		if (mInputs [(int)KeyInput.GoRight]) {
			move = 1;
		}

		if (mInputs [(int)KeyInput.GoLeft]) {
			move = -1;
		}

		if (mInputs [(int)KeyInput.Jump] && grounded) {
			jump = true;
			Debug.Log (jump);
		} else if (mInputs [(int)KeyInput.Jump] && !grounded) {
			continueJumping = true;
		}
		if (move != 0/*Input.GetKey(KeyCode.RightArrow*/) {
			if (move > 0) {
				if (!facingRight) {
					Flip ();
				}
			} else {
				if (facingRight) {
					Flip ();
				}
			}
			RB.velocity = new Vector2 (speed * move, RB.velocity.y);
			anim.SetBool ("Sprint", true);
		} else {
			anim.SetBool ("Sprint", false);
			//Stop left or right movement when those keys aren't pressed
			RB.velocity = new Vector2 (0f, RB.velocity.y);
		}


		if (jump) {
			RB.velocity = new Vector2 (RB.velocity.x, jumpSpeed);
			//anim.SetBool ("Jumping", true);
			jump = false; //reset the jump flag so it doesn't happen again immediately
			groundTimer = MAX_GROUNDED_TIMER;//reset groundTimer to MAX_GROUNDED_TIMER so that it registers that we've used our 1 jump after stepping off a ledge
		}

		//This should make it so holding the jump button causes a higher jump (May need to tweek this a bit)
		if (continueJumping && RB.velocity.y > 0) {
			RB.gravityScale = 0.3f;
		} else {
			RB.gravityScale = 1f;
		}

		//Need to update old position after update
		mOldPosition = new Vector2 (transform.position.x, transform.position.y);
		UpdatePrevInputs ();








	}

	void Update()
	{
		if (grounded) {
			playerDist = Vector3.Distance (RB.transform.position, (player.transform.position));
			if (playerDist <= maxDistance) {
				if (playerDist < 9 && punchCooldown < 0) {
					if (mCurrentBotState != BotState.Punch) {
						ChangeState (BotState.Punch);
					}
				} else if (pathTimer < 0 && grounded) {
					MoveTo (new Vector2i (((int)(player.transform.position.x)), ((int)(player.transform.position.y)) - 2));
					pathTimer = 40;

				}
			}
		}
		BotUpdate ();






        
       /** if (Input.GetButtonDown("Fire1") && (grounded)) { anim.SetTrigger("Punch"); }

        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Sprint", true);
            HSpeed = 14f;
}
        else
        {
            anim.SetBool("Sprint", false);
            HSpeed = 10f;
        }

        //Flipping direction character is facing based on players Input
        if (moveXInput > 0 && !facingRight)
            Flip();
        else if (moveXInput < 0 && facingRight)
            Flip();*/
    }
    ////Flipping direction of character
    void Flip()
	{
		facingRight = !facingRight;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}



	//******Bot Functions below******
	void BotUpdate()
	{
		//get the position of the bottom of the bot's aabb, this will be much more useful than the center of the sprite (mPosition)
		int tileX, tileY;
		var position = transform.position;
		position.y -= (height/2f);

		//This nulls out on map startup. Should probably do this in the MoveTo method;
		//mLevel.GetMapTileAtPoint(position, out tileX, out tileY);

		int characterHeight = height;//Mathf.CeilToInt(mAABB.size.y*2.0f/Level.cTileSize);

		int dir;

		switch (mCurrentBotState)
		{
		case BotState.None:
			TestJumpValues ();

			if (mFramesOfJumping > 0) {
				mFramesOfJumping -= 1;
				mInputs [(int)KeyInput.Jump] = true;
			} else {
				mInputs [(int)KeyInput.Jump] = false;
			}

			break;

		case BotState.Punch:
			if (punchCooldown < 0) {
				anim.SetTrigger ("Punch");
				punchCooldown = 50;

				Unit scriptin = player.GetComponent<Unit>();
				scriptin.playerHealthChange (-bearDamage);
				PunchNoise.Play();
			}
			break;

		case BotState.MoveTo:
			if (!runningSound.isPlaying) {
				runningSound.Play ();
			}
			Vector2 prevDest, currentDest, nextDest;
			bool destOnGround, reachedY, reachedX;

			mLevel.GetMapTileAtPoint(position, out tileX, out tileY);
			GetContext (out prevDest, out currentDest, out nextDest, out destOnGround, out reachedX, out reachedY);

			//TODO: Turn this into just a Vector2 Method (Currently its grabbing the bottom center point of the unit, dunno if that's good)
			Vector2 pathPosition = new Vector2(transform.position.x,transform.position.y - (height/2));//mAABB.center - (mAABB.size / 2) + Vector3.one * Level.cTileSize * 0.5f;

			mInputs [(int)KeyInput.GoRight] = false;
			mInputs [(int)KeyInput.GoLeft] = false;
			mInputs [(int)KeyInput.Jump] = false;
			mInputs [(int)KeyInput.GoDown] = false;

			if (pathPosition.y - currentDest.y > Constants.cBotMaxPositionError/* && mOnOneWayPlatform TODO: Handle one way platforms*/)
				mInputs [(int)KeyInput.GoDown] = true;

			if (reachedX && reachedY) {
				int prevNodeId = mCurrentNodeId;
				mCurrentNodeId++;

				if (mCurrentNodeId >= mPath.Count) {
					mCurrentNodeId = -1;
					ChangeAction (BotState.None);
					break;
				}

				if (grounded)
					mFramesOfJumping = GetJumpFramesForNode (prevNodeId);

				goto case BotState.MoveTo;
			} else if (!reachedX) {
				if (currentDest.x - pathPosition.x > Constants.cBotMaxPositionError)
					mInputs [(int)KeyInput.GoRight] = true;
				else if (pathPosition.x - currentDest.x > Constants.cBotMaxPositionError)
					mInputs [(int)KeyInput.GoLeft] = true;
			} else if (!reachedY && mPath.Count > mCurrentNodeId + 1 && !destOnGround) {
				int checkedX = 0;

				if (mPath [mCurrentNodeId + 1].x != mPath [mCurrentNodeId].x) {
					mLevel.GetMapTileAtPoint (pathPosition, out tileX, out tileY);

					if (mPath [mCurrentNodeId + 1].x > mPath [mCurrentNodeId].x)
						checkedX = tileX + width;
					else
						checkedX = tileX - 1;
				}

				if (checkedX != 0 && !mLevel.AnySolidBlockInStripe (checkedX, tileY, mPath [mCurrentNodeId + 1].y)) {
					if (nextDest.x - pathPosition.x > Constants.cBotMaxPositionError)
						mInputs [(int)KeyInput.GoRight] = true;
					else if (pathPosition.x - nextDest.x > Constants.cBotMaxPositionError)
						mInputs [(int)KeyInput.GoLeft] = true;

					if (ReachedNodeOnXAxis (pathPosition, currentDest, nextDest) && ReachedNodeOnYAxis (pathPosition, currentDest, nextDest)) {
						mCurrentNodeId += 1;
						goto case BotState.MoveTo;
					}
				}
			}

			if (mFramesOfJumping > 0 &&
				(!grounded || (reachedX && !destOnGround) || (grounded && destOnGround))) {
				//Useful to try and debug how high the unit can g o
				//Debug.Log (mFramesOfJumping + " : " + jumpSpeed);
				mInputs [(int)KeyInput.Jump] = true;
				if (!grounded)
					--mFramesOfJumping;
			}

			Vector2 mPosition = new Vector2 (transform.position.x, transform.position.y);
			if (mPosition == mOldPosition)
			{
				++mStuckFrames;
				if (mStuckFrames > cMaxStuckFrames) {
					MoveTo (mPath [mPath.Count - 1]);
					mStuckFrames = 0;
				}
			}
			else
				mStuckFrames = 0;

			break;
		}

		//		if (gameObject.activeInHierarchy)
		//			CharacterUpdate();
		//		}
	}

	//I'm assuming this is a debug function to test how long certain number of button frames affect jumping
	public void TestJumpValues()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			mFramesOfJumping = GetJumpFrameCount(1);
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			mFramesOfJumping = GetJumpFrameCount(2);
		else if (Input.GetKeyDown(KeyCode.Alpha3))
			mFramesOfJumping = GetJumpFrameCount(3);
		else if (Input.GetKeyDown(KeyCode.Alpha4))
			mFramesOfJumping = GetJumpFrameCount(4);
		else if (Input.GetKeyDown(KeyCode.Alpha5))
			mFramesOfJumping = GetJumpFrameCount(5);
		else if (Input.GetKeyDown(KeyCode.Alpha6))
			mFramesOfJumping = GetJumpFrameCount(6);
		else if (Input.GetKeyDown(KeyCode.Alpha7))
			mFramesOfJumping = GetJumpFrameCount(7);
	}

	public void ChangeState(BotState newState)
	{
		mCurrentBotState = newState;
	}

	int GetJumpFrameCount(int deltaY)
	{
		if (deltaY <= 0)
			return 0;
		else
		{
			switch (deltaY)
			{
			case 1:
				return 1;
			case 2:
				return 2;
			case 3:
				return 6;
			case 4:
				return 12;
			case 5:
				return 20;
			case 6:
				return 28;
			case 7:
				return 34;
			default:
				return 40;
			}
		}
	}

	//Returns the tile coordinate of the empty space above a ground tile found below the tile coordinate passed in
	public Vector2i getTopOfGroundTileBelowTile(Vector2i mapPos)
	{
		//TODO: Need to check if there is NO ground tile below passed in point
		RaycastHit2D groundHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(0, -1), Mathf.Infinity, mLevel.playerLayerMask);
		if (groundHit.collider == null) {
			//If there's no ground right below this unit just return the passed in cordinate instead
			return mapPos;
		}

		while (!(mLevel.IsGround(mapPos.x, mapPos.y)))
			--mapPos.y;

		return new Vector2i(mapPos.x, mapPos.y + 1);
	}

	public void MoveTo(Vector2i destination)
	{
		//Reset Bot State and clear old path;
		clearPathAndBotState();

		//TODO: Try to use an actual AABB object eventually (Bounding Box)
		//Vector2 mAABBCenter = new Vector2(transform.position.x, transform.position.y);
		//Vector2 mAABBHalfSize = new Vector2(width/2, height/2);

		Vector2i startTile;

		if (grounded) {
			startTile = mLevel.GetMapTileAtPoint (new Vector2 (transform.position.x, transform.position.y - height/2f + 1));
			Debug.DrawLine (new Vector3 (startTile.x, startTile.y, 1), new Vector3 (startTile.x, startTile.y + 1, 1), Color.blue);
		} else {
			Vector2i centerUnitTile = mLevel.GetMapTileAtPoint (new Vector2 (transform.position.x, transform.position.y - (height/2) + 1));
			startTile = getTopOfGroundTileBelowTile(centerUnitTile);
		}
		//Vector2i startTile = mLevel.GetMapTileAtPoint(mAABBCenter - mAABBHalfSize/* + Vector2.one * Level.cTileSize * 0.5f*/);

		if (grounded && !IsOnGroundAndFitsPos(startTile))
		{
			if (IsOnGroundAndFitsPos(new Vector2i(startTile.x + 1, startTile.y)))
				startTile.x += 1;
			else
				startTile.x -= 1;

		}

		var path =  mLevel.mPathFinder.FindPath(
			startTile, 
			destination,
			width,
			height,
			(short)maxJumpHeight,
			1);

		//***** Wierd pathfinding fix so that dudes don't get stuck too long
		while (path == null && width > 2) {
			width--;
			path =  mLevel.mPathFinder.FindPath(
				startTile, 
				destination,
				width,
				height,
				(short)maxJumpHeight,
				1);
		}

		if (path != null && path.Count > 1)
		{
			for (var i = path.Count - 1; i >= 0; --i)
				mPath.Add(path[i]);

			mCurrentNodeId = 1;

			ChangeState(BotState.MoveTo);

			mFramesOfJumping = GetJumpFramesForNode(0);
		}

		//reset width?
		if (width != originalWidth) {
			width = originalWidth;
		}

		//Debug path
		//DrawPathLines(mPath);
	}

	bool IsOnGroundAndFitsPos(Vector2i pos)
	{
		for (int y = pos.y; y < pos.y + height; ++y)
		{
			for (int x = pos.x; x < pos.x + width; ++x)
			{
				if (mLevel.IsObstacle(x, y))
					return false;
			}
		}

		for (int x = pos.x; x < pos.x + width; ++x)
		{
			if (mLevel.IsGround(x, pos.y - 1))
				return true;
		}

		return false;
	}

	public void ChangeAction(BotState newAction)
	{
		mCurrentBotState = newAction;
	}

	public void GetContext(out Vector2 prevDest, out Vector2 currentDest, out Vector2 nextDest, out bool destOnGround, out bool reachedX, out bool reachedY)
	{
		//Translate from map coordinates to world coordinates
		prevDest = new Vector2(mPath[mCurrentNodeId - 1].x * Level.cTileSize + mLevel.transform.position.x,
			mPath[mCurrentNodeId - 1].y * Level.cTileSize + mLevel.transform.position.y);

		currentDest = new Vector2(mPath[mCurrentNodeId].x * Level.cTileSize + mLevel.transform.position.x,
			mPath[mCurrentNodeId].y * Level.cTileSize + mLevel.transform.position.y);

		nextDest = currentDest;

		if (mPath.Count > mCurrentNodeId + 1)
		{
			nextDest = new Vector2(mPath[mCurrentNodeId + 1].x * Level.cTileSize + mLevel.transform.position.x,
				mPath[mCurrentNodeId + 1].y * Level.cTileSize + mLevel.transform.position.y);
		}

		destOnGround = false;

		//TODO: This loop assumes we're checking width starting with the bottom left, but we're actually starting from the center
		//		for (int x = mPath[mCurrentNodeId].x; x < mPath[mCurrentNodeId].x + width; ++x)
		//		{
		//			//if y-1 is ground but y also is ground then it's not a valid ground spot I think
		//			if (mLevel.IsGround(x, mPath[mCurrentNodeId].y - 1))
		//			{
		//				destOnGround = true;
		//				break;
		//			}
		//		}

		//Middle origin ground check try:
		int currentNodeX = mPath[mCurrentNodeId].x;
		for (int i = 0; i < (int)Mathf.FloorToInt(width / 2); i++) {
			//if y-1 is ground but y also is ground then it's not a valid ground spot I think
			if (mLevel.IsGround(mPath[mCurrentNodeId].x+i, mPath[mCurrentNodeId].y - 1))
			{
				destOnGround = true;
				break;
			}
			//if y-1 is ground but y also is ground then it's not a valid ground spot I think
			if (mLevel.IsGround(mPath[mCurrentNodeId].x-i, mPath[mCurrentNodeId].y - 1))
			{
				destOnGround = true;
				break;
			}
		}

		//TODO: Turn this into just a Vector2 Method (Currently its grabbing the bottom center point of the unit, dunno if that's good)
		Vector2 pathPosition = new Vector2(transform.position.x,transform.position.y - (height/2));//mAABB.center - (mAABB.size / 2) + Vector3.one * Level.cTileSize * 0.5f;

		Debug.DrawLine (new Vector3 (pathPosition.x, pathPosition.y, 1), new Vector3 (pathPosition.x, pathPosition.y + 1, 1), Color.blue);

		reachedX = ReachedNodeOnXAxis(pathPosition, prevDest, currentDest);
		reachedY = ReachedNodeOnYAxis(pathPosition, prevDest, currentDest);


		//TODO: THIS KINDA DOESNT LOOK GOOD. Either fix jump height and physics or figure this one out
		//snap the character if it reached the goal but overshot it by more than cBotMaxPositionError
		if (reachedX && Mathf.Abs(pathPosition.x - currentDest.x) > Constants.cBotMaxPositionError && Mathf.Abs(pathPosition.x - currentDest.x) < Constants.cBotMaxPositionError*3.0f && !mPrevInputs[(int)KeyInput.GoRight] && !mPrevInputs[(int)KeyInput.GoLeft])
		{
			pathPosition.x = currentDest.x;
			transform.position = new Vector3 (pathPosition.x/* - Level.cTileSize * 0.5f + mAABB.size.x/2 + mAABBOffset.x dunno how to use this*/, transform.position.y, transform.position.z);
		}

		if (destOnGround && !grounded)
			reachedY = false;
	}

	public bool ReachedNodeOnXAxis(Vector2 pathPosition, Vector2 prevDest, Vector2 currentDest)
	{
		return (prevDest.x <= currentDest.x && pathPosition.x >= currentDest.x)
			|| (prevDest.x >= currentDest.x && pathPosition.x <= currentDest.x)
			|| Mathf.Abs(pathPosition.x - currentDest.x) <= Constants.cBotMaxPositionError;
	}

	public bool ReachedNodeOnYAxis(Vector2 pathPosition, Vector2 prevDest, Vector2 currentDest)
	{
		//Made a tweak to have them keep moving past jumps. Might fuck shit up
		return (pathPosition.y > currentDest.y)
			|| (prevDest.y <= currentDest.y && pathPosition.y >= currentDest.y)
			|| (prevDest.y >= currentDest.y && pathPosition.y <= currentDest.y)
			|| (Mathf.Abs(pathPosition.y - currentDest.y) <= Constants.cBotMaxPositionError);
	}

	public int GetJumpFramesForNode(int prevNodeId)
	{
		int currentNodeId = prevNodeId + 1;

		if (mPath[currentNodeId].y - mPath[prevNodeId].y > 0 && grounded)
		{
			int jumpHeight = 1;
			for (int i = currentNodeId; i < mPath.Count; ++i)
			{
				if (mPath[i].y - mPath[prevNodeId].y >= jumpHeight)
					jumpHeight = mPath[i].y - mPath[prevNodeId].y;
				//My code snippet here is to detect whether or not the previou node isn't directly below the next node (Because if it is then it's just the start of the jump node)
				if ((mPath[i].y - mPath[prevNodeId].y < jumpHeight || mLevel.IsGround(mPath[i].x, mPath[i].y - 1))/* && ((mPath[i].x != mPath[prevNodeId].x && mPath[i].y != mPath[prevNodeId].y+1))*/)
					return GetJumpFrameCount(jumpHeight);
			}
		}

		return 0;
	}

	public void UpdatePrevInputs()
	{
		var count = (byte)KeyInput.Count;

		for (byte i = 0; i < count; ++i)
			mPrevInputs[i] = mInputs[i];
	}

	public void clearPathAndBotState() {
		ChangeState(BotState.None);
		mPath.Clear();
		mInputs [(int)KeyInput.GoRight] = false;
		mInputs [(int)KeyInput.GoLeft] = false;
		mInputs [(int)KeyInput.Jump] = false;
		mInputs [(int)KeyInput.GoDown] = false;
	}

	//Debug tool
	protected void DrawPathLines(List<Vector2i> path) {
		if (path != null && path.Count > 0) {
			lineRenderer.enabled = true;
			lineRenderer.SetVertexCount (path.Count);
			lineRenderer.SetWidth (0.5f, 0.5f);

			for (var i = 0; i < path.Count; ++i) {
				lineRenderer.SetColors (Color.green, Color.green);
				//Commenting out tile size because I've manually made the tiles big
				lineRenderer.SetPosition (i, mLevel.transform.position + new Vector3 (path [i].x/* * cTileSize*/, path [i].y/* * cTileSize*/, -5.0f));
			}
		} else {
			lineRenderer.enabled = false;
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
				AS.Play ();
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
		bearVitality -= damage;
		if(bearVitality < 0){
			
			Instantiate (deathEffect, this.transform.position, Quaternion.identity);
			float deathSpawn = UnityEngine.Random.Range (0, 15);
			if (!dead) {
				if (deathSpawn < 1) {
					dead = true;
					Debug.Log ("SPAWN HEALTH");
					Instantiate (HealthPickup, this.transform.position, Quaternion.identity);
				} else if (deathSpawn < 2) {
					dead = true;
					Instantiate (EnergyPickup, this.transform.position, Quaternion.identity);
					Debug.Log ("SPAWN ENERGY");
				}
				Destroy (this.gameObject);
			}
		}
	}

}
