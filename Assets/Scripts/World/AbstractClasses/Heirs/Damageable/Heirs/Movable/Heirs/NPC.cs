using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Class which denotes an entity with a level of agency, regardless of whether it's organic (i.e. human) or not (i.e. robotic)
 */
public abstract class NPC : Movable
{

    public const int UtilityTimerLimit = 15; //15 is chosen as the limit because if this game is running at 60 FPS, 15 frames amounts to about 250-260 milliseconds, which is in line with a basic human's response time

    public float speed; //The speed at which this NPC moves through the world
    public float hpWeight; //The weight to be applied to hp, when the latter is considered as a factor in this NPC's decision-making process
    public float hpRateWeight; //The weight to be applied to hpRate, when the latter is considered as a factor in this NPC's decision-making process
    public float proximityWeight; //The weight to be applied to this NPC's proximity to its potential behaviors/actions when engaged in the decision-making process
    public int utilityTimer; //The timer which, upon reaching a value of UtilityTimerLimit, indicates that we may re-evaluate the utilities of the various choices this NPC can make
    public bool incrementUtilityTimer; //This bool represents whether or not we ought to start incrementing utilityTimer to allow for a utility calculation
    public bool executeBehavior; //This bool represents whether or not this NPC ought to engage in whatever behavior's been assigned to it

    public Behavior currentBehavior; //The current Behavior of this NPC (the multifaceted goal in which they're presently engaged)

    public ResponseCurve utilityCurve; //The ResponseCurve which represents all of this NPC's possible behaviors, and their associated utilities

    // Use this for initialization
    public new void Start()
    {
        base.Start();
        utilityTimer = 0;
        incrementUtilityTimer = true;
        InitUtilityCurve();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (incrementUtilityTimer)
        {
            if (GetType() == typeof(Human)) //This NPC is a human, so we increment utilityTimer to simulate an actual human's reaction time
            {
                utilityTimer++;

                if (utilityTimer >= UtilityTimerLimit)
                {
                    SetNewBehavior();
                }
            }
            else //This NPC is non-human/robotic, so we execute its utility functions immediately
            {
                SetNewBehavior();
            }
        }
        if(executeBehavior)
        {
            Autonomy();
        }
    }

    public void SetNewBehavior()
    {
        CalculateUtility(this, false);
        currentBehavior = utilityCurve.GetBehavior();
        executeBehavior = true;
        ResetUtilityTimer();
    }

    public void ResetUtilityTimer()
    {
        utilityTimer = 0;
        incrementUtilityTimer = false;
    }

    public void FinishBehavior()
    {
        executeBehavior = false;
        incrementUtilityTimer = true;
    }

    public abstract void Autonomy();

    public abstract void CalculateUtility(NPC npc, bool sub);

    public abstract void InitUtilityCurve();
}

public enum Behavior
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
