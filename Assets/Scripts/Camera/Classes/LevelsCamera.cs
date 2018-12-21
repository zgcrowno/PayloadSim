using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsCamera : PayloadCamera {

    public Renderer[] contentRenderers; //The Renderer components contained within content

    // Use this for initialization
    new public void Start () {
        base.Start();
        content = GameObject.Find("/Level");
        contentRenderers = content.GetComponentsInChildren<Renderer>();
        PositionRelativeToContent();
    }
	
	// Update is called once per frame
	new public void FixedUpdate () {
        
	}

    /*
     * This method ensures that the content on which this PayloadCamera is focused fits perfectly within the viewport
     */
    public override void PositionRelativeToContent()
    {
        float cameraDistance = 1.25f; // Constant factor
        Bounds bounds = new Bounds();

        //Construct a single Bounds object from all the Bounds in Level
        foreach (Renderer renderer in contentRenderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        Vector3 objectSizes = bounds.max - bounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * cam.fieldOfView); // Visible height 1 meter in front
        float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
        distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
        transform.position = bounds.center - distance * cam.transform.forward;
    }
}
