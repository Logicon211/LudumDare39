﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBossStart : MonoBehaviour {
    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        Unit scriptin = player.GetComponent<Unit>();
        scriptin.playerEnergyChange(50f);
        scriptin.playerHealthChange(100);

    }
	
}
