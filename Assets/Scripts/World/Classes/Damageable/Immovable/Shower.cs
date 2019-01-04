using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : Immovable, IHackable, IRegenerative {

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
        designation = "Shower";
        cls = "Appliances";
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
