﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Immovable, IRegenerative {

    //START interface properties
    public float regenRate
    {
        get;
        set;
    }
    //END interface properties

    // Use this for initialization
    public new void Start () {
        base.Start();
        designation = "Bed";
        cls = "Furniture";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //START interface methods
    public void Regen(NPC npc)
    {

    }
    //END interface methods
}
