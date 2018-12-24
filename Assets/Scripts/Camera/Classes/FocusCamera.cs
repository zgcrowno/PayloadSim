using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : PayloadCamera {

    // Use this for initialization
    new public void Start () {
        base.Start();
        content = GameObject.Find("/Level/Rooms/Kitchen/Fridge");
        contentRenderers = content.GetComponentsInChildren<Renderer>();
        CenterContent();
    }
	
	// Update is called once per frame
	new public void FixedUpdate () {
        transform.RotateAround(contentBounds.center, Vector3.up, 0.5f);
    }
}
