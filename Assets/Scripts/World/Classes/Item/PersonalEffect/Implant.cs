using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implant : PersonalEffect, IHackable {

	// Use this for initialization
	public new void Start () {
        base.Start();
        designation = "B.U.D.I.";
        cls = "Electronics";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
