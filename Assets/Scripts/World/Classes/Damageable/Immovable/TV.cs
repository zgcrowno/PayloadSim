using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Immovable, IHackable, IRegenerative {

    //START interface properties
    public float regenRate
    {
        get;
        set;
    }

    public FirewallLevel firewallLevel
    {
        get;
        set;
    }

    public bool bypassed
    {
        get;
        set;
    }
    //END interface properties

    // Use this for initialization
    public new void Start () {
        base.Start();
        designation = "TV";
        cls = "Electronics";
	}

    public new void Update()
    {
        base.Update();
    }

    //START interface methods
    public void Regen(NPC npc)
    {

    }
    //END interface methods
}
