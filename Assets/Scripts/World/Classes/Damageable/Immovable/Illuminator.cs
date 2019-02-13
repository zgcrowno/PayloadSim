using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminator : Immovable, IHackable {

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
        designation = "Light";
        cls = "Fixtures";
	}

    public new void Update()
    {
        base.Update();
    }
}
