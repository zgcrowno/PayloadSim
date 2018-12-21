using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PayloadCamera : MonoBehaviour {

    public GameObject content; //The object on which this PayloadCamera is focused
    public Camera cam; //The Camera component of this PayloadCamera

	// Use this for initialization
	public void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
        
	}

    public abstract void PositionRelativeToContent();
}
