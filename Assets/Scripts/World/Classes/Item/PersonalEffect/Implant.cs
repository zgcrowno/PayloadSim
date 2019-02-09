using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implant : PersonalEffect, IHackable {

    //START interface properties
    public FirewallLevel firewallLevel
    {
        get;
        set;
    }
    //END interface properties

    // Use this for initialization
    public new void Start () {
        base.Start();
        designation = "B.U.D.I.";
        cls = "Electronics";
	}

    public new void Update()
    {
        base.Update();
    }
}
