﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Drink {

	// Use this for initialization
	public new void Start () {
        base.Start();
        designation = "Water";
        cls = "Beverage";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}