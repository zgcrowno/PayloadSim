using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket {

    public const int Granularity = 100;

    public const float LogitXMin = 0.5f; //The returned y-value of the logit function is 0 at x = 0.5, so we want logitX to always be equal to at least 0.5

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

        Drink nearestDrink;
        Drink farthestDrink;
        Food nearestFood;
        Food farthestFood;
        Bed nearestBed;
        Bed farthestBed;
        Toilet nearestToilet;
        Toilet farthestToilet;
        Thermostat nearestThermostat;
        Thermostat farthestThermostat;
        Shower nearestShower;
        Shower farthestShower;
        Human nearestHuman;
        Human farthestHuman;
        Couch nearestCouch;
        Couch farthestCouch;
        Computer nearestComputer;
        Computer farthestComputer;

        float normalizedHP;
        float normalizedHPRate;
        float normalizedProximity;
        float hpFactor;
        float hpRateFactor;
        float proximityFactor;
        float normalizedResult;
        float logitX;
        float logitResult;

        int utility;

        switch (behaviorType)
        {
            case BehaviorType.Hydrate:
                nearestDrink = human.GetObjectOfTypeWithShortestPath(typeof(Drink)) as Drink;
                farthestDrink = human.GetObjectOfTypeWithLongestPath(typeof(Drink)) as Drink;

                float normalizedThirst = human.thirst / Clickable.MaxValue;
                float normalizedThirstRate = human.thirstRate / Clickable.MaxValue;
                normalizedHP = human.hp / Clickable.MaxValue;
                normalizedHPRate = human.hpRate / Clickable.MaxValue;
                normalizedProximity = nearestDrink != null && farthestDrink != null ? human.CalculatePathLength(nearestDrink.transform.position) / human.CalculatePathLength(farthestDrink.transform.position) : 1;
                float thirstFactor = normalizedThirst * human.thirstWeight;
                float thirstRateFactor = normalizedThirstRate * human.thirstRateWeight;
                hpFactor = (1 - normalizedHP) * human.hpWeight;
                hpRateFactor = (1 - normalizedHPRate) * human.hpRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (thirstFactor + thirstRateFactor + hpFactor + hpRateFactor + proximityFactor) / (human.thirstWeight + human.thirstRateWeight + human.hpWeight + human.hpRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if(utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Satisfy:
                nearestFood = human.GetObjectOfTypeWithShortestPath(typeof(Food)) as Food;
                farthestFood = human.GetObjectOfTypeWithLongestPath(typeof(Food)) as Food;

                float normalizedHunger = human.hunger / Clickable.MaxValue;
                float normalizedHungerRate = human.hungerRate / Clickable.MaxValue;
                normalizedHP = human.hp / Clickable.MaxValue;
                normalizedHPRate = human.hpRate / Clickable.MaxValue;
                normalizedProximity = nearestFood != null && farthestFood != null ? human.CalculatePathLength(nearestFood.transform.position) / human.CalculatePathLength(farthestFood.transform.position) : 1;
                float hungerFactor = normalizedHunger * human.hungerWeight;
                float hungerRateFactor = normalizedHungerRate * human.hungerRateWeight;
                hpFactor = (1 - normalizedHP) * human.hpWeight;
                hpRateFactor = (1 - normalizedHPRate) * human.hpRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (hungerFactor + hungerRateFactor + hpFactor + hpRateFactor + proximityFactor) / (human.hungerWeight + human.hungerRateWeight + human.hpWeight + human.hpRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Energize:
                nearestBed = human.GetObjectOfTypeWithShortestPath(typeof(Bed)) as Bed;
                farthestBed = human.GetObjectOfTypeWithLongestPath(typeof(Bed)) as Bed;

                float normalizedFatigue = human.fatigue / Clickable.MaxValue;
                float normalizedFatigueRate = human.fatigueRate / Clickable.MaxValue;
                normalizedHP = human.hp / Clickable.MaxValue;
                normalizedHPRate = human.hpRate / Clickable.MaxValue;
                normalizedProximity = nearestBed != null && farthestBed != null ? human.CalculatePathLength(nearestBed.transform.position) / human.CalculatePathLength(farthestBed.transform.position) : 1;
                float fatigueFactor = normalizedFatigue * human.fatigueWeight;
                float fatigueRateFactor = normalizedFatigueRate * human.fatigueRateWeight;
                hpFactor = (1 - normalizedHP) * human.hpWeight;
                hpRateFactor = (1 - normalizedHPRate) * human.hpRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (fatigueFactor + fatigueRateFactor + hpFactor + hpRateFactor + proximityFactor) / (human.fatigueWeight + human.fatigueRateWeight + human.hpWeight + human.hpRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Urinate:
                nearestToilet = human.GetObjectOfTypeWithShortestPath(typeof(Toilet)) as Toilet;
                farthestToilet = human.GetObjectOfTypeWithLongestPath(typeof(Toilet)) as Toilet;

                float normalizedBladder = human.bladder / Clickable.MaxValue;
                float normalizedBladderRate = human.bladderRate / Clickable.MaxValue;
                normalizedProximity = nearestToilet != null && farthestToilet != null ? human.CalculatePathLength(nearestToilet.transform.position) / human.CalculatePathLength(farthestToilet.transform.position) : 1;
                float bladderFactor = normalizedBladder * human.bladderWeight;
                float bladderRateFactor = normalizedBladderRate * human.bladderRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (bladderFactor + bladderRateFactor + proximityFactor) / (human.bladderWeight + human.bladderRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Defecate:
                nearestToilet = human.GetObjectOfTypeWithShortestPath(typeof(Toilet)) as Toilet;
                farthestToilet = human.GetObjectOfTypeWithLongestPath(typeof(Toilet)) as Toilet;

                float normalizedStomach = human.stomach / Clickable.MaxValue;
                float normalizedStomachRate = human.stomachRate / Clickable.MaxValue;
                normalizedProximity = nearestToilet != null && farthestToilet != null ? human.CalculatePathLength(nearestToilet.transform.position) / human.CalculatePathLength(farthestToilet.transform.position) : 1;
                float stomachFactor = normalizedStomach * human.stomachWeight;
                float stomachRateFactor = normalizedStomachRate * human.stomachRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (stomachFactor + stomachRateFactor + proximityFactor) / (human.stomachWeight + human.stomachRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Temper:
                nearestThermostat = human.GetObjectOfTypeWithShortestPath(typeof(Thermostat)) as Thermostat;
                farthestThermostat = human.GetObjectOfTypeWithLongestPath(typeof(Thermostat)) as Thermostat;

                float normalizedTemperature = human.temperature / Clickable.MaxValue;
                float normalizedTemperatureRate = human.temperatureRate / Clickable.MaxValue;
                normalizedProximity = nearestThermostat != null && farthestThermostat != null ? human.CalculatePathLength(nearestThermostat.transform.position) / human.CalculatePathLength(farthestThermostat.transform.position) : 1;
                float temperatureFactor = (0.5f - normalizedTemperature) * human.temperatureWeight;
                float temperatureRateFactor = normalizedTemperatureRate * human.temperatureRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (temperatureFactor + temperatureRateFactor + proximityFactor) / (human.temperatureWeight + human.temperatureRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Clean:
                nearestShower = human.GetObjectOfTypeWithShortestPath(typeof(Shower)) as Shower;
                farthestShower = human.GetObjectOfTypeWithLongestPath(typeof(Shower)) as Shower;

                float normalizedUncleanliness = human.uncleanliness / Clickable.MaxValue;
                float normalizedUncleanlinessRate = human.uncleanlinessRate / Clickable.MaxValue;
                normalizedProximity = nearestShower != null && farthestShower != null ? human.CalculatePathLength(nearestShower.transform.position) / human.CalculatePathLength(farthestShower.transform.position) : 1;
                float uncleanlinessFactor = normalizedUncleanliness * human.uncleanlinessWeight;
                float uncleanlinessRateFactor = normalizedUncleanlinessRate * human.uncleanlinessRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (uncleanlinessFactor + uncleanlinessRateFactor + proximityFactor) / (human.uncleanlinessWeight + human.uncleanlinessRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Socialize:
                nearestHuman = human.GetObjectOfTypeWithShortestPath(typeof(Human)) as Human;
                farthestHuman = human.GetObjectOfTypeWithLongestPath(typeof(Human)) as Human;

                float normalizedLoneliness = human.loneliness / Clickable.MaxValue;
                float normalizedLonelinessRate = human.lonelinessRate / Clickable.MaxValue;
                normalizedProximity = nearestHuman != null && farthestHuman != null ? human.CalculatePathLength(nearestHuman.transform.position) / human.CalculatePathLength(farthestHuman.transform.position) : 1;
                float lonelinessFactor = normalizedLoneliness * human.lonelinessWeight;
                float lonelinessRateFactor = normalizedLonelinessRate * human.lonelinessRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (lonelinessFactor + lonelinessRateFactor + proximityFactor) / (human.lonelinessWeight + human.lonelinessRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Relax:
                nearestCouch = human.GetObjectOfTypeWithShortestPath(typeof(Couch)) as Couch;
                farthestCouch = human.GetObjectOfTypeWithLongestPath(typeof(Couch)) as Couch;

                float normalizedStress = human.stress / Clickable.MaxValue;
                float normalizedStressRate = human.stressRate / Clickable.MaxValue;
                normalizedProximity = nearestCouch != null && farthestCouch != null ? human.CalculatePathLength(nearestCouch.transform.position) / human.CalculatePathLength(farthestCouch.transform.position) : 1;
                float stressFactor = normalizedStress * human.stressWeight;
                float stressRateFactor = normalizedStressRate * human.stressRateWeight;
                proximityFactor = (1 - normalizedProximity) * human.proximityWeight;
                normalizedResult = (stressFactor + stressRateFactor + proximityFactor) / (human.stressWeight + human.stressRateWeight + human.proximityWeight);

                logitX = LogitXMin + (normalizedResult / 2); //Dividing normalizedResult by 2 so as to prevent logitX from exceeding a value of 1
                logitResult = MathUtil.Logit(logitX, (float)Math.E, false);
                utility = Mathf.CeilToInt(logitResult * Granularity);

                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Work:
                //nearestComputer = human.GetObjectOfTypeWithShortestPath(typeof(Computer)) as Computer;
                //farthestComputer = human.GetObjectOfTypeWithLongestPath(typeof(Computer)) as Computer;

                logitX = 0.7f;
                utility = Mathf.CeilToInt(MathUtil.Logit(logitX, (float)Math.E, true));
                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Report:
                //nearestHuman = human.GetObjectOfTypeWithShortestPath(typeof(Human)) as Human;
                //farthestHuman = human.GetObjectOfTypeWithLongestPath(typeof(Human)) as Human;

                logitX = 0.6f;
                utility = Mathf.CeilToInt(MathUtil.Logit(logitX, (float)Math.E, true));
                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
            case BehaviorType.Resolve:
                //nearestHuman = human.GetObjectOfTypeWithShortestPath(typeof(Human)) as Human;
                //farthestHuman = human.GetObjectOfTypeWithLongestPath(typeof(Human)) as Human;

                logitX = 0.6f;
                utility = Mathf.CeilToInt(MathUtil.Logit(logitX, (float)Math.E, true));
                if (utility <= 0)
                {
                    size = 1;
                }
                else
                {
                    size = utility;
                }
                break;
        }
    }
}
