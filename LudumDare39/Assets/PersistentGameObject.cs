using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObject : MonoBehaviour {

	public int playerLevel = 1;
	public int playerWeapon = 0;

	private static PersistentGameObject _instance;
	// Use this for initialization
	void Awake () {
		//DontDestroyOnLoad(transform.gameObject);

		//if we don't have an [_instance] set yet
		if (!_instance) {
			_instance = this;
			playerLevel = 1;
			playerWeapon = 0;
			Debug.Log ("creating instance of persistent object");
		}
		//otherwise, if we do, kill this thing
		else {
			Debug.Log ("destroying extra instance");
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
		Debug.Log ("Current level: " + playerLevel);
		Debug.Log ("Current weapon: " + playerWeapon);
	}

	// Update is called once per frame
	void Update () {

	}

	public int getPlayerLevel(){
		return playerLevel;
	}

	public void setPlayerLevel(int level) {
		playerLevel = level;
	}

	public int getPlayerWeapon() {
		return playerWeapon;
	}

	public void setPlayerWeapon(int weapon) {
		playerWeapon = weapon;
	}
}
