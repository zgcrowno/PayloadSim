using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket {

    public BehaviorType behaviorType; //The possible action this bucket represents

    public int size; //The size of this bucket, which has purpose only when compared to the sizes of its overarching data structure's other buckets (synonymous with relative utility)
    public int edge; //The right edge of this bucket, with the left edge being retrievable only by consulting the overarching data structure

    public Bucket()
    {
        behaviorType = BehaviorType.Work;
        size = 0;
        edge = 0;
    }

    public Bucket(BehaviorType behaviorType)
    {
        this.behaviorType = behaviorType;
        this.size = 0;
        this.edge = 0;
    }

    public Bucket(BehaviorType behaviorType, int size, int edge)
    {
        this.behaviorType = behaviorType;
        this.size = size;
        this.edge = edge;
    }

    /*
     * Calculates and sets the utility (size) of this Bucket for the passed NPC
     * @param npc The passed NPC for whom we're calculating various utilities
     */
    public void CalculateUtility(NPC npc)
    {
        Human human = (Human)npc;

        switch (behaviorType)
        {
            case BehaviorType.Hydrate:
                int hydrateUtility = Mathf.CeilToInt(MathUtil.Logit(human.thirst / Clickable.MaxValue, (float)Math.E, true));
                if(hydrateUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = hydrateUtility;
                }
                break;
            case BehaviorType.Satisfy:
                int satisfyUtility = Mathf.CeilToInt(MathUtil.Logit(human.hunger / Clickable.MaxValue, (float)Math.E, true));
                if (satisfyUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = satisfyUtility;
                }
                break;
            case BehaviorType.Energize:
                int energizeUtility = Mathf.CeilToInt(MathUtil.Logit(human.fatigue / Clickable.MaxValue, (float)Math.E, true));
                if (energizeUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = energizeUtility;
                }
                break;
            case BehaviorType.Urinate:
                int urinateUtility = Mathf.CeilToInt(MathUtil.Logit(human.bladder / Clickable.MaxValue, (float)Math.E, true));
                if (urinateUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = urinateUtility;
                }
                break;
            case BehaviorType.Defecate:
                int defecateUtility = Mathf.CeilToInt(MathUtil.Logit(human.stomach / Clickable.MaxValue, (float)Math.E, true));
                if (defecateUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = defecateUtility;
                }
                break;
            case BehaviorType.Temper:
                int temperUtility = Mathf.CeilToInt(MathUtil.Logit(human.temperature / Clickable.MaxValue, (float)Math.E, true));
                if (temperUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = temperUtility;
                }
                break;
            case BehaviorType.Clean:
                int cleanUtility = Mathf.CeilToInt(MathUtil.Logit(human.uncleanliness / Clickable.MaxValue, (float)Math.E, true));
                if (cleanUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = cleanUtility;
                }
                break;
            case BehaviorType.Socialize:
                int socializeUtility = Mathf.CeilToInt(MathUtil.Logit(human.loneliness / Clickable.MaxValue, (float)Math.E, true));
                if (socializeUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = socializeUtility;
                }
                break;
            case BehaviorType.Relax:
                int relaxUtility = Mathf.CeilToInt(MathUtil.Logit(human.stress / Clickable.MaxValue, (float)Math.E, true));
                if (relaxUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = relaxUtility;
                }
                break;
            case BehaviorType.Work:
                int workUtility = Mathf.CeilToInt(MathUtil.Logit((Clickable.MaxValue - human.stress) / Clickable.MaxValue, (float)Math.E, true));
                if (workUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = workUtility;
                }
                break;
            case BehaviorType.Report:
                int reportUtility = Mathf.CeilToInt(MathUtil.Logit(human.awareness / Clickable.MaxValue, (float)Math.E, true));
                if (reportUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = reportUtility;
                }
                break;
            case BehaviorType.Resolve:
                int resolveUtility = Mathf.CeilToInt(MathUtil.Logit((Clickable.MaxValue - human.awareness) / Clickable.MaxValue, (float)Math.E, true));
                if (resolveUtility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = resolveUtility;
                }
                break;
        }
    }
}
