﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PayloadCamera : MonoBehaviour {

    public GameObject content; //The object on which this PayloadCamera is focused
    public Camera cam; //The Camera component of this PayloadCamera
    public Renderer[] contentRenderers; //The Renderer components contained within content
    public Bounds contentBounds; //The Bounds object in which is contained all of content's Bounds

    // Use this for initialization
    public void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
        
	}

    /*
     * This method ensures that the content on which this PayloadCamera is focused fits perfectly within the viewport
     */
    public void CenterContent()
    {
        //Construct a single Bounds object from all the Bounds in content
        foreach (Renderer renderer in contentRenderers)
        {
            if(contentBounds.size == Vector3.zero)
            {
                //contentBounds hasn't been initialized, so initialize it here
                contentBounds = renderer.bounds;
            } else
            {
                //Grow contentBounds to encompass all of content's renderers' Bounds
                contentBounds.Encapsulate(renderer.bounds);
            }
        }

        float cameraDistance = 1.75f; // Constant factor
        Vector3 objectSizes = contentBounds.max - contentBounds.min;
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * cam.fieldOfView); // Visible height 1 meter in front
        float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
        distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
        transform.position = contentBounds.center - distance * cam.transform.forward;
    }

    /*
     * Shifts this PayloadCamera's focus from one GameObject to another, resetting both GameObject's layers such that only the currently focused 
     * object is visible, and centering the new content within this PayloadCamera's viewport
     * @param newContent The new GameObject to receive this PayloadCamera's focus
     */ 
    public void SwitchContent(GameObject newContent)
    {
        content.layer = LayerMask.NameToLayer("Default");
        content = newContent;
        content.layer = LayerMask.NameToLayer("Focus");
        CenterContent();
    }
}
