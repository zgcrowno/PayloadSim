using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour {

    public World world; //The overarching GameObject of which all others are children

    public GameObject parentTabObject; //If null, this tab's parent is the screen, and if not null, this tab is nested in another
    public Tab parentTab; //The Tab script associated with the parentTabObject

    public Rect headerRect;
    public Rect bodyRect;

    public Rect[] nestedTabs = new Rect[8];

    public Stack<Vector2> prevPos = new Stack<Vector2>();

    public int depth; //Integer representing the order in which different Tabs will be drawn, thus allowing for overlay
    public int numNestedTabs; //Integer representing how many nested tabs are contained within this overarching parent tab

    public float octantWidth;
    public float octantHeight;

    public float[][] nestedTabOctants = new float[8][];
    
    public bool beingDragged; //Represents whether or not this tab is currently being dragged by the player

	// Use this for initialization
	void Start () {
        world = GameObject.Find("/World").GetComponent<World>();
        parentTab = null;
        numNestedTabs = 1;
        //headerRect = new Rect(0, 0, Screen.width / 6, Screen.height / 20);
        //bodyRect = new Rect(0, Screen.height / 20, Screen.width, Screen.height - (Screen.height / 20));

        octantWidth = bodyRect.width / 4;
        octantHeight = (bodyRect.height / 2);

        //Initialize nestedTabs
        for (int i = 0; i < nestedTabs.Length; i++)
        {
            float x;
            float y;

            if (i < 4)
            {
                x = i * octantWidth;
                y = bodyRect.y;
            }
            else
            {
                x = (i - 4) * octantWidth;
                y = bodyRect.y + octantHeight;
            }
            nestedTabs[i] = new Rect(x, y, octantWidth, octantHeight);
        }
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void OnGUI()
    {
        //Set the depth of this Tab in order to achieve the correct drawing order
        GUI.depth = depth;

        //Draw the tab's header
        GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, true, 0, Color.white, 0, 5);

        //Draw the tab's body
        GUI.DrawTexture(bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, true, 0, Color.white, 1, 0);

        //Draw the nestedTabs for debugging purposes
        if (depth == 0)
        {
            foreach (Rect rect in nestedTabs)
            {
                GUI.DrawTexture(rect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, true, 0);
                GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, true, 0, Color.white, 1, 0);
            }
        }

        //headerRect interactivity
        if (headerRect.Contains(Event.current.mousePosition)) //The mouse cursor is within the bounds of headerRect
        {
            if (Event.current.type == EventType.MouseDown)
            {
                world.SetTabDepths(this);
                beingDragged = true;
            }
        }
        if (beingDragged && Event.current.type == EventType.MouseUp)
        {
            beingDragged = false;

            Place();
        }
        if (depth == 0 && beingDragged && Event.current.type == EventType.MouseDrag)
        {
            headerRect.x = Event.current.mousePosition.x - (headerRect.width / 2);
            headerRect.y = Event.current.mousePosition.y - (headerRect.height / 2);
        }
    }

    //Method that returns whether or not the tab may be placed where it's currently dragged by the player
    public bool IsPlaceable()
    {
        Tab tabToBecomeParent = world.GetTabByDepth(depth + 1);
        
        if(tabToBecomeParent.numNestedTabs < 8)
        {
            if (tabToBecomeParent.bodyRect.Contains(Event.current.mousePosition))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //Method by which a tab is placed in a new location or its previously held one
    public void Place()
    {
        //Pop the previous positions from the stack
        Vector2 prevPosBody = prevPos.Pop();
        Vector2 prevPosHeader = prevPos.Pop();

        if (IsPlaceable())
        {
            Tab tabToBecomeParent = world.GetTabByDepth(depth + 1);

            if (tabToBecomeParent.numNestedTabs == 1)
            {
                if(tabToBecomeParent.nestedTabs[0].Contains(Event.current.mousePosition)
                   || tabToBecomeParent.nestedTabs[4].Contains(Event.current.mousePosition))
                {
                    //Tab will be placed on left side of two
                    headerRect.x = 0;
                    headerRect.y = headerRect.height;
                    bodyRect.x = 0;
                    bodyRect.y = headerRect.y + headerRect.height;
                    bodyRect.width = Screen.width / 2;
                    bodyRect.height = Screen.height - (2 * headerRect.height);
                }
                else if(tabToBecomeParent.nestedTabs[3].Contains(Event.current.mousePosition)
                        || tabToBecomeParent.nestedTabs[7].Contains(Event.current.mousePosition))
                {
                    //Tab will be placed on right side of two
                    headerRect.x = Screen.width / 2;
                    headerRect.y = headerRect.height;
                    bodyRect.x = Screen.width / 2;
                    bodyRect.y = headerRect.y + headerRect.height;
                    bodyRect.width = Screen.width / 2;
                    bodyRect.height = Screen.height - (2 * headerRect.height);
                }
                else if(tabToBecomeParent.nestedTabs[1].Contains(Event.current.mousePosition)
                        || tabToBecomeParent.nestedTabs[2].Contains(Event.current.mousePosition))
                {
                    //Tab will be placed on upper side of two
                    headerRect.x = 0;
                    headerRect.y = headerRect.height;
                    bodyRect.x = 0;
                    bodyRect.y = headerRect.y + headerRect.height;
                    bodyRect.width = Screen.width;
                    bodyRect.height = (Screen.height / 2) - (2 * headerRect.height);
                }
                else
                {
                    //Tab will be placed on lower side of two
                    headerRect.x = 0;
                    headerRect.y = (Screen.height / 2) + headerRect.height;
                    bodyRect.x = 0;
                    bodyRect.y = (Screen.height / 2) + (2 * headerRect.height);
                    bodyRect.width = Screen.width;
                    bodyRect.height = (Screen.height / 2) - (2 * headerRect.height);
                }
            }
            else if(tabToBecomeParent.numNestedTabs == 2)
            {
                if (tabToBecomeParent.nestedTabs[0].Contains(Event.current.mousePosition)
                   || tabToBecomeParent.nestedTabs[4].Contains(Event.current.mousePosition))
                {

                }
            }
        }
        else
        {
            //Snap to previous position
            headerRect.x = prevPosHeader.x;
            headerRect.y = prevPosHeader.y;
            bodyRect.x = prevPosBody.x;
            bodyRect.y = prevPosBody.y;
        }

        //Push the new previous positions onto the stack
        prevPos.Push(new Vector2(headerRect.x, headerRect.y));
        prevPos.Push(new Vector2(bodyRect.x, bodyRect.y));

        //Reset the rect array to conform with new header and body positions/dimensions
        ResetRectArray();
    }

    public void ResetRectArray()
    {
        octantWidth = bodyRect.width / 4;
        octantHeight = (bodyRect.height / 2);

        for (int i = 0; i < nestedTabs.Length; i++)
        {
            float x;
            float y;

            if (i < 4)
            {
                x = bodyRect.x + (i * octantWidth);
                y = bodyRect.y;
            }
            else
            {
                x = bodyRect.x + ((i - 4) * octantWidth);
                y = bodyRect.y + octantHeight;
            }
            nestedTabs[i] = new Rect(x, y, octantWidth, octantHeight);
        }
    }
}
