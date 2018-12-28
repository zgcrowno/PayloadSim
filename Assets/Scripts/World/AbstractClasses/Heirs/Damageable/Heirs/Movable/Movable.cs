﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Movable : Damageable {

    public NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * Moves this object as close as possible to the passed Transform
     * @param trans The passed Transform towards which this object is moving
     */ 
    public void MoveTo(Transform trans)
    {

    }
}