using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class represents any number of items which may be consumed by NPCs in the game world for some benefit and/or detriment. Such items may be
 * of the food, drink and drug varieties.
 */ 
public class Consumable : MonoBehaviour {

    public const int Food = 0;
    public const int Drink = 1;
    public const int Drug = 2;

    public float quality; //The quality of this Consumable (regularly decreases if the item is perishable)
    public float hydration; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's hydration
    public float satisfaction; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's appetite
    public float relaxation; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's relaxation
    public float warmth; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's temperature
    public float stimulation; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's energy
    public float socialization; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's sociability
    public float focus; //The amount (in trivial units) by which this Consumable increases or decreases its consumer's perception
    public float bladderInc; //The amount (in trivial units) by which this Consumable adds to its consumer's bladder capacity
    public float stomachInc; //The amount (in trivial units) by which this Consumable adds to its consumer's stomach capacity
    public float sugar; //The amount (in trivial units) of sugar this consumable contains
    public float sodium; //The amount (in trivial units) of sodium this consumable contains (generally associated with salt)
    public bool diuretic; //Whether or not this Consumable causes increased passing of urine
    public bool laxative; //Whether or not this Consumable causes increased passing of feces
    public bool perishable; //Whether or not this Consumable is perishable (and should thus be stored in a fridge)

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
