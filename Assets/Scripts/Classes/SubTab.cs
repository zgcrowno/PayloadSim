using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTab : Tab {

    public const int Lateral = 0; //Represents the left or right sides of the SubTab
    public const int Vertical = 1; //Represents the upper middle or lower middle sides of the SubTab
    public const int Left = 0; //Represents the left side of the SubTab
    public const int Right = 1; //Represents the right side of the SubTab
    public const int UpperMiddle = 2; //Represents the upper middle side of the SubTab
    public const int LowerMiddle = 3; //Represents the lower middle side of the SubTab
    
    public SuperTab superTab; //The SuperTab of which this SubTab is a child
    public Rect[] quadrants; //The left, right, middle-top and middle-bottom quadrants by which is determined the placement of subsequent SubTabs within the overarching SuperTab
    
    public float minWidth; //The minimum allowable width of the SubTab
    public float minHeight; //The minimum allowable height of the SubTab
    public float maxWidth; //The maximum allowable width of the SubTab
    public float maxHeight; //The maximum allowable height of the SubTab

    // Use this for initialization
    new public void Start () {
        base.Start();
        SetUpWholeRect(superTab.wholeRect.x, superTab.wholeRect.y + superTab.headerRect.height, superTab.wholeRect.width, superTab.wholeRect.height - superTab.headerRect.height);
        minWidth = Screen.width / 4;
        minHeight = (Screen.height - (Screen.height / 20)) / 4;
        maxWidth = Screen.width;
        maxHeight = Screen.height - (Screen.height / 20);
    }
	
	// Update is called once per frame
	public void Update () {
		
	}

    //Method which returns whether or not this SubTab has space for another SubTab on a given side
    public bool HasSpace(int side)
    {
        if(side == Lateral)
        {
            float widthAfterPlacement = wholeRect.width / 2;

            if (widthAfterPlacement < minWidth)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else //side == Vertical
        {
            float heightAfterPlacement = wholeRect.height / 2;

            if (heightAfterPlacement < minHeight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    //Method which returns the side of the SubTab's wholeRect on which the mouse cursor currently resides
    public int SideOfCursor()
    {
        if(quadrants[0].Contains(Event.current.mousePosition))
        {
            return Left;
        }
        else if(quadrants[1].Contains(Event.current.mousePosition))
        {
            return Right;
        }
        else if(quadrants[2].Contains(Event.current.mousePosition))
        {
            return UpperMiddle;
        }
        else //quadrants[3].Contains(Event.current.mousePosition)
        {
            return LowerMiddle;
        }
    }

    public override void SetUpWholeRect(float x, float y, float width, float height)
    {
        wholeRect.x = x;
        wholeRect.y = y;
        wholeRect.width = width;
        wholeRect.height = height;

        prevWhole.x = x;
        prevWhole.y = y;
        prevWhole.width = width;
        prevWhole.height = height;

        headerRect.x = wholeRect.x + (wholeRect.width / 20);
        headerRect.y = wholeRect.y;
        headerRect.width = Screen.width / 8;
        headerRect.height = Screen.height / 20;

        bodyRect.x = wholeRect.x;
        bodyRect.y = wholeRect.y + (headerRect.height / 2);
        bodyRect.width = wholeRect.width;
        bodyRect.height = wholeRect.height - (headerRect.height / 2);

        quadrants[0].x = wholeRect.x;
        quadrants[0].y = wholeRect.y;
        quadrants[0].width = wholeRect.width / 3;
        quadrants[0].height = wholeRect.height;
        quadrants[1].x = wholeRect.x + wholeRect.width - (wholeRect.width / 3);
        quadrants[1].y = wholeRect.y;
        quadrants[1].width = wholeRect.width / 3;
        quadrants[1].height = wholeRect.height;
        quadrants[2].x = wholeRect.x + (wholeRect.width / 3);
        quadrants[2].y = wholeRect.y;
        quadrants[2].width = wholeRect.width / 3;
        quadrants[2].height = wholeRect.height / 2;
        quadrants[3].x = wholeRect.x + (wholeRect.width / 3);
        quadrants[3].y = wholeRect.y + (wholeRect.height / 2);
        quadrants[3].width = wholeRect.width / 3;
        quadrants[3].height = wholeRect.height / 2;
    }

    public override void Draw()
    {
        GUI.depth = depth;

        if (!superTab.beingDragged)
        {
            GUI.DrawTexture(bodyRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
            GUI.DrawTexture(bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);
            GUI.DrawTexture(headerRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
            GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);

            headerStyle.normal.textColor = Color.white;
            GUI.Label(headerRect, headerText, headerStyle);
        }
    }

    public override void Drag()
    {
        if (headerRect.Contains(Event.current.mousePosition)) //The mouse cursor is within the bounds of headerRect
        {
            if (Event.current.type == EventType.MouseDown)
            {
                superTab.SetSubTabToDepth(this, 0);
                beingDragged = true;
            }
        }
        if (beingDragged && Event.current.type == EventType.MouseUp)
        {
            beingDragged = false;

            Place();
        }
        if (this == superTab.GetSubTabByDepth(0) && beingDragged && Event.current.type == EventType.MouseDrag)
        {
            headerRect.x = Event.current.mousePosition.x - (headerRect.width / 2);
            headerRect.y = Event.current.mousePosition.y - (headerRect.height / 2);

            foreach (SubTab subTab in superTab.subTabs)
            {
                if (subTab != this && subTab.wholeRect.Contains(Event.current.mousePosition))
                {
                    subTab.SetUpWholeRect(prevWhole.x, prevWhole.y, prevWhole.width, prevWhole.height);
                    superTab.SetSubTabToDepth(subTab, 1);
                }
            }
        }
    }

    //Method that returns whether or not the SubTab may be placed where it's currently dragged by the player
    public override bool IsPlaceable()
    {

        if(superTab.bodyRect.Contains(Event.current.mousePosition) || pi.superTabs.Count < 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Method by which a tab is placed in a new location or its previously held one
    public override void Place()
    {
        if(IsPlaceable())
        {
            if (superTab.bodyRect.Contains(Event.current.mousePosition))
            {
                //Swapping this SubTab with another
                foreach (SubTab subTab in superTab.subTabs)
                {
                    if (subTab != this)
                    {
                        if (subTab.wholeRect.Contains(Event.current.mousePosition))
                        {
                            subTab.SetUpWholeRect(prevWhole.x,
                                prevWhole.y,
                                prevWhole.width,
                                prevWhole.height);
                            SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width,
                                    subTab.wholeRect.height);
                        }
                    }
                }
            }
            else
            {
                SuperTab superTabToBecomeParent = new SuperTab();
                pi.superTabs.Add(superTabToBecomeParent);
                superTab.subTabs.Remove(this);
                if (superTab.subTabs.Count < 1)
                {
                    Destroy(superTab.gameObject);
                }
                superTab = superTabToBecomeParent;
                pi.OrganizeSuperTabHeaders();
            }
        }
        else
        {
            SnapToPreviousPosition();
        }
    }
}
