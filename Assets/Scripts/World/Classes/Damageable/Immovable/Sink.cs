using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Immovable, IContainer, IHackable {

    //START interface properties
    public int capacity
    {
        get;
        set;
    }

    public List<Consumable> contents
    {
        get;
        set;
    }

    public bool wasteFriendly
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
        capacity = 20;
        contents = new List<Consumable>();
        wasteFriendly = true;
        designation = "Sink";
        cls = "Appliances";
	}

    public new void Update()
    {
        base.Update();
    }
}
