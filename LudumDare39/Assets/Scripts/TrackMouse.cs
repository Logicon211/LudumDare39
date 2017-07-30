using UnityEngine;
using System.Collections;

public class TrackMouse : MonoBehaviour {

	public GameObject projectile;

	public float cooldown = 1f;
	public float velocity = 80f;

	public float cooldownTimer = 0f;
	public bool onCooldown = false;

	private bool upsideDown = false;

	private Animator anim;
	public int weapon;
	private Transform barrel;

	//Audio clips
	private AudioSource gunAudioSource;
	public AudioClip[] gunSounds = new AudioClip[10];

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		gunAudioSource = GetComponent<AudioSource> ();
		barrel = transform.Find ("Barrel");
		weapon = 0;
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
				cooldownTimer = 0f;
			}
		} else {
			cooldownTimer = 0f;
		}


		if(Input.GetMouseButton(0) && !onCooldown) {


			if (weapon == 0) {
				anim.SetTrigger ("Shoot");

				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;

				if (faceRight) {
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				} else {
					Vector3 posit = transform.localPosition;
					posit = new Vector3 (-posit.x, posit.y, posit.z);
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				}
				projectileLaunched.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity+ Random.Range(-2,2));
				onCooldown = true;
				cooldown = 1f;

			} else if (weapon == 1) {

				GameObject projectileLaunched = null;
			
				if (faceRight) {
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				} else {
					Vector3 posit = transform.localPosition;
					posit = new Vector3 (-posit.x, posit.y, posit.z);
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				}

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * velocity;
				onCooldown = true;
				cooldown = 0.25f;
			}

			else if (weapon == 2) {
				anim.SetTrigger ("Shoot");

				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;
				GameObject projectileLaunched4 = null;
				GameObject projectileLaunched5 = null;
				GameObject projectileLaunched6 = null;
				GameObject projectileLaunched7 = null;


				if (faceRight) {
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched4 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched5 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched6 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched7 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				} else {
					Vector3 posit = transform.localPosition;
					posit = new Vector3 (-posit.x, posit.y, posit.z);
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched4 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched5 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched6 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched7 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				}
				projectileLaunched.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched4.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched5.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched6.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched7.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range(-2,2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched4.GetComponent<Rigidbody2D> ().velocity = projectileLaunched4.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched5.GetComponent<Rigidbody2D> ().velocity = projectileLaunched5.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched6.GetComponent<Rigidbody2D> ().velocity = projectileLaunched6.transform.right * (velocity+ Random.Range(-2,2));
				projectileLaunched7.GetComponent<Rigidbody2D> ().velocity = projectileLaunched7.transform.right * (velocity+ Random.Range(-2,2));
				onCooldown = true;
				cooldown = 1f;

			}

			else if (weapon == 3) {

				GameObject projectileLaunched = null;

				if (faceRight) {
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				} else {
					Vector3 posit = transform.localPosition;
					posit = new Vector3 (-posit.x, posit.y, posit.z);
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				}

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * velocity;
				onCooldown = true;
				cooldown = 0.1f;
			}

			else if (weapon == 4) {

				GameObject projectileLaunched = null;

				if (faceRight) {
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				} else {
					Vector3 posit = transform.localPosition;
					posit = new Vector3 (-posit.x, posit.y, posit.z);
					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				}

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-1, 1));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * velocity;
				onCooldown = true;
				cooldown = 0.5f;
			}

			gunAudioSource.PlayOneShot (gunSounds [weapon]);

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

	public void upgradeWeapon(){
		weapon++;

	}
}
