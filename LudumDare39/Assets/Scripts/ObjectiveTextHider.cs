using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTextHider : MonoBehaviour {
    float timeElapsed = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed > 6.0)
        {
            Destroy(this.gameObject);
        }

    }
}
