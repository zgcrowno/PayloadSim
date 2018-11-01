using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public PlayerInterface pi;

	// Use this for initialization
	void Start () {
        //Initialize the PlayerInterface
        pi = gameObject.AddComponent<PlayerInterface>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
