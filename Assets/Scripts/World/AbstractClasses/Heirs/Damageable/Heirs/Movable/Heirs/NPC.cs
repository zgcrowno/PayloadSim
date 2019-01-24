using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Class which denotes an entity with a level of agency, regardless of whether it's organic (i.e. human) or not (i.e. robotic)
 */ 
public abstract class NPC : Movable {

    public float hpWeight; //The weight to be applied to hp, when the latter is considered as a factor in this NPC's decision-making process
    public float hpRateWeight; //The weight to be applied to hpRate, when the latter is considered as a factor in this NPC's decision-making process
    public float proximityWeight; //The weight to be applied to this NPC's proximity to its potential behaviors/actions when engaged in the decision-making process

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
