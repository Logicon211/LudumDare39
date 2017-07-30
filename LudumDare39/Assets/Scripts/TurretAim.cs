using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D RB;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //RB = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        Vector3 dir = pos - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		Debug.Log (dir);

        //We need 2 rotations stored so that we can display them correctly when the model is flipped left, but also need the actual rotate for when we spawn new rockets down below
        Quaternion actualRotate = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = actualRotate;

		Debug.Log (actualRotate);
    }
}
