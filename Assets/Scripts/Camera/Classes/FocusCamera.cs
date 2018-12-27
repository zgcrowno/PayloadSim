using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : PayloadCamera {

    public const int rotationTimerLimit = 10;

    public int rotationTimer = 0;

    // Use this for initialization
    new public void Start () {
        base.Start();
        content = GameObject.Find("/Level/Rooms/Kitchen/Fridge");
        contentRenderers = content.GetComponentsInChildren<Renderer>();
        CenterContent();
    }
	
	// Update is called once per frame
	public void FixedUpdate () {
        if(content != null)
        {
            if(rotationTimer < rotationTimerLimit)
            {
                rotationTimer++;
            }
            else
            {
                rotationTimer = 0;
            }
            if(rotationTimer == rotationTimerLimit)
            {
                transform.RotateAround(contentBounds.center, Vector3.up, 11.25f);
            }
        }
    }
}
