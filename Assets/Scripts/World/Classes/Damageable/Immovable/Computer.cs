using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Immovable, IHackable {

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
        designation = "Computer";
        cls = "Electronics";

        //Setting this value here for testing purposes
        firewallLevel = FirewallLevel.One;
	}

    public new void Update()
    {
        base.Update();
    }
}
