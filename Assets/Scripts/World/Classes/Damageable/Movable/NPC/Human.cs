using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : NPC {
    public const float OptimalTemp = 98.6f; //The optimal body temperature of a Human

    public const int CircadianSleep = 0;
    public const int CircadianWake = 1;

    public const int WorkHoursIn = 0;
    public const int WorkHoursOut = 1;

    //Days of the week
    public const int Monday = 0;
    public const int Tuesday = 1;
    public const int Wednesday = 2;
    public const int Thursday = 3;
    public const int Friday = 4;
    public const int Saturday = 5;
    public const int Sunday = 6;

    //Potential perks (buff, debuff or mix) to be held by this NPC
    public const int Epileptic = 0;
    public const int Loner = 1;
    public const int Hemorrhoids = 2;
    public const int Diabetes = 3;
    public const int LactoseIntolerance = 4;
    public const int GlutenSensitivity = 5;
    public const int Narcolepsy = 6;

    //Variables which may represent both the sex of the NPC, and of those other NPCs to which this one may be attracted
    public const int Male = 0;
    public const int Female = 1;
    public const int Either = 2;
    public const int Neither = 3;

    public Dictionary<int, float> circadian = new Dictionary<int, float>(); //Contains the time(s) at which this NPC typically goes to sleep and wakes up
    public Dictionary<int, float> workHours = new Dictionary<int, float>(); //Contains the time(s) at which this NPC typically clocks in and clocks out of work
    public List<int> workDays = new List<int>(); //Contains the days this NPC typically works
    public List<int> perks = new List<int>(); //Contains the perks which apply to this NPC
    
    public int sex; //The NPC's sex
    public int attractedTo; //The sex to which this NPC is attracted
    public int intention; //The current intention of this Human (the goal in which they're presently engaged)
    public float thirst; //Measures how much this NPC needs to hydrate themselves
    public float thirstWeight; //The weight to be applied to thirst, when the latter is considered as a factor in this NPC's decision-making process
    public float thirstRate; //The rate at which this NPC's thirst increases
    public float thirstRateWeight; //The weight to be applied to thirstRate, when the latter is considered as a factor in this NPC's decision-making process
    public float bladder; //Measures how much this NPC needs to urinate
    public float bladderWeight; //The weight to be applied to bladder, when the latter is considered as a factor in this NPC's decision-making process
    public float bladderRate; //The rate at which this NPC's bladder fills up when consuming certain things
    public float bladderRateWeight; //The weight to be applied to bladderRate, when the latter is considered as a factor in this NPC's decision-making process
    public float hunger; //Measures how much this NPC needs to feed themselves
    public float hungerWeight; //The weight to be applied to hunger, when the latter is considered as a factor in this NPC's decision-making process
    public float hungerRate; //The rate at which this NPC's hunger increases
    public float hungerRateWeight; //The weight to be applied to hungerRate, when the latter is considered as a factor in this NPC's decision-making process
    public float stomach; //Measures how much this NPC needs to defecate
    public float stomachWeight; //The weight to be applied to stomach, when the latter is considered as a factor in this NPC's decision-making process
    public float stomachRate; //The rate at which this NPC's stomach fills up when consuming certain things
    public float stomachRateWeight; //The weight to be applied to stomachRate, when the latter is considered as a factor in this NPC's decision-making process
    public float stress; //Measures how much this NPC needs to take a break or relax
    public float stressWeight; //The weight to be applied to stress, when the latter is considered as a factor in this NPC's decision-making process
    public float stressRate; //The rate at which this NPC accumulates stress
    public float stressRateWeight; //The weight to be applied to stressRate, when the latter is considered as a factor in this NPC's decision-making process
    public float temperature; //Measures how hot or cold this NPC is
    public float temperatureWeight; //The weight to be applied to temperature, when the latter is considered as a factor in this NPC's decision-making process
    public float temperatureRate; //The rate at which this NPC's temperature deviates from the norm under certain circumstances
    public float temperatureRateWeight; //The weight to be applied to temperatureRate, when the latter is considered as a factor in this NPC's decision-making process
    public float fatigue; //Measures how tired this NPC is
    public float fatigueWeight; //The weight to be applied to fatigue, when the latter is considered as a factor in this NPC's decision-making process
    public float fatigueRate; //The rate at which this NPC accumulates fatigue
    public float fatigueRateWeight; //The weight to be applied to fatigueRate, when the latter is considered as a factor in this NPC's decision-making process
    public float uncleanliness; //Measures how hygienic this NPC is
    public float uncleanlinessWeight; //The weight to be applied to uncleanliness, when the latter is considered as a factor in this NPC's decision-making process
    public float uncleanlinessRate; //The rate at which this NPC either becomes dirty or develops body odor
    public float uncleanlinessRateWeight; //The weight to be applied to uncleanlinessRate, when the latter is considered as a factor in this NPC's decision-making process
    public float loneliness; //Measures how in-need of companionship/conversation this NPC is
    public float lonelinessWeight; //The weight to be applied to loneliness, when the latter is considered as a factor in this NPC's decision-making process
    public float lonelinessRate; //The rate at which this NPC's loneliness increases
    public float lonelinessRateWeight; //The weight to be applied to lonelinessRate, when the latter is considered as a factor in this NPC's decision-making process
    public float awareness; //Measures how aware this NPC currently is of the player's actions/presence
    public float awarenessRate; //The rate at which this NPC becomes aware/unaware of the player's actions/presence when witness to evidence thereof
    public bool well; //Whether this NPC is well or ill

    public NPCBehavior currentBehavior;

    // Use this for initialization
    public new void Start () {
        //TODO: A lot of this information will be set in specific character classes. We're just setting here for testing purposes
        base.Start();
        hp = MaxValue;
        hpWeight = 3;
        hpRate = 0.01f;
        hpRateWeight = 2.5f;
        proximityWeight = 1.8f;

        circadian.Add(CircadianSleep, 2300);
        circadian.Add(CircadianWake, 0800);
        workHours.Add(WorkHoursIn, 0900);
        workHours.Add(WorkHoursOut, 1700);
        workDays.Add(Monday);
        workDays.Add(Tuesday);
        workDays.Add(Wednesday);
        workDays.Add(Thursday);
        workDays.Add(Friday);
        perks.Add(Hemorrhoids);

        //All of the below values are set here for testing purposes only, but will later simply be set in the inspector
        designation = "Sommers, Harold"; 
        cls = "Human"; 
        sex = Male;
        attractedTo = Female;
        thirst = 0;
        thirstWeight = 2;
        thirstRate = 0.1f;
        thirstRateWeight = 1.75f;
        bladder = 1;
        bladderWeight = 1.5f;
        bladderRate = 5;
        bladderRateWeight = 1.25f;
        hunger = 0;
        hungerWeight = 1.8f;
        hungerRate = 0.1f;
        hungerRateWeight = 1.6f;
        stomach = 0;
        stomachWeight = 1.5f;
        stomachRate = 5;
        stomachRateWeight = 1.25f;
        stress = 0;
        stressWeight = 1;
        stressRate = 0.1f;
        stressRateWeight = 0.9f;
        temperature = MaxValue / 2;
        temperatureWeight = 1;
        temperatureRate = 0.001f;
        temperatureRateWeight = 0.9f;
        fatigue = 0;
        fatigueWeight = 1.2f;
        fatigueRate = 0.1f;
        fatigueRateWeight = 1.05f;
        uncleanliness = 0;
        uncleanlinessWeight = 0.7f;
        uncleanlinessRate = 0.1f;
        uncleanlinessRateWeight = 0.5f;
        loneliness = 0;
        lonelinessWeight = 1.1f;
        lonelinessRate = 0.1f;
        lonelinessRateWeight = 0.9f;
        awareness = 0;
        awarenessRate = 0.5f;
        well = true;

        currentBehavior = new NPCBehavior(BehaviorType.Work);
    }

    public void FixedUpdate()
    {
        Depreciate();
        CalculateUtility(this);
        Autonomy();
    }

    /*
     * Makes this Human consume the passed consumable
     * @param con The passed Consumable
     */
    public void Consume(Consumable con)
    {

    }

    /*
     * The catch-all method by which this Human completely governs themselves and their actions. This method is based on Maslow's hierarchy of needs, with more fundamental physiological needs taking precedence over more cerebral/human ones.
     */ 
    public void Autonomy()
    {
        currentBehavior.behaviorType = utilityCurve.GetBehaviorType();
        print("Current Behavior: " + currentBehavior);
        foreach (Bucket bucket in utilityCurve.buckets)
        {
            print(bucket.behaviorType + ": " + bucket.size + ", " + bucket.edge);
        }
        if (currentBehavior.behaviorType == BehaviorType.Hydrate)
        {
            Drink nearestDrink = GetObjectOfTypeWithShortestPath(typeof(Drink)) as Drink;
            if(nearestDrink != null)
            {
                SetDestination(nearestDrink.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Satisfy)
        {
            Food nearestFood = GetObjectOfTypeWithShortestPath(typeof(Food)) as Food;
            if(nearestFood != null)
            {
                SetDestination(nearestFood.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Urinate)
        {
            Toilet nearestToilet = GetObjectOfTypeWithShortestPath(typeof(Toilet)) as Toilet;
            if(nearestToilet != null)
            {
                SetDestination(nearestToilet.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Defecate)
        {
            Toilet nearestToilet = GetObjectOfTypeWithShortestPath(typeof(Toilet)) as Toilet;
            if(nearestToilet != null)
            {
                SetDestination(nearestToilet.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Temper)
        {
            Thermostat nearestThermostat = GetObjectOfTypeWithShortestPath(typeof(Thermostat)) as Thermostat;
            if(nearestThermostat != null)
            {
                SetDestination(nearestThermostat.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Energize)
        {
            Bed nearestBed = GetObjectOfTypeWithShortestPath(typeof(Bed)) as Bed;
            if(nearestBed != null)
            {
                SetDestination(nearestBed.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Clean)
        {
            Shower nearestShower = GetObjectOfTypeWithShortestPath(typeof(Shower)) as Shower;
            if(nearestShower != null)
            {
                SetDestination(nearestShower.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Socialize)
        {
            Human nearestHuman = GetObjectOfTypeWithShortestPath(typeof(Human)) as Human;
            if(nearestHuman != null && nearestHuman != this)
            {
                SetDestination(nearestHuman.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Relax)
        {
            Couch nearestCouch = GetObjectOfTypeWithShortestPath(typeof(Couch)) as Couch;
            if(nearestCouch != null)
            {
                SetDestination(nearestCouch.transform.position);
            }
        }
        else if(currentBehavior.behaviorType == BehaviorType.Work)
        {
            Computer nearestComputer = GetObjectOfTypeWithShortestPath(typeof(Computer)) as Computer;
            if(nearestComputer != null)
            {
                SetDestination(nearestComputer.transform.position);
            }
        }
    }

    /*
     * The catch-all method by which this Human's attributes depreciate over time
     */ 
    public void Depreciate()
    {
        if(thirst < MaxValue)
        {
            thirst += thirstRate;
        }
        else
        {
            thirst = MaxValue;
            TakeDamage(hpRate);
        }

        if(hunger < MaxValue)
        {
            hunger += hungerRate;
        }
        else
        {
            hunger = MaxValue;
            TakeDamage(hpRate);
        }

        if(!well)
        {
            if(thirst < MaxValue)
            {
                thirst += thirstRate / 100;
            }
            TakeDamage(hpRate);
        }

        if (temperature > OptimalTemp)
        {
            fatigue += fatigueRate;
        }
        else if(temperature < OptimalTemp)
        {
            if(Random.Range(0, MaxValue) < 1)
            {
                well = false;
            }
        }

        if (fatigue < MaxValue)
        {
            fatigue += fatigueRate;
        }
        else
        {
            fatigue = MaxValue;

            if (Random.Range(0, MaxValue) < 1)
            {
                well = false;
            }
        }

        if(uncleanliness < MaxValue)
        {
            uncleanliness += uncleanlinessRate;
        }
        else
        {
            uncleanliness = MaxValue;

            if (Random.Range(0, MaxValue) < 1)
            {
                well = false;
            }
        }

        if(loneliness < MaxValue)
        {
            loneliness += lonelinessRate;
        }
        else
        {
            loneliness = MaxValue;
        }

        if(stress < MaxValue)
        {
            stress += stressRate;
        }
        else
        {
            stress = MaxValue;
        }
    }

    public override void CalculateUtility(NPC npc)
    {
        utilityCurve.CalculateUtility(npc);
    }

    public override void InitUtilityCurve()
    {
        List<Bucket> behaviorBuckets = new List<Bucket>()
        {
            new Bucket(BehaviorType.Hydrate),
            new Bucket(BehaviorType.Satisfy),
            new Bucket(BehaviorType.Energize),
            new Bucket(BehaviorType.Urinate),
            new Bucket(BehaviorType.Defecate),
            new Bucket(BehaviorType.Temper),
            new Bucket(BehaviorType.Clean),
            new Bucket(BehaviorType.Socialize),
            new Bucket(BehaviorType.Relax),
            new Bucket(BehaviorType.Work),
            new Bucket(BehaviorType.Report),
            new Bucket(BehaviorType.Resolve)
        };

        utilityCurve = new ResponseCurve(behaviorBuckets);

        CalculateUtility(this);
    }

    public override string GenerateDescription()
    {
        return base.GenerateDescription() + "\n" + "SEX: " + (sex == Male ? "M" : "F") + "\n" + "THIRST: " + FloatUtil.AsPercentString(thirst, MaxValue) + "\n" + "BLADDER: " + FloatUtil.AsPercentString(bladder, MaxValue) + "\n" + "HUNGER: " + FloatUtil.AsPercentString(hunger, MaxValue) + "\n" + "STOMACH: " + FloatUtil.AsPercentString(stomach, MaxValue) + "\n" + "STRESS: " + FloatUtil.AsPercentString(stress, MaxValue) + "\n" + "TEMPERATURE: " + FloatUtil.AsDegreeString(98.6f - (((MaxValue / 2) - temperature) / 10)) + "\n" + "FATIGUE: " + FloatUtil.AsPercentString(fatigue, MaxValue) + "\n" + "WELLNESS: " + (well ? "Healthy" : "Ill");
    }
}
