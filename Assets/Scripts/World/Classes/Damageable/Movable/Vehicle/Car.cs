﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle {

	// Use this for initialization
	public new void Start () {
        base.Start();
        capacity = 35;
        wasteFriendly = false;
        designation = "Car";
        cls = "Vehicle";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
