using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : Immovable, IContainer, IHackable {

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
        capacity = 35;
        contents = new List<Consumable>();
        wasteFriendly = false;
        designation = "Cabinet";
        cls = "Fixtures";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
