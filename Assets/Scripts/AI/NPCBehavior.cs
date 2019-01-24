using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior {

    public BehaviorType behaviorType;

    public List<NPCAction> actions;

    public NPCBehavior()
    {
        this.actions = new List<NPCAction>();
        this.behaviorType = BehaviorType.Work;
    }

    public NPCBehavior(List<NPCAction> actions)
    {
        this.actions = actions;
        this.behaviorType = BehaviorType.Work;
    }

    public NPCBehavior(BehaviorType behaviorType)
    {
        this.actions = new List<NPCAction>();
        this.behaviorType = behaviorType;
    }

    public NPCBehavior(List<NPCAction> actions, BehaviorType behaviorType)
    {
        this.actions = actions;
        this.behaviorType = behaviorType;
    }
}

public enum BehaviorType
{
    Hydrate,
    Satisfy,
    Energize,
    Urinate,
    Defecate,
    Temper,
    Clean,
    Socialize,
    Relax,
    Work,
    Report,
    Resolve
}
