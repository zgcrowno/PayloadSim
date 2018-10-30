using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour {

    public World world; //The overarching World of which everything else is a child
    public Rect wholeRect; //The Rect making up the entirety of this Tab
    public Rect prevWhole; //The Rect containing the coordinates and dimensions previously held by this Tab
    public Rect headerRect; //The Rect making up this Tab's header
    public Rect bodyRect; //The Rect making up this Tab's body

    public bool beingDragged; //The bool representing whether or not this Tab is currently being dragged with the mouse by the player
    public int depth; //Integer representing the order in which different Tabs will be drawn, thus allowing for overlay

    // Use this for initialization
    public void Start () {
        world = GameObject.Find("/World").GetComponent<World>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Method which sets up the wholeRect's values, and afterwards, the values of the header, body and prevWhole rects which are dependent on them (and the quadrants for SubTabs)
    public void SetUpWholeRect(float x, float y, float width, float height)
    {
        wholeRect.x = x;
        wholeRect.y = y;
        wholeRect.width = width;
        wholeRect.height = height;

        prevWhole.x = x;
        prevWhole.y = y;
        prevWhole.width = width;
        prevWhole.height = height;

        if(GetType() == typeof(SuperTab))
        {
            SuperTab superTab = (SuperTab) this;
            
            headerRect.width = Screen.width / 8;
            headerRect.height = Screen.height / 20;
            headerRect.x = wholeRect.x + (superTab.placement * headerRect.width);
            headerRect.y = wholeRect.y;

            bodyRect.x = wholeRect.x;
            bodyRect.y = wholeRect.y + headerRect.height;
            bodyRect.width = wholeRect.width;
            bodyRect.height = wholeRect.height - headerRect.height;
        }
        else //GetType() == typeof(SubTab)
        {
            SubTab subTab = (SubTab) this;
            
            headerRect.x = wholeRect.x + (wholeRect.width / 20);
            headerRect.y = wholeRect.y;
            headerRect.width = Screen.width / 8;
            headerRect.height = Screen.height / 20;

            bodyRect.x = wholeRect.x;
            bodyRect.y = wholeRect.y + (headerRect.height / 2);
            bodyRect.width = wholeRect.width;
            bodyRect.height = wholeRect.height - (headerRect.height / 2);

            subTab.quadrants[0].x = wholeRect.x;
            subTab.quadrants[0].y = wholeRect.y;
            subTab.quadrants[0].width = wholeRect.width / 3;
            subTab.quadrants[0].height = wholeRect.height;
            subTab.quadrants[1].x = wholeRect.x + wholeRect.width - (wholeRect.width / 3);
            subTab.quadrants[1].y = wholeRect.y;
            subTab.quadrants[1].width = wholeRect.width / 3;
            subTab.quadrants[1].height = wholeRect.height;
            subTab.quadrants[2].x = wholeRect.x + (wholeRect.width / 3);
            subTab.quadrants[2].y = wholeRect.y;
            subTab.quadrants[2].width = wholeRect.width / 3;
            subTab.quadrants[2].height = wholeRect.height / 2;
            subTab.quadrants[3].x = wholeRect.x + (wholeRect.width / 3);
            subTab.quadrants[3].y = wholeRect.y + (wholeRect.height / 2);
            subTab.quadrants[3].width = wholeRect.width / 3;
            subTab.quadrants[3].height = wholeRect.height / 2;
        }
    }
}
