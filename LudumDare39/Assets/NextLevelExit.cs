using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelExit : MonoBehaviour {

	private GameObject player;

	public int sceneIndexToLoad = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (player.transform.position.x >= this.transform.position.x) {
			PersistentGameObject PGO = GameObject.FindGameObjectWithTag ("PersistentObject").GetComponent<PersistentGameObject> ();
			PGO.setPlayerWeapon (player.transform.Find ("RifleWeapon").gameObject.GetComponent<TrackMouse> ().weapon);

			LoadingScreenManager.LoadScene (sceneIndexToLoad);
		}
	}
}
