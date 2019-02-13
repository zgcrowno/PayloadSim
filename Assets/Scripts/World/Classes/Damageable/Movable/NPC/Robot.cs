using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : NPC, IHackable {

    //START interface properties
    public FirewallLevel firewallLevel
    {
        get;
        set;
    }

    public bool bypassed
    {
        get;
        set;
    }
    //END interface properties

    // Use this for initialization
    public new void Start () {
        base.Start();
        designation = "Robot";
        cls = "Electronics";
	}

    public new void Update()
    {
        base.Update();
    }

    public override void Autonomy()
    {
        
    }

    public override void CalculateUtility(NPC npc, bool sub)
    {
        
    }

    public override void InitUtilityCurve()
    {
        
    }
}
