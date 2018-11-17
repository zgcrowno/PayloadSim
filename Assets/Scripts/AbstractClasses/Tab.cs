using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tab : MonoBehaviour {

    public PlayerInterface pi; //The overarching PlayerInterface of which all Tabs are children
    public Rect wholeRect; //The Rect making up the entirety of this Tab
    public Rect prevWhole; //The Rect containing the coordinates and dimensions previously held by this Tab
    public Rect headerRect; //The Rect making up this Tab's header
    public Rect bodyRect; //The Rect making up this Tab's body

    public bool beingDragged; //The bool representing whether or not this Tab is currently being dragged with the mouse by the player
    public int depth; //Integer representing the order in which different Tabs will be drawn, thus allowing for overlay
    public string headerText; //The text to be displayed in this Tab's header

    public void Awake()
    {
        pi = GameObject.Find("/World").GetComponent<PlayerInterface>();
    }
    
    public void Start () {
        
    }
	
	public void Update () {
		
	}

    public void OnGUI()
    {
        Draw();

        MouseInput();
    }

    /*
     * Method by which a Tab's wholeRect is assigned those values held by its prevWhole datum
     */
    public void SnapToPreviousPosition()
    {
        SetUpWholeRect(prevWhole.x, prevWhole.y, prevWhole.width, prevWhole.height);
    }

    /*
     * Method by which mouse input is read, and associated behaviors executed accordingly
     */
    public void MouseInput()
    {
        MouseDown();
        MouseDrag();
        MouseUp();
    }

    /*
     * Method by which all of pi.superTabs is iterated through in order for every superTab to fill any dead space within its body
     */
    public void FillDeadSpace()
    {
        foreach (SuperTab superTab in pi.superTabs)
        {
            superTab.FillDeadSpace();
        }
    }
    
    public abstract void SetUpWholeRect(float x, float y, float width, float height);

    public abstract void Draw();

    public abstract void Place();

    public abstract void MouseDown();

    public abstract void MouseUp();

    public abstract void MouseDrag();
}
