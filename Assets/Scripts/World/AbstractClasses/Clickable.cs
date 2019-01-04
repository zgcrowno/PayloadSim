using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour {

    public const float MaxValue = 1000; //The maximum value any of this Clickable's numerical attributes may reach

    public GameObject stage; //The stage in which this Clickable resides

    public string designation; //In short, the "name" of the object
    public string cls; //A string representing the "class" of the object (Human, Food, Personal Effect, etc.)
    public string description; //A long string representing the assets of this Clickable that will be visible to the player when focused upon

	// Use this for initialization
	public void Start () {
        stage = GameObject.Find("/Stage");
	}

    /*
     * Returns the object of the passed type which is nearest to this Clickable
     * @param type The passed type for which we're checking proximity
     * @return The Clickable of the passed type which is nearest to this Clickable
     */ 
    public Clickable GetNearestObjectOfType(Type type)
    {
        Clickable[] stageClickables = stage.GetComponentsInChildren<Clickable>();
        List<Clickable> stageClickablesOfType = new List<Clickable>();
        Clickable nearestClickableOfType = stageClickables[0];

        for(int i = 0; i < stageClickables.Length; i++)
        {
            if(stageClickables[i].GetType() == type)
            {
                stageClickablesOfType.Add(stageClickables[i]);
            }
        }

        foreach(Clickable clickable in stageClickablesOfType)
        {
            if(Vector3.Distance(transform.position, clickable.transform.position) < Vector3.Distance(transform.position, nearestClickableOfType.transform.position))
            {
                nearestClickableOfType = clickable;
            }
        }

        return nearestClickableOfType;
    }

    public virtual string GenerateDescription()
    {
        return "DESIGNATION: " + designation + "\n" + "CLASS: " + cls;
    }
}
