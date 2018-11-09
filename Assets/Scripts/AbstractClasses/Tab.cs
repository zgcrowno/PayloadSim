using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tab : MonoBehaviour {

    public PlayerInterface pi; //The overarching PlayerInterface of which all Tabs are children
    public Rect wholeRect; //The Rect making up the entirety of this Tab
    public Rect prevWhole; //The Rect containing the coordinates and dimensions previously held by this Tab
    public Rect headerRect; //The Rect making up this Tab's header
    public Rect bodyRect; //The Rect making up this Tab's body
    public GUIStyle headerStyle; //The GUIStyle object by which the appearance of the headerText will be determined

    public bool beingDragged; //The bool representing whether or not this Tab is currently being dragged with the mouse by the player
    public int depth; //Integer representing the order in which different Tabs will be drawn, thus allowing for overlay
    public string headerText; //The text to be displayed in this Tab's header

    // Use this for initialization
    public void Start () {
        pi = GameObject.Find("/World").GetComponent<PlayerInterface>();

        headerStyle = new GUIStyle();
        headerStyle.fontSize = 25;
        headerStyle.font = (Font)Resources.Load("Fonts/FontCommodoreAngled");
        headerStyle.alignment = TextAnchor.MiddleLeft;
    }
	
	// Update is called once per frame
	public void Update () {
		
	}

    public void OnGUI()
    {
        Draw();

        MouseInput();
    }

    public void SnapToPreviousPosition()
    {
        SetUpWholeRect(prevWhole.x, prevWhole.y, prevWhole.width, prevWhole.height);
    }

    public void MouseInput()
    {
        MouseDown();
        MouseDrag();
        MouseUp();
    }

    public void FillDeadSpace()
    {
        foreach (SuperTab superTab in pi.superTabs)
        {
            superTab.FillDeadSpace();
        }
    }

    //Method which sets up the wholeRect's values, and afterwards, the values of the header, body and prevWhole rects which are dependent on them (and the quadrants and cursorChangeRects for SubTabs)
    public abstract void SetUpWholeRect(float x, float y, float width, float height);

    public abstract void Draw();

    public abstract void Place();

    public abstract void MouseDown();

    public abstract void MouseUp();

    public abstract void MouseDrag();
}
