using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : Immovable, IContainer, IHackable, IRegenerative {

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
    //END interface properties

    // Use this for initialization
    public new void Start () {
        base.Start();
        capacity = 10;
        contents = new List<Consumable>();
        wasteFriendly = true;
        designation = "Toilet";
        cls = "Appliances";
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
