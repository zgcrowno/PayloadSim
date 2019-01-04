using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : Immovable, IContainer, IHackable {

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
    //END interface properties

    // Use this for initialization
    public new void Start () {
        base.Start();
        capacity = 80;
        contents = new List<Consumable>();
        wasteFriendly = false;
        designation = "Vending Machine";
        cls = "Electronics";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
