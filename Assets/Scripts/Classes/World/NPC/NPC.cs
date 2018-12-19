using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Class which denotes an entity with a level of agency, regardless of whether it's organic (i.e. human) or not (i.e. robotic)
 */ 
public class NPC : MonoBehaviour {

    public NavMeshAgent agent;

	// Use this for initialization
	public void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
