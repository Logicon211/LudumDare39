using UnityEngine;
using System.Collections;

public class TrackMouse : MonoBehaviour {

	public GameObject projectile;

	public float cooldown = 1f;
	public float velocity = 80f;

	public float cooldownTimer = 0f;
	private bool onCooldown = false;

	private bool upsideDown = false;

	private Animator anim;

	private Transform barrel;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		barrel = transform.Find ("Barrel");
	}

	void Update() {

		bool faceRight = transform.GetComponentInParent<Unit>().facingRight;

		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		//We need 2 rotations stored so that we can display them correctly when the model is flipped left, but also need the actual rotate for when we spawn new rockets down below
		Quaternion actualRotate = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = actualRotate;

		if (angle > 90f || angle < -90f) {
			if (!upsideDown) {
				YFlip ();
			}
		} else {
			if (upsideDown) {
				YFlip ();
			}
		}

		if (onCooldown) {
			cooldownTimer += Time.fixedDeltaTime;
			if (cooldownTimer > cooldown) {
				onCooldown = false;
			}
		} else {
			cooldownTimer = 0f;
		}


		if(Input.GetMouseButtonDown(0) && !onCooldown) {

			anim.SetTrigger ("Shoot");

			GameObject projectileLaunched = null;
			if(faceRight) {
				projectileLaunched = Instantiate(projectile, barrel.position, actualRotate) as GameObject;
			} else {
				Vector3 posit = transform.localPosition;
				posit = new Vector3(-posit.x,posit.y,posit.z);
				projectileLaunched = Instantiate(projectile, barrel.position, actualRotate) as GameObject;
			}
			projectileLaunched.GetComponent<Rigidbody2D>().velocity = projectileLaunched.transform.right * velocity;
			onCooldown = true;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if(!(transform.parent.GetComponent<Unit>()).facingRight) {
		//	Flip();
		//}
	}

	//Used for when the tracked object goes past 90 or -90 degrees so we flip it vertically so it's no longer upside down
	void YFlip() {
		upsideDown = !upsideDown;

		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}
}
