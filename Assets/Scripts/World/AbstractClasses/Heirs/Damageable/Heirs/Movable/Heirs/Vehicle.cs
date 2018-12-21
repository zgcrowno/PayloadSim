using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : Movable, IContainer, IHackable {

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
        contents = new List<Consumable>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
