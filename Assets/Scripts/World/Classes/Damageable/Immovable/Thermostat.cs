using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermostat : Immovable, IHackable {

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
        designation = "Thermostat";
        cls = "Electronics";
	}

    public new void Update()
    {
        base.Update();
    }
}
