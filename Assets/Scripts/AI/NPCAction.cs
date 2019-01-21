using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction {

    public ActionType type;

    public ResponseCurve responseCurve;

    public float value;
    public float utility;
    public float weight;
    public float weightedSum;
    public float function;

    public NPCAction()
    {
        this.type = ActionType.Work;
    }

    public NPCAction(ActionType type)
    {
        this.type = type;
    }
}

public enum ActionType
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
