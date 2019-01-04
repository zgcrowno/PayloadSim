using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminator : Immovable, IHackable {

	// Use this for initialization
	public new void Start () {
        base.Start();
        designation = "Light";
        cls = "Fixtures";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
