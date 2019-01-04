using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Immovable, IHackable {

	// Use this for initialization
	public new void Start () {
        base.Start();
        designation = "Computer";
        cls = "Electronics";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
