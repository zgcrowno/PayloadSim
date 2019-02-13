using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : PersonalEffect, IHackable {

    //START interface properties
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
        designation = "Phone";
        cls = "Electronics";
	}

    public new void Update()
    {
        base.Update();
    }
}
