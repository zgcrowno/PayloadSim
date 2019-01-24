using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : NPC {
    public const float OptimalTemp = 98.6f; //The optimal body temperature of a Human

    public const int CircadianSleep = 0;
    public const int CircadianWake = 1;

    public const int WorkHoursIn = 0;
    public const int WorkHoursOut = 1;

    //The various intentions a Human may have, and which direct their actions
    public const int IntentionDrink = 0;
    public const int IntentionEat = 1;
    public const int IntentionSleep = 2;
    public const int IntentionUrinate = 3;
    public const int IntentionDefecate = 4;
    public const int IntentionTemper = 5;
    public const int IntentionClean = 6;
    public const int IntentionSocialize = 7;
    public const int IntentionRelax = 8;
    public const int IntentionWork = 9;
    public const int IntentionReport = 10;
    public const int IntentionResolve = 11;

    //The various levels of sociability this Human may have, in increasingly dependent order
    public const int Dependence1 = 0;
    public const int Dependence2 = 1;
    public const int Dependence3 = 2;
    public const int Dependence4 = 3;
    public const int Dependence5 = 4;

    //The various levels of perception this Human may have, in increasingly perceptive order
    public const int Perception1 = 0;
    public const int Perception2 = 1;
    public const int Perception3 = 2;
    public const int Perception4 = 3;
    public const int Perception5 = 4;

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
    public float bladder; //Measures how much this NPC needs to urinate
    public float hunger; //Measures how much this NPC needs to feed themselves
    public float stomach; //Measures how much this NPC needs to defecate
    public float stress; //Measures how much this NPC needs to take a break or relax
    public float temperature; //Measures how hot or cold this NPC is
    public float fatigue; //Measures how tired this NPC is
    public float uncleanliness; //Measures how hygienic this NPC is
    public float sociability; //Measures how quickly this NPC becomes lonely
    public float loneliness; //Measures how in-need of companionship/conversation this NPC is
    public float perception; //Measures how quickly this NPC may become aware/unaware of the player's actions/presence
    public float awareness; //Measures how aware this NPC currently is of the player's actions/presence
    public bool well; //Whether this NPC is well or ill

    // Use this for initialization
    public new void Start () {
        //TODO: A lot of this information will be set in specific character classes. We're just setting here for testing purposes
        base.Start();
        hp = MaxValue;

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
        intention = IntentionSleep;
        thirst = 0;
        bladder = 0;
        hunger = 0;
        stomach = 0;
        stress = 0;
        temperature = OptimalTemp;
        fatigue = 0;
        uncleanliness = 0;
        sociability = Dependence2;
        loneliness = 0;
        perception = Perception4;
        awareness = 0;
        well = true;
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
        BehaviorType currentBehavior = utilityCurve.GetBehaviorType();
        //print("Current Behavior: " + currentBehavior);
        //foreach(Bucket bucket in utilityCurve.buckets)
        //{
        //    print(bucket.behaviorType + ": " + bucket.size + ", " + bucket.edge);
        //}
        if(currentBehavior == BehaviorType.Hydrate)
        {
            Drink nearestDrink = GetNearestObjectOfType(typeof(Drink)) as Drink;
            if(nearestDrink != null)
            {
                SetDestination(nearestDrink.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Satisfy)
        {
            Food nearestFood = GetNearestObjectOfType(typeof(Food)) as Food;
            if(nearestFood != null)
            {
                SetDestination(nearestFood.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Urinate)
        {
            Toilet nearestToilet = GetNearestObjectOfType(typeof(Toilet)) as Toilet;
            if(nearestToilet != null)
            {
                SetDestination(nearestToilet.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Defecate)
        {
            Toilet nearestToilet = GetNearestObjectOfType(typeof(Toilet)) as Toilet;
            if(nearestToilet != null)
            {
                SetDestination(nearestToilet.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Temper)
        {
            Thermostat nearestThermostat = GetNearestObjectOfType(typeof(Thermostat)) as Thermostat;
            if(nearestThermostat != null)
            {
                SetDestination(nearestThermostat.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Energize)
        {
            Bed nearestBed = GetNearestObjectOfType(typeof(Bed)) as Bed;
            if(nearestBed != null)
            {
                SetDestination(nearestBed.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Clean)
        {
            Shower nearestShower = GetNearestObjectOfType(typeof(Shower)) as Shower;
            if(nearestShower != null)
            {
                SetDestination(nearestShower.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Socialize)
        {
            Human nearestHuman = GetNearestObjectOfType(typeof(Human)) as Human;
            if(nearestHuman != null && nearestHuman != this)
            {
                SetDestination(nearestHuman.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Relax)
        {
            Couch nearestCouch = GetNearestObjectOfType(typeof(Couch)) as Couch;
            if(nearestCouch != null)
            {
                SetDestination(nearestCouch.transform.position);
            }
        }
        else if(currentBehavior == BehaviorType.Work)
        {
            Computer nearestComputer = GetNearestObjectOfType(typeof(Computer)) as Computer;
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
            thirst += 0.1f;
        }
        else
        {
            thirst = MaxValue;
            hp -= 0.01f;
            intention = IntentionDrink;
        }

        if(hunger < MaxValue)
        {
            hunger += 0.1f;
        }
        else
        {
            hunger = MaxValue;
            hp -= 0.01f;
            intention = IntentionEat;
        }

        if(!well)
        {
            if(thirst < MaxValue)
            {
                thirst += 0.001f;
            }
            hp -= 0.01f;
            intention = IntentionSleep;
        }

        if (temperature > OptimalTemp)
        {
            fatigue += 0.1f;
            intention = IntentionTemper;
        }
        else if(temperature < OptimalTemp)
        {
            if(Random.Range(0, MaxValue) < 1)
            {
                well = false;
            }
            intention = IntentionTemper;
        }

        if (fatigue < MaxValue)
        {
            fatigue += 0.1f;
        }
        else
        {
            fatigue = MaxValue;

            if (Random.Range(0, MaxValue) < 1)
            {
                well = false;
            }
            intention = IntentionSleep;
        }

        if(uncleanliness < MaxValue)
        {
            uncleanliness += 0.1f;
        }
        else
        {
            uncleanliness = MaxValue;

            if (Random.Range(0, MaxValue) < 1)
            {
                well = false;
            }
            intention = IntentionClean;
        }

        if(loneliness < MaxValue)
        {
            loneliness += 0.1f;
        }
        else
        {
            loneliness = MaxValue;
            intention = IntentionSocialize;
        }

        if(stress < MaxValue)
        {
            stress += 0.1f;
        }
        else
        {
            stress = MaxValue;
            intention = IntentionRelax;
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
        return base.GenerateDescription() + "\n" + "SEX: " + (sex == Male ? "M" : "F") + "\n" + "THIRST: " + FloatUtil.AsPercentString(thirst, MaxValue) + "\n" + "BLADDER: " + FloatUtil.AsPercentString(bladder, MaxValue) + "\n" + "HUNGER: " + FloatUtil.AsPercentString(hunger, MaxValue) + "\n" + "STOMACH: " + FloatUtil.AsPercentString(stomach, MaxValue) + "\n" + "STRESS: " + FloatUtil.AsPercentString(stress, MaxValue) + "\n" + "TEMPERATURE: " + FloatUtil.AsDegreeString(temperature) + "\n" + "FATIGUE: " + FloatUtil.AsPercentString(fatigue, MaxValue) + "\n" + "WELLNESS: " + (well ? "Healthy" : "Ill");
    }
}
