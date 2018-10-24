using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour {

    public GameObject header;

    public TabHeader headerScript;
    public Camera mainCam;
    public Vector2 position;
    public LineRenderer border;

    public float width;
    public float height;

	// Use this for initialization
	void Start () {
        //Initialize the Tab object's corresponding TabHeader object
        header = new GameObject();
        headerScript = header.AddComponent<TabHeader>();

        //All Tab objects will need to make use of the main camera
        mainCam = Camera.main;

        //Vector2 representing the center of the Tab object
        position = new Vector2(0.5f, 0.5f);

        //Add and setup the LineRenderer component by which the Tab object's border is rendered
        border = gameObject.AddComponent<LineRenderer>();
        border.positionCount = 4; //Four vertices
        AnimationCurve borderWidth = new AnimationCurve();
        borderWidth.AddKey(0, 0.02f);
        borderWidth.AddKey(1, 0.02f);
        border.widthCurve = borderWidth;
        border.startColor = Color.magenta;
        border.endColor = Color.magenta;
        border.loop = true;

        //Initialize the Tab object's width and height in viewport-relative values (0 is no width/height, and 1 is viewport-sized width/height)
        width = 1;
        height = 1;
	}
	
	// Update is called once per frame
	void Update () {
        //Update the Tab object's corresponding TabHeader object
        headerScript.width = width;
        headerScript.height = 0.02f;
        headerScript.background.startWidth = height;
        headerScript.background.endWidth = height;
        headerScript.position = new Vector2(position.x, position.y + (height / 2));

        //Must set z-axis value to a positive number so that the lines are visible within the camera's frustum
        border.SetPosition(0, mainCam.ViewportToWorldPoint(new Vector3(position.x - (width / 2), position.y - (height / 2), 1)));
        border.SetPosition(1, mainCam.ViewportToWorldPoint(new Vector3(position.x - (width / 2), position.y + (height / 2), 1)));
        border.SetPosition(2, mainCam.ViewportToWorldPoint(new Vector3(position.x + (width / 2), position.y + (height / 2), 1)));
        border.SetPosition(3, mainCam.ViewportToWorldPoint(new Vector3(position.x + (width / 2), position.y - (height / 2), 1)));
        headerScript.background.SetPosition(0, mainCam.ViewportToWorldPoint(new Vector3(headerScript.position.x - (headerScript.width / 2), headerScript.position.y, 1)));
        headerScript.background.SetPosition(1, mainCam.ViewportToWorldPoint(new Vector3(headerScript.position.x + (headerScript.width / 2), headerScript.position.y, 1)));
    }
}
