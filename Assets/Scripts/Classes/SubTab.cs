using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTab : Tab {

    public const int Lateral = 0; //Represents the left or right sides of the SubTab
    public const int Vertical = 1; //Represents the upper middle or lower middle sides of the SubTab
    public const int Left = 0; //Represents the left side of the SubTab
    public const int Right = 1; //Represents the right side of the SubTab
    public const int Upper = 2; //Represents the upper middle side of the SubTab
    public const int Lower = 3; //Represents the lower middle side of the SubTab
    
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
	new public void Update () {
        //TODO: Move this out of Update method, and call only when required
        FillDeadSpace();
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
            return Upper;
        }
        else //quadrants[3].Contains(Event.current.mousePosition)
        {
            return Lower;
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
        headerRect.width = Screen.width / PlayerInterface.MaxSuperTabs;
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
            if(!beingDragged)
            {
                GUI.DrawTexture(bodyRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
                GUI.DrawTexture(bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);
            }
            GUI.DrawTexture(headerRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
            GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);

            headerStyle.normal.textColor = Color.white;
            GUI.Label(headerRect, headerText, headerStyle);
        }
    }

    public override void Drag()
    {
        if (superTab == pi.GetSuperTabByDepth(0) && headerRect.Contains(Event.current.mousePosition)) //The mouse cursor is within the bounds of headerRect
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
                    Rect prevSubTabWhole = subTab.prevWhole;
                    subTab.SetUpWholeRect(prevWhole.x, prevWhole.y, prevWhole.width, prevWhole.height);
                    SetUpWholeRect(prevSubTabWhole.x, prevSubTabWhole.y, prevSubTabWhole.width, prevSubTabWhole.height);
                    superTab.SetSubTabToDepth(subTab, 1);
                }
            }
        }
    }

    //Method by which a tab is placed in a new location or its previously held one
    public override void Place()
    {
        //This SubTab is onle placeable if it's being moved within its current superTab or it's being added to the PlayerInterface's superTabs list, and that list is not full
        if (superTab.bodyRect.Contains(Event.current.mousePosition))
        {
            //Setting to previous position here since the previous position's value is changed in the Drag() method
            SnapToPreviousPosition();
            pi.SetSuperTabToDepth(superTab, 0);
        }
        else if(pi.superTabs.Count < PlayerInterface.MaxSuperTabs)
        {
            //Adding as new SuperTab since the mouse cursor isn't contained within superTab's bodyRect
            AddAsSuperTab(new GameObject().AddComponent<SuperTab>());
        }
        else
        {
            //If not placeable, the SubTab is simply snapped back to its previous position
            SnapToPreviousPosition();
        }
    }

    public void AddAsSuperTab(SuperTab superTabToBecomeParent)
    {
        superTab.subTabs.Remove(this);
        if(superTab.subTabs.Count < 1)
        {
            Destroy(superTab.gameObject);
        }

        superTabToBecomeParent.subTabs = new List<SubTab>();
        superTabToBecomeParent.headerText = "New Text";
        pi.superTabs.Add(superTabToBecomeParent);

        superTab = superTabToBecomeParent;
        superTab.subTabs.Add(this);

        SetUpWholeRect(0, headerRect.height, Screen.width, Screen.height - headerRect.height);

        pi.OrganizeSuperTabHeaders();
        pi.SetSuperTabToDepth(superTab, 0);
    }

    //Returns whether or not this SubTab has dead/unoccupied space adjacent to it (meaning the superTab has dead space which must be filled)
    public bool HasDeadSpaceToSide(int side)
    {
        if(HasSideAdjacentSubTab(side))
        {
            return false;
        }
        else
        {
            switch(side)
            {
                case Left:
                    return !(wholeRect.x == 0);
                    break;
                case Right:
                    return !(wholeRect.x + wholeRect.width == Screen.width);
                    break;
                case Upper:
                    return !(wholeRect.y == headerRect.height);
                    break;
                case Lower:
                    return !(wholeRect.y + wholeRect.height == Screen.height);
                    break;
            }

            return false;
        }
    }

    //Returns whether or not this SubTab has dead/unoccupied space adjacent to any of its sides
    public bool HasDeadSpaceOnAnySide()
    {
        return HasDeadSpaceToSide(Left) || HasDeadSpaceToSide(Right) || HasDeadSpaceToSide(Upper) || HasDeadSpaceToSide(Lower);
    }

    //Method by which a SubTab alters its own wholeRect to fill up any dead/unoccupied space which is adjacent to it
    public void FillDeadSpace()
    {
        if(HasDeadSpaceToSide(Left))
        {
            SubTab subTabToLeft = GetSubTabToSide(Left);

            if(subTabToLeft != null)
            {
                SetUpWholeRect(subTabToLeft.wholeRect.x + subTabToLeft.wholeRect.width,
                               wholeRect.y,
                               wholeRect.width + (wholeRect.x - (subTabToLeft.wholeRect.x + subTabToLeft.wholeRect.width)),
                               wholeRect.height);
            }
            else
            {
                SetUpWholeRect(0,
                               wholeRect.y,
                               wholeRect.x + wholeRect.width,
                               wholeRect.height);
            }
        }
        if(HasDeadSpaceToSide(Right))
        {
            SubTab subTabToRight = GetSubTabToSide(Right);

            if (subTabToRight != null)
            {
                SetUpWholeRect(wholeRect.x,
                               wholeRect.y,
                               wholeRect.width + (subTabToRight.wholeRect.x - (wholeRect.x + wholeRect.width)),
                               wholeRect.height);
            }
            else
            {
                SetUpWholeRect(wholeRect.x,
                               wholeRect.y,
                               Screen.width - wholeRect.x,
                               wholeRect.height);
            }
        }
        if(HasDeadSpaceToSide(Upper))
        {
            SubTab subTabAbove = GetSubTabToSide(Upper);

            if (subTabAbove != null)
            {
                SetUpWholeRect(wholeRect.x,
                               subTabAbove.wholeRect.y + subTabAbove.wholeRect.height,
                               wholeRect.width,
                               Screen.height - (subTabAbove.wholeRect.y + subTabAbove.wholeRect.height));
            }
            else
            {
                SetUpWholeRect(wholeRect.x,
                               headerRect.height,
                               wholeRect.width,
                               Screen.height - headerRect.height);
            }
        }
        if(HasDeadSpaceToSide(Lower))
        {
            SubTab subTabBelow = GetSubTabToSide(Lower);

            if (subTabBelow != null)
            {
                SetUpWholeRect(wholeRect.x,
                               wholeRect.y,
                               wholeRect.width,
                               subTabBelow.wholeRect.y - (wholeRect.y + wholeRect.height));
            }
            else
            {
                SetUpWholeRect(wholeRect.x,
                               wholeRect.y,
                               wholeRect.width,
                               Screen.height - wholeRect.y);
            }
        }
    }

    //Returns whether or not this SubTab has an adjacent one to its passed side
    public bool HasSideAdjacentSubTab(int side)
    {
        foreach(SubTab subTab in superTab.subTabs)
        {
            if(subTab != this)
            {
                bool subTabIsSideAdjacent = false;

                switch (side)
                {
                    case Left:
                        subTabIsSideAdjacent = subTab.wholeRect.x + subTab.wholeRect.width == wholeRect.x
                                               && ((wholeRect.y >= subTab.wholeRect.y
                                                    && wholeRect.y < subTab.wholeRect.y + subTab.wholeRect.height)
                                                   || (wholeRect.y + wholeRect.height > subTab.wholeRect.y
                                                       && wholeRect.y + wholeRect.height <= subTab.wholeRect.y + subTab.wholeRect.height));
                        break;
                    case Right:
                        subTabIsSideAdjacent = wholeRect.x + wholeRect.width == subTab.wholeRect.x
                                               && ((wholeRect.y >= subTab.wholeRect.y
                                                    && wholeRect.y < subTab.wholeRect.y + subTab.wholeRect.height)
                                                   || (wholeRect.y + wholeRect.height > subTab.wholeRect.y
                                                       && wholeRect.y + wholeRect.height <= subTab.wholeRect.y + subTab.wholeRect.height));
                        break;
                    case Upper:
                        subTabIsSideAdjacent = subTab.wholeRect.y + subTab.wholeRect.height == wholeRect.y
                                               && ((wholeRect.x >= subTab.wholeRect.x
                                                    && wholeRect.x < subTab.wholeRect.x + subTab.wholeRect.width)
                                                   || (wholeRect.x + wholeRect.width > subTab.wholeRect.x
                                                       && wholeRect.x + wholeRect.width <= subTab.wholeRect.x + subTab.wholeRect.width));
                        break;
                    case Lower:
                        subTabIsSideAdjacent = wholeRect.y + wholeRect.height == subTab.wholeRect.y
                                               && ((wholeRect.x >= subTab.wholeRect.x
                                                    && wholeRect.x < subTab.wholeRect.x + subTab.wholeRect.width)
                                                   || (wholeRect.x + wholeRect.width > subTab.wholeRect.x
                                                       && wholeRect.x + wholeRect.width <= subTab.wholeRect.x + subTab.wholeRect.width));
                        break;
                }

                if (subTabIsSideAdjacent)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //Returns whether or not the passed SubTab is adjacent to this one
    public bool SubTabIsAdjacent(SubTab subTab)
    {
        bool subTabIsLeftAdjacent = subTab.wholeRect.x + subTab.wholeRect.width == wholeRect.x
                                    && wholeRect.y >= subTab.wholeRect.y
                                    && wholeRect.y < subTab.wholeRect.y + subTab.wholeRect.height;
        bool subTabIsRightAdjacent = wholeRect.x + wholeRect.width == subTab.wholeRect.x
                                     && wholeRect.y >= subTab.wholeRect.y
                                     && wholeRect.y < subTab.wholeRect.y + subTab.wholeRect.height;
        bool subTabIsAboveAdjacent = subTab.wholeRect.y + subTab.wholeRect.height == wholeRect.y
                                     && wholeRect.x >= subTab.wholeRect.x
                                     && wholeRect.x < subTab.wholeRect.x;
        bool subTabIsBelowAdjacent = wholeRect.y + wholeRect.height == subTab.wholeRect.y
                                     && wholeRect.x >= subTab.wholeRect.x
                                     && wholeRect.x < subTab.wholeRect.x;

        return subTabIsLeftAdjacent || subTabIsRightAdjacent || subTabIsAboveAdjacent || subTabIsBelowAdjacent;
    }

    //Returns the first SubTab which is on the passed side of this SubTab (but not necessarily adjacent to it)
    public SubTab GetSubTabToSide(int side)
    {
        foreach(SubTab subTab in superTab.subTabs)
        {
            if(subTab != this)
            {
                bool subTabIsToSide = false;

                switch (side)
                {
                    case Left:
                        subTabIsToSide = subTab.wholeRect.x < wholeRect.x
                                         && ((wholeRect.y >= subTab.wholeRect.y 
                                              && wholeRect.y < subTab.wholeRect.y + subTab.wholeRect.height)
                                             || (wholeRect.y + wholeRect.height > subTab.wholeRect.y 
                                                 && wholeRect.y + wholeRect.height <= subTab.wholeRect.y + subTab.wholeRect.height));
                        break;
                    case Right:
                        subTabIsToSide = subTab.wholeRect.x > wholeRect.x
                                         && ((wholeRect.y >= subTab.wholeRect.y
                                              && wholeRect.y < subTab.wholeRect.y + subTab.wholeRect.height)
                                             || (wholeRect.y + wholeRect.height > subTab.wholeRect.y
                                                 && wholeRect.y + wholeRect.height <= subTab.wholeRect.y + subTab.wholeRect.height));
                        break;
                    case Upper:
                        subTabIsToSide = subTab.wholeRect.y < wholeRect.y
                                         && ((wholeRect.x >= subTab.wholeRect.x
                                              && wholeRect.x < subTab.wholeRect.x + subTab.wholeRect.width)
                                             || (wholeRect.x + wholeRect.width > subTab.wholeRect.x
                                                 && wholeRect.x + wholeRect.width <= subTab.wholeRect.x + subTab.wholeRect.width));
                        break;
                    case Lower:
                        subTabIsToSide = subTab.wholeRect.y > wholeRect.y
                                         && ((wholeRect.x >= subTab.wholeRect.x
                                              && wholeRect.x < subTab.wholeRect.x + subTab.wholeRect.width)
                                             || (wholeRect.x + wholeRect.width > subTab.wholeRect.x
                                                 && wholeRect.x + wholeRect.width <= subTab.wholeRect.x + subTab.wholeRect.width));
                        break;
                }

                if (subTabIsToSide)
                {
                    return subTab;
                }
            }
        }

        return null;
    }

    //Returns a list containing all of this SubTab's adjacent SubTabs
    public List<SubTab> GetAdjacentSubTabs()
    {
        List<SubTab> adjacentSubTabs = new List<SubTab>();

        foreach(SubTab subTab in superTab.subTabs)
        {
            if(SubTabIsAdjacent(subTab))
            {
                adjacentSubTabs.Add(subTab);
            }
        }

        return adjacentSubTabs;
    }
}
