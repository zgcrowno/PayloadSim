using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : NPC, IHackable {

    // Use this for initialization
    public new void Start () {
        base.Start();
        designation = "Robot";
        cls = "Electronics";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
