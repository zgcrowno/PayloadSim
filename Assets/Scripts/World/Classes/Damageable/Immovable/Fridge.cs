using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Immovable, IContainer, IHackable {

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
        capacity = 50;
        contents = new List<Consumable>();
        contents.Add(new PuddingSkin());
        contents.Add(new Water());
        wasteFriendly = false;
        designation = "Fridge";
        cls = "Appliances";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
