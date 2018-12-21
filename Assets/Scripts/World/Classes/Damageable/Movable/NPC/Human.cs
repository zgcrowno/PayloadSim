using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : NPC {

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

    public Dictionary<int, float> circadian; //Contains the time(s) at which this NPC typically goes to sleep and wakes up
    public Dictionary<int, float> workHours; //Contains the time(s) at which this NPC typically clocks in and clocks out of work
    public List<int> workDays; //Contains the days this NPC typically works
    public List<int> perks; //Contains the perks which apply to this NPC

    public string firstName; //The NPC's first name
    public string lastName; //The NPC's last name
    public int sex; //The NPC's sex
    public int attractedTo; //The sex to which this NPC is attracted
    public float hydration; //Measures how much this NPC needs to hydrate themselves
    public float bladder; //Measures how much this NPC needs to urinate
    public float satisfaction; //Measures how much this NPC needs to feed themselves
    public float stomach; //Measures how much this NPC needs to defecate
    public float relaxation; //Measures how much this NPC needs to take a break or relax
    public float temperature; //Measures how hot or cold this NPC is
    public float energy; //Measures how tired this NPC is
    public float hygiene; //Measures how hygienic this NPC is
    public float sociability; //Measures how quickly this NPC becomes lonely
    public float loneliness; //Measures how in-need of companionship/conversation this NPC is
    public float perception; //Measures how quickly this NPC may become aware/unaware of the player's actions/presence
    public float awareness; //Measures how aware this NPC currently is of the player's actions/presence
    public bool well; //Whether this NPC is well or ill

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * Makes this Human consume the passed consumable
     * @param con The passed Consumable
     */
    public void Consume(Consumable con)
    {

    }
}
