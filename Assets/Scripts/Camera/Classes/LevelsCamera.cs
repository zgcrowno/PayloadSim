using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsCamera : PayloadCamera {

    // Use this for initialization
    new public void Start () {
        base.Start();
        content = GameObject.Find("/Level");
        contentRenderers = content.GetComponentsInChildren<Renderer>();
        CenterContent();
    }
	
	// Update is called once per frame
	new public void FixedUpdate () {
        
	}
}
