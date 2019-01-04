using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : PersonalEffect, IHackable {

	// Use this for initialization
	public new void Start () {
        base.Start();
        designation = "Phone";
        cls = "Electronics";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
