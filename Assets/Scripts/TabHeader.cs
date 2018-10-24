using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabHeader : MonoBehaviour {
    
    public Vector2 position;
    public LineRenderer background;

    public float width;
    public float height;

    // Use this for initialization
    void Start () {
        //Add and setup the LineRenderer component by which the Tab object's border is rendered
        background = gameObject.AddComponent<LineRenderer>();
        background.positionCount = 2; //Two vertices
        background.startWidth = 0.05f;
        background.endWidth = 0.05f;
        background.startColor = Color.magenta;
        background.endColor = Color.magenta;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
