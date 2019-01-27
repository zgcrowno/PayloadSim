using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket {

    public const int Granularity = 100;

    public const float LogitXMin = 0.5f; //The returned y-value of the logit function is 0 at x = 0.5, so we want logitX to always be equal to at least 0.5

    public Behavior behavior; //The possible action this bucket represents

    public int size; //The size of this bucket, which has purpose only when compared to the sizes of its overarching data structure's other buckets (synonymous with relative utility)
    public int edge; //The right edge of this bucket, with the left edge being retrievable only by consulting the overarching data structure

    public Bucket()
    {
        behavior = Behavior.Work;
        size = 0;
        edge = 0;
    }

    public Bucket(Behavior behavior)
    {
        this.behavior = behavior;
        this.size = 0;
        this.edge = 0;
    }

    public Bucket(Behavior behavior, int size, int edge)
    {
        this.behavior = behavior;
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

        Sink nearestSink;
        Sink farthestSink;
        Fridge nearestFridge;
        Fridge farthestFridge;
        Bed nearestBed;
        Bed farthestBed;
        Toilet nearestToilet;
        Toilet farthestToilet;
        Thermostat nearestThermostat;
        Thermostat farthestThermostat;
        Shower nearestShower;
        Shower farthestShower;
        TV nearestTV;
        TV farthestTV;
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

        switch (behavior)
        {
            case Behavior.Hydrate:
                nearestSink = human.GetObjectOfTypeWithShortestPath(typeof(Sink)) as Sink;
                farthestSink = human.GetObjectOfTypeWithLongestPath(typeof(Sink)) as Sink;

                float normalizedThirst = human.thirst / Clickable.MaxValue;
                float normalizedThirstRate = human.thirstRate / Clickable.MaxValue;
                normalizedHP = human.hp / Clickable.MaxValue;
                normalizedHPRate = human.hpRate / Clickable.MaxValue;
                normalizedProximity = nearestSink != null && farthestSink != null ? human.CalculatePathLength(nearestSink.transform.position) / human.CalculatePathLength(farthestSink.transform.position) : 1;
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
            case Behavior.Satisfy:
                nearestFridge = human.GetObjectOfTypeWithShortestPath(typeof(Fridge)) as Fridge;
                farthestFridge = human.GetObjectOfTypeWithLongestPath(typeof(Fridge)) as Fridge;

                float normalizedHunger = human.hunger / Clickable.MaxValue;
                float normalizedHungerRate = human.hungerRate / Clickable.MaxValue;
                normalizedHP = human.hp / Clickable.MaxValue;
                normalizedHPRate = human.hpRate / Clickable.MaxValue;
                normalizedProximity = nearestFridge != null && farthestFridge != null ? human.CalculatePathLength(nearestFridge.transform.position) / human.CalculatePathLength(farthestFridge.transform.position) : 1;
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
            case Behavior.Energize:
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
            case Behavior.Urinate:
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
            case Behavior.Defecate:
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
            case Behavior.Temper:
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
            case Behavior.Clean:
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
            case Behavior.Socialize:
                nearestTV = human.GetObjectOfTypeWithShortestPath(typeof(TV)) as TV;
                farthestTV = human.GetObjectOfTypeWithLongestPath(typeof(TV)) as TV;

                float normalizedLoneliness = human.loneliness / Clickable.MaxValue;
                float normalizedLonelinessRate = human.lonelinessRate / Clickable.MaxValue;
                normalizedProximity = nearestTV != null && farthestTV != null ? human.CalculatePathLength(nearestTV.transform.position) / human.CalculatePathLength(farthestTV.transform.position) : 1;
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
            case Behavior.Relax:
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
            case Behavior.Work:
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
            case Behavior.Report:
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
            case Behavior.Resolve:
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
