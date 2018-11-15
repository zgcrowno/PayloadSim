using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public PlayerInterface pi;
    
	void Start () {
        //Initialize the PlayerInterface
        pi = gameObject.AddComponent<PlayerInterface>();
	}
	
	void Update () {
		
	}
}
