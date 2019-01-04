using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : Clickable {

    public float hp;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Method by which this Damageable is destroyed or dies; it's called upon hp reaching zero
     */ 
    public void Die()
    {

    }

    public override string GenerateDescription()
    {
        return base.GenerateDescription() + "\n" + "HP: " + FloatUtil.AsPercentString(hp, MaxValue);
    }
}
