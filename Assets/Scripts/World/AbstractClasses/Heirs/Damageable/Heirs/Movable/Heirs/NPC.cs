using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Class which denotes an entity with a level of agency, regardless of whether it's organic (i.e. human) or not (i.e. robotic)
 */ 
public abstract class NPC : Movable {

    public Behavior behavior;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
