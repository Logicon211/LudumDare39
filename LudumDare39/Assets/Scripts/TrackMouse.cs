using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrackMouse : MonoBehaviour {

	public GameObject projectile;
	public GameObject Rocket;
	public float cooldown = 1f;
	public float velocity = 80f;

	public float cooldownTimer = 0f;
	public bool onCooldown = false;

	private bool upsideDown = false;

	private Animator anim;
	public int weapon;
	private Transform barrel;

	private GameObject playerIn;

	//Audio clips
	private AudioSource gunAudioSource;
	public AudioClip[] gunSounds = new AudioClip[10];

	// Use this for initialization
	void Start () {
		playerIn = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		gunAudioSource = GetComponent<AudioSource> ();
		barrel = transform.Find ("Barrel");

		//Persistent Object stuff
		PersistentGameObject PGO = GameObject.FindGameObjectWithTag ("PersistentObject").GetComponent<PersistentGameObject> ();
		PGO.setPlayerLevel(SceneManager.GetActiveScene ().buildIndex);
		weapon = PGO.getPlayerWeapon();
		anim.SetInteger ("weapon", weapon);
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




		if (Input.GetMouseButton (0) && !onCooldown) {


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
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-2, 2));
				onCooldown = true;
				cooldown = 1f;

			} else if (weapon == 1) {
				anim.SetBool ("shooting", true);
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
				cooldown = 0.2f;
			} else if (weapon == 2) {
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
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched4.GetComponent<Rigidbody2D> ().velocity = projectileLaunched4.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched5.GetComponent<Rigidbody2D> ().velocity = projectileLaunched5.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched6.GetComponent<Rigidbody2D> ().velocity = projectileLaunched6.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched7.GetComponent<Rigidbody2D> ().velocity = projectileLaunched7.transform.right * (velocity + Random.Range (-2, 2));
				onCooldown = true;
				cooldown = 0.5f;


			} else if (weapon == 3) {
				anim.SetBool ("shooting", true);
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
			} else if (weapon == 4) {
				anim.SetTrigger ("Shoot");
				GameObject projectileLaunched = null;

				if (faceRight) {
					projectileLaunched = Instantiate (Rocket, barrel.position, actualRotate) as GameObject;

				} else {
					Vector3 posit = transform.localPosition;
					posit = new Vector3 (-posit.x, posit.y, posit.z);
					projectileLaunched = Instantiate (Rocket, barrel.position, actualRotate) as GameObject;
				}

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-1, 1));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * velocity;
				onCooldown = true;
				cooldown = 0.5f;
			}

			gunAudioSource.PlayOneShot (gunSounds [weapon]);
		} 

		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//ALTERNATE FIRE
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		else if (Input.GetMouseButton (1) && !onCooldown) {
					
			if (weapon == 0) {
				anim.SetTrigger ("Shoot");

				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;
				GameObject projectileLaunched4 = null;
				GameObject projectileLaunched5 = null;
				GameObject projectileLaunched6 = null;
				GameObject projectileLaunched7 = null;
				GameObject projectileLaunched8 = null;
				GameObject projectileLaunched9 = null;

					projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched4 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched5 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched6 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched7 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched8 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
					projectileLaunched9 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched4.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched5.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched6.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched7.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched8.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched9.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched4.GetComponent<Rigidbody2D> ().velocity = projectileLaunched4.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched5.GetComponent<Rigidbody2D> ().velocity = projectileLaunched5.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched6.GetComponent<Rigidbody2D> ().velocity = projectileLaunched6.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched7.GetComponent<Rigidbody2D> ().velocity = projectileLaunched7.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched8.GetComponent<Rigidbody2D> ().velocity = projectileLaunched8.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched9.GetComponent<Rigidbody2D> ().velocity = projectileLaunched9.transform.right * (velocity + Random.Range (-2, 2));
				onCooldown = true;
				cooldown = 0.6f;
				Unit scriptin = playerIn.GetComponent<Unit> ();
				scriptin.playerEnergyChange (-10f);

			} else if (weapon == 1) {
				anim.SetBool ("shooting", true);
				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;

				projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-2, 2));
				onCooldown = true;
				cooldown = 0.1f;
				Unit scriptin = playerIn.GetComponent<Unit> ();
				scriptin.playerEnergyChange (-3f);

			} else if (weapon == 2) {
				anim.SetTrigger ("Shoot");

				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;
				GameObject projectileLaunched4 = null;
				GameObject projectileLaunched5 = null;
				GameObject projectileLaunched6 = null;
				GameObject projectileLaunched7 = null;
				GameObject projectileLaunched8 = null;
				GameObject projectileLaunched9 = null;
				GameObject projectileLaunched10 = null;
				GameObject projectileLaunched11 = null;
				GameObject projectileLaunched12 = null;
				GameObject projectileLaunched13 = null;
				GameObject projectileLaunched14 = null;
				GameObject projectileLaunched15 = null;
				GameObject projectileLaunched16 = null;
				GameObject projectileLaunched17 = null;
				GameObject projectileLaunched18 = null;
				GameObject projectileLaunched19 = null;
				GameObject projectileLaunched20 = null;
				GameObject projectileLaunched21 = null;

		
				projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched4 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched5 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched6 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched7 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched8 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched9 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched10 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched11 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched12 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched13 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched14 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched15 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched16 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched17 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched18 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched19 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched20 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched21 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched4.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched5.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched6.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched7.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched8.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched9.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched10.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched11.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched12.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched13.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched14.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched15.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched16.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched17.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched18.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched19.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched20.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched21.transform.Rotate (0, 0, Random.Range (-15, 15));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched4.GetComponent<Rigidbody2D> ().velocity = projectileLaunched4.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched5.GetComponent<Rigidbody2D> ().velocity = projectileLaunched5.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched6.GetComponent<Rigidbody2D> ().velocity = projectileLaunched6.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched7.GetComponent<Rigidbody2D> ().velocity = projectileLaunched7.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched8.GetComponent<Rigidbody2D> ().velocity = projectileLaunched8.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched9.GetComponent<Rigidbody2D> ().velocity = projectileLaunched9.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched10.GetComponent<Rigidbody2D> ().velocity = projectileLaunched10.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched11.GetComponent<Rigidbody2D> ().velocity = projectileLaunched11.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched12.GetComponent<Rigidbody2D> ().velocity = projectileLaunched12.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched13.GetComponent<Rigidbody2D> ().velocity = projectileLaunched13.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched14.GetComponent<Rigidbody2D> ().velocity = projectileLaunched14.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched15.GetComponent<Rigidbody2D> ().velocity = projectileLaunched15.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched16.GetComponent<Rigidbody2D> ().velocity = projectileLaunched16.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched17.GetComponent<Rigidbody2D> ().velocity = projectileLaunched17.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched18.GetComponent<Rigidbody2D> ().velocity = projectileLaunched18.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched19.GetComponent<Rigidbody2D> ().velocity = projectileLaunched19.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched20.GetComponent<Rigidbody2D> ().velocity = projectileLaunched20.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched21.GetComponent<Rigidbody2D> ().velocity = projectileLaunched21.transform.right * (velocity + Random.Range (-3, 3));
				onCooldown = true;
				cooldown = 0.3f;
				Unit scriptin = playerIn.GetComponent<Unit> ();
				scriptin.playerEnergyChange (-10f);

			} else if (weapon == 3) {
				anim.SetBool ("shooting", true);
				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;

		
				projectileLaunched = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched2 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;
				projectileLaunched3 = Instantiate (projectile, barrel.position, actualRotate) as GameObject;


				projectileLaunched.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-5, 5));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-2, 2));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-2, 2));

				onCooldown = true;
				cooldown = 0.05f;
				Unit scriptin = playerIn.GetComponent<Unit> ();
				scriptin.playerEnergyChange (-1.5f);

			} else if (weapon == 4) {
				anim.SetTrigger ("Shoot");
				GameObject projectileLaunched = null;
				GameObject projectileLaunched2 = null;
				GameObject projectileLaunched3 = null;

				projectileLaunched = Instantiate (Rocket, barrel.position, actualRotate) as GameObject;
				projectileLaunched2= Instantiate (Rocket, barrel.position, actualRotate) as GameObject;
				projectileLaunched3 = Instantiate (Rocket, barrel.position, actualRotate) as GameObject;

				projectileLaunched.transform.Rotate (0, 0, Random.Range (-7, 7));
				projectileLaunched2.transform.Rotate (0, 0, Random.Range (-7, 7));
				projectileLaunched3.transform.Rotate (0, 0, Random.Range (-7, 7));
				projectileLaunched.GetComponent<Rigidbody2D> ().velocity = projectileLaunched.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched2.GetComponent<Rigidbody2D> ().velocity = projectileLaunched2.transform.right * (velocity + Random.Range (-3, 3));
				projectileLaunched3.GetComponent<Rigidbody2D> ().velocity = projectileLaunched3.transform.right * (velocity + Random.Range (-3, 3));
				onCooldown = true;
				cooldown = 0.25f;
				Unit scriptin = playerIn.GetComponent<Unit> ();
				scriptin.playerEnergyChange (-5f);
			}

			gunAudioSource.PlayOneShot (gunSounds [weapon]);


		}

		else if(!Input.GetMouseButton (0) && !Input.GetMouseButton (1) ) {
			anim.SetBool ("shooting", false);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if(!(transform.parent.GetComponent<Unit>()).facingRight) {
		//	Flip();
		//}

		if (onCooldown) {
			cooldownTimer += Time.fixedDeltaTime;
			if (cooldownTimer > cooldown) {
				onCooldown = false;
				cooldownTimer = 0f;
			}
		} else {
			cooldownTimer = 0f;
		}

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
		anim.SetInteger ("weapon", weapon);

	}
}
