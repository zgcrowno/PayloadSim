using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Immovable, IContainer, IHackable {

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
    void Start () {
        capacity = 25;
        contents = new List<Consumable>();
        wasteFriendly = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
