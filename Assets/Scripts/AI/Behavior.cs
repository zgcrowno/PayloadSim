using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior {

    public List<NPCAction> actions;

    public Behavior(List<NPCAction> actions)
    {
        this.actions = actions;
    }
}
