using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : Clickable {

    public float hp;
    public float hpRate; //The rate at which this Damageable loses hp in certain circumstances

    // Use this for initialization
    public new void Start()
    {
        base.Start();
    }

    public new void Update()
    {
        base.Update();
    }

    /*
     * Method by which this Damageable is destroyed or dies; it's called upon hp reaching zero
     */
    public void Die()
    {

    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    public override string GenerateDescription()
    {
        return base.GenerateDescription() + "\n" + "HP: " + FloatUtil.AsPercentString(hp, MaxValue);
    }
}
