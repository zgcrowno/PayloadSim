using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Hackable {

    public int capacity; //The amount (in trivial units) this Container can hold
    public int contentSize; //The amount (in trivial units) that this Container is currently holding
    public bool wasteFriendly; //Whether or not this Container is a viable holder of human waste (and thus usable as a toilet)

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
