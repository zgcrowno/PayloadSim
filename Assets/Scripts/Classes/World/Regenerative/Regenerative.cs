using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerative : MonoBehaviour {

    public const int Energy = 0;
    public const int Relaxation = 1;
    public const int Hygiene = 2;

    public List<int> types; //Variable which determines what all this Regenerative improves

    public float regenRate; //The rate at which this Regenerative improves its interactor's energy, relaxation and/or hygiene

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
