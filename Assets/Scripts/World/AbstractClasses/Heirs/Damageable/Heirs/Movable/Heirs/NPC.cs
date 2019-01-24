using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Class which denotes an entity with a level of agency, regardless of whether it's organic (i.e. human) or not (i.e. robotic)
 */ 
public abstract class NPC : Movable {

    public NPCBehavior behavior; //The current Behavior of this NPC (the multifaceted goal in which they're presently engaged)
    public ResponseCurve utilityCurve; //The ResponseCurve which represents all of this NPC's possible behaviors, and their associated utilities

    // Use this for initialization
    public new void Start()
    {
        base.Start();
        InitUtilityCurve();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void CalculateUtility(NPC npc);

    public abstract void InitUtilityCurve();
}
