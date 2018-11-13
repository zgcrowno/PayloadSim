using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SubTab : Tab {

    public const int Lateral = 0; //Represents the left or right sides of the SubTab
    public const int Vertical = 1; //Represents the upper middle or lower middle sides of the SubTab
    public const int Left = 0; 
    public const int Right = 1; 
    public const int Upper = 2; 
    public const int Lower = 3; 
    public const int LowerLeft = 4;
    public const int UpperLeft = 5;
    public const int UpperRight = 6;
    public const int LowerRight = 7;
    public const int None = 8;
    public const int First = 0;
    public const int Second = 1;
    public const int Third = 2;
    public const int ResizeOffset = 5;
    public const int HeaderRectOffset = 20;
    
    public SuperTab superTab; //The SuperTab of which this SubTab is a child
    public Rect[] quadrants; //The left, right, middle-top and middle-bottom quadrants by which is determined the placement of subsequent SubTabs within the overarching SuperTab
    public Rect[] resizeRects; //The left, right, top, bottom, bottom-left, top-left, top-right and bottom-right rects within which the mouse cursor will change to one of four different resize cursors
    
    public float minWidth; //The minimum allowable width of the SubTab
    public float minHeight; //The minimum allowable height of the SubTab
    public float maxWidth; //The maximum allowable width of the SubTab
    public float maxHeight; //The maximum allowable height of the SubTab
    public int resizingWhat; //The int representing which, if any, of this SubTab's sides/corners is currently being resized with the mouse by the player

    // Use this for initialization
    new public void Start () {
        base.Start();
        SetUpWholeRect(superTab.wholeRect.x, superTab.wholeRect.y + superTab.headerRect.height, superTab.wholeRect.width, superTab.wholeRect.height - superTab.headerRect.height);
        minWidth = Screen.width / 4;
        minHeight = (Screen.height - (Screen.height / 20)) / 4;
        maxWidth = Screen.width;
        maxHeight = Screen.height - (Screen.height / 20);
        resizingWhat = None;
    }
	
	// Update is called once per frame
	new public void Update () {
        
	}
    
    /*
     * Method which returns whether or not this SubTab has space for another SubTab on a given side
     * @param side An int representing the side (left/right or top/bottom) on which this SubTab may or may not have space for another SubTab
     * @return a bool representing whether or not this SubTab has any space on the specified side
     */
    public bool HasSpace(int side)
    {
        if(side == Lateral)
        {
            float widthAfterPlacement = wholeRect.width / 2;

            return FloatUtil.GTE(widthAfterPlacement, minWidth);
        }
        else //side == Vertical
        {
            float heightAfterPlacement = wholeRect.height / 2;

            return FloatUtil.GTE(heightAfterPlacement, minHeight);
        }
    }
    
    /*
     * Method which returns the side of the SubTab's wholeRect on which the mouse cursor currently resides
     * @return an int representing the side of the SubTab's wholeRect on which the mouse cursor currently resides
     */
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
    
    /*
     * Sets up all of this SubTab's rects and quadrants based on values passed for the wholeRect datum
     * @param x The x-coordinate to be applied to this SubTab's wholeRect
     * @param y The y-coordinate to be applied to this SubTab's wholeRect
     * @param width The width to be applied to this SubTab's wholeRect
     * @param height The height to be applied to this SubTab's wholeRect
     */
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

        headerRect.x = wholeRect.x + HeaderRectOffset;
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

        //Left resizeRect
        resizeRects[0].x = bodyRect.x;
        resizeRects[0].y = bodyRect.y + ResizeOffset;
        resizeRects[0].width = ResizeOffset;
        resizeRects[0].height = bodyRect.height - (ResizeOffset * 2);

        //Right resizeRect
        resizeRects[1].x = bodyRect.x + bodyRect.width - ResizeOffset;
        resizeRects[1].y = bodyRect.y + ResizeOffset;
        resizeRects[1].width = ResizeOffset;
        resizeRects[1].height = bodyRect.height - (ResizeOffset * 2);

        //Top resizeRect
        resizeRects[2].x = bodyRect.x + ResizeOffset;
        resizeRects[2].y = bodyRect.y;
        resizeRects[2].width = bodyRect.width - (ResizeOffset * 2);
        resizeRects[2].height = ResizeOffset;

        //Bottom resizeRect
        resizeRects[3].x = bodyRect.x + ResizeOffset;
        resizeRects[3].y = bodyRect.y + bodyRect.height - ResizeOffset;
        resizeRects[3].width = bodyRect.width - (ResizeOffset * 2);
        resizeRects[3].height = ResizeOffset;

        //Bottom-left resizeRect
        resizeRects[4].x = bodyRect.x;
        resizeRects[4].y = bodyRect.y + bodyRect.height - ResizeOffset;
        resizeRects[4].width = ResizeOffset;
        resizeRects[4].height = ResizeOffset;

        //Top-left resizeRect
        resizeRects[5].x = bodyRect.x;
        resizeRects[5].y = bodyRect.y;
        resizeRects[5].width = ResizeOffset;
        resizeRects[5].height = ResizeOffset;

        //Top-right resizeRect
        resizeRects[6].x = bodyRect.x + bodyRect.width - ResizeOffset;
        resizeRects[6].y = bodyRect.y;
        resizeRects[6].width = ResizeOffset;
        resizeRects[6].height = ResizeOffset;

        //Bottom-right resizeRect
        resizeRects[7].x = bodyRect.x + bodyRect.width - ResizeOffset;
        resizeRects[7].y = bodyRect.y + bodyRect.height - ResizeOffset;
        resizeRects[7].width = ResizeOffset;
        resizeRects[7].height = ResizeOffset;
    }
    
    /*
     * Method by which this SubTab's GUI elements are drawn on the screen
     */
    public override void Draw()
    {
        GUI.depth = depth;

        if (!superTab.beingDragged)
        {
            //Only draw this SubTab's GUI elements if its superTab object is not being dragged, and therefore condensed to only its header

            if(!beingDragged)
            {
                //If this SubTab is being dragged, only its header is visible

                GUI.DrawTexture(bodyRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
                GUI.DrawTexture(bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);
            }
            GUI.DrawTexture(headerRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
            GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);

            headerStyle.normal.textColor = Color.white;
            GUI.Label(headerRect, headerText, headerStyle);
        }
    }
    
    /*
     * Method by which a tab is placed in a new location or its previously held one
     */
    public override void Place()
    {
        //This SubTab is onle placeable if it's being moved within its current superTab or it's being added to the PlayerInterface's superTabs list, and that list is not full
        if (superTab.bodyRect.Contains(Event.current.mousePosition))
        {
            //Setting to previous position here since the previous position's value is changed in the MouseDrag() method
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

        //Ensure that in the event this SubTab is removed from its current superTab's subTabs collection, the dead space left by its absence will be filled by one or more other SubTabs
        base.FillDeadSpace();
    }
    
    /*
     * Method by which this SubTab is added to the PlayerInterface object's header as a SuperTab
     * 
     * @param superTabToBecomeParent A SuperTab object which will be formatted from this SubTab's data
     */
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
    
    /*
     * Returns whether or not this SubTab has dead/unoccupied/overlapped space adjacent to it (meaning the superTab has dead space which must be filled)
     * @param side The side of this SubTab on which we're checking for dead/unoccupied/overlapped space
     * @return A bool representing whether or not any dead/unoccupied/overlapped space was found
     */
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
                    return !(FloatUtil.Equals(wholeRect.x, 0));
                    break;
                case Right:
                    return !(FloatUtil.Equals(wholeRect.x + wholeRect.width, Screen.width));
                    break;
                case Upper:
                    return !(FloatUtil.Equals(wholeRect.y, headerRect.height));
                    break;
                case Lower:
                    return !(FloatUtil.Equals(wholeRect.y + wholeRect.height, Screen.height));
                    break;
            }

            return false;
        }
    }
    
    /*
     * Returns whether or not this SubTab has dead/unoccupied space adjacent to any of its sides
     * @return A bool representing whether or not any dead/unoccupied/overlapped space was found on any of this SubTab's four sides
     */
    public bool HasDeadSpaceOnAnySide()
    {
        return HasDeadSpaceToSide(Left) || HasDeadSpaceToSide(Right) || HasDeadSpaceToSide(Upper) || HasDeadSpaceToSide(Lower);
    }

    /*
     * Method by which a SubTab alters its own wholeRect to fill up any dead/unoccupied/overlapped space which is adjacent to it
     */
    public new void FillDeadSpace()
    {
        if (depth != 0) //Ensure depth is not equal to zero so that SubTab does not try to fill dead space created by modifying itself
        {
            if (HasDeadSpaceToSide(Left))
            {
                SubTab subTabToLeft = GetNearestSubTabToSide(Left);

                if (subTabToLeft != null)
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
            if (HasDeadSpaceToSide(Right))
            {
                SubTab subTabToRight = GetNearestSubTabToSide(Right);

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
            if (HasDeadSpaceToSide(Upper))
            {
                SubTab subTabAbove = GetNearestSubTabToSide(Upper);

                if (subTabAbove != null)
                {
                    SetUpWholeRect(wholeRect.x,
                                   subTabAbove.wholeRect.y + subTabAbove.wholeRect.height,
                                   wholeRect.width,
                                   wholeRect.height + (wholeRect.y - (subTabAbove.wholeRect.y + subTabAbove.wholeRect.height)));
                }
                else
                {
                    SetUpWholeRect(wholeRect.x,
                                   headerRect.height,
                                   wholeRect.width,
                                   wholeRect.y + wholeRect.height);
                }
            }
            if (HasDeadSpaceToSide(Lower))
            {
                SubTab subTabBelow = GetNearestSubTabToSide(Lower);

                if (subTabBelow != null)
                {
                    SetUpWholeRect(wholeRect.x,
                                   wholeRect.y,
                                   wholeRect.width,
                                   subTabBelow.wholeRect.y - wholeRect.y);
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
    }
    
    /*
     * Returns whether or not this SubTab has an adjacent one to its passed side
     * @param side The side on which we're checking for an adjacent SubTab
     * @return A bool representing whether or not any adjacent SubTab was found on the passed side
     */
    public bool HasSideAdjacentSubTab(int side)
    {
        foreach(SubTab subTab in superTab.subTabs)
        {
            if(subTab != this)
            {
                if(SubTabIsSideAdjacent(subTab, side))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /*
     * Returns whether or not the passed SubTab is adjacent to this one
     * @param subTab The SubTab for which we're checking for adjacency
     * @return A bool representing whether or not the passed SubTab is adjacent to this one
     */
    public bool SubTabIsAdjacent(SubTab subTab)
    {
        return SubTabIsSideAdjacent(subTab, Left) 
               || SubTabIsSideAdjacent(subTab, Right) 
               || SubTabIsSideAdjacent(subTab, Upper) 
               || SubTabIsSideAdjacent(subTab, Lower);
    }

    /*
     * Returns whether or not the passed SubTab is adjacent to this one on the passed side
     * @param subTab The SubTab for which we're checking for adjacency
     * @param side The side of this SubTab on which we're checking for adjacency with subTab
     * @return A bool representing whether or not the passed SubTab is adjacent to this one on the passed side
     */
    public bool SubTabIsSideAdjacent(SubTab subTab, int side)
    {
        switch(side)
        {
            case Left:
                return FloatUtil.Equals(wholeRect.x, subTab.wholeRect.x + subTab.wholeRect.width)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x - wholeRect.width, wholeRect.y, wholeRect.width, wholeRect.height));
                break;
            case Right:
                return FloatUtil.Equals(subTab.wholeRect.x, wholeRect.x + wholeRect.width)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x + wholeRect.width, wholeRect.y, wholeRect.width, wholeRect.height));
                break;
            case Upper:
                return FloatUtil.Equals(wholeRect.y, subTab.wholeRect.y + subTab.wholeRect.height)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x, wholeRect.y - wholeRect.height, wholeRect.width, wholeRect.height));
                break;
            case Lower:
                return FloatUtil.Equals(subTab.wholeRect.y, wholeRect.y + wholeRect.height)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x, wholeRect.y + wholeRect.height, wholeRect.width, wholeRect.height));
                break;
        }

        return false;
    }

    /*
     * Returns whether or not the passed SubTab's passed side is collinear with the passed side of this SubTab
     * @param subTab The SubTab for which we're checking for collinearity
     * @param side The side of both this SubTab and the passed SubTab on which we're checking for collinearity
     * @return A bool representing whether or not the passed SubTab's passed side is collinear with this SubTab's passed side
     */
    public bool SubTabIsSideCollinear(SubTab subTab, int side)
    {
        switch(side)
        {
            case Left:
                return FloatUtil.Equals(wholeRect.x, subTab.wholeRect.x);
                break;
            case Right:
                return FloatUtil.Equals(wholeRect.x + wholeRect.width, subTab.wholeRect.x + subTab.wholeRect.width);
                break;
            case Upper:
                return FloatUtil.Equals(wholeRect.y, subTab.wholeRect.y);
                break;
            case Lower:
                return FloatUtil.Equals(wholeRect.y + wholeRect.height, subTab.wholeRect.y + subTab.wholeRect.height);
                break;
        }

        return false;
    }

    /*
     * Returns whether or not the passed SubTab's side-opposite side is collinear with the passed side of this SubTab
     * @param subTab The SubTab for which we're checking for collinearity
     * @param side The side of this SubTab on which we're checking for collinearity with the opposite side of the passed SubTab
     * @return A bool representing whether or not this SubTab's passed side is collinear with the passed SubTab's opposite side
     */
    public bool SubTabIsOppositeSideCollinear(SubTab subTab, int side)
    {
        switch(side)
        {
            case Left:
                return FloatUtil.Equals(wholeRect.x, subTab.wholeRect.x + subTab.wholeRect.width);
                break;
            case Right:
                return FloatUtil.Equals(subTab.wholeRect.x, wholeRect.x + wholeRect.width);
                break;
            case Upper:
                return FloatUtil.Equals(wholeRect.y, subTab.wholeRect.y + subTab.wholeRect.height);
                break;
            case Lower:
                return FloatUtil.Equals(subTab.wholeRect.y, wholeRect.y + wholeRect.height);
                break;
        }

        return false;
    }

    /*
     * Returns the nearest SubTab which is on the passed side of this SubTab (but not necessarily adjacent to it), or NULL if no SubTab is found which fits the criteria
     * @param side The side of this SubTab on which we're checking for another nearby SubTab
     * @return The nearest SubTab which is on the passed side of this SubTab (but not necessarily adjacent to it), or NULL if no SubTab is found which fits the criteria
     */
    public SubTab GetNearestSubTabToSide(int side)
    {
        SubTab subTabToReturn = null;

        foreach(SubTab subTab in superTab.subTabs)
        {
            if(subTab != this)
            {
                bool subTabIsToSide = false;

                switch (side)
                {
                    case Left:
                        subTabIsToSide = FloatUtil.LT(subTab.wholeRect.x, wholeRect.x)
                                         && ((FloatUtil.GTE(wholeRect.y, subTab.wholeRect.y)
                                              && FloatUtil.LT(wholeRect.y, subTab.wholeRect.y + subTab.wholeRect.height))
                                             || (FloatUtil.GT(wholeRect.y + wholeRect.height, subTab.wholeRect.y)
                                                 && FloatUtil.LTE(wholeRect.y + wholeRect.height, subTab.wholeRect.y + subTab.wholeRect.height)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.GT(subTab.wholeRect.x + subTab.wholeRect.width, subTabToReturn.wholeRect.x + subTabToReturn.wholeRect.width))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                    case Right:
                        subTabIsToSide = FloatUtil.GT(subTab.wholeRect.x, wholeRect.x)
                                         && ((FloatUtil.GTE(wholeRect.y, subTab.wholeRect.y)
                                              && FloatUtil.LT(wholeRect.y, subTab.wholeRect.y + subTab.wholeRect.height))
                                             || (FloatUtil.GT(wholeRect.y + wholeRect.height, subTab.wholeRect.y)
                                                 && FloatUtil.LTE(wholeRect.y + wholeRect.height, subTab.wholeRect.y + subTab.wholeRect.height)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.LT(subTab.wholeRect.x, subTabToReturn.wholeRect.x))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                    case Upper:
                        subTabIsToSide = FloatUtil.LT(subTab.wholeRect.y, wholeRect.y)
                                         && ((FloatUtil.GTE(wholeRect.x, subTab.wholeRect.x)
                                              && FloatUtil.LT(wholeRect.x, subTab.wholeRect.x + subTab.wholeRect.width))
                                             || (FloatUtil.GT(wholeRect.x + wholeRect.width, subTab.wholeRect.x)
                                                 && FloatUtil.LTE(wholeRect.x + wholeRect.width, subTab.wholeRect.x + subTab.wholeRect.width)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.GT(subTab.wholeRect.y + subTab.wholeRect.height, subTabToReturn.wholeRect.y + subTabToReturn.wholeRect.height))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                    case Lower:
                        subTabIsToSide = FloatUtil.GT(subTab.wholeRect.y, wholeRect.y)
                                         && ((FloatUtil.GTE(wholeRect.x, subTab.wholeRect.x)
                                              && FloatUtil.LT(wholeRect.x, subTab.wholeRect.x + subTab.wholeRect.width))
                                             || (FloatUtil.GT(wholeRect.x + wholeRect.width, subTab.wholeRect.x)
                                                 && FloatUtil.LTE(wholeRect.x + wholeRect.width, subTab.wholeRect.x + subTab.wholeRect.width)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.LT(subTab.wholeRect.y, subTabToReturn.wholeRect.y))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                }
            }
        }

        return subTabToReturn;
    }
    
    /*
     * Returns a list containing all of this SubTab's adjacent SubTabs
     * @return A list containing all of this SubTab's adjacent SubTabs
     */
    public List<SubTab> GetAdjacentSubTabs()
    {
        List<SubTab> adjacentSubTabs = new List<SubTab>();

        foreach(SubTab subTab in superTab.subTabs)
        {
            if(subTab != this && SubTabIsAdjacent(subTab))
            {
                adjacentSubTabs.Add(subTab);
            }
        }

        return adjacentSubTabs;
    }

    /*
     * Returns a list containing all of those SubTabs adjacent to this one on a given side
     * @param side An int representing the side of this SubTab on which we're looking for adjacent SubTabs
     * @return A list containing all of those SubTabs adjacent to this one on the passed side
     */
    public List<SubTab> GetSideAdjacentSubTabs(int side)
    {
        List<SubTab> sideAdjacentSubTabs = new List<SubTab>();

        foreach (SubTab subTab in superTab.subTabs)
        {
            if (subTab != this && SubTabIsSideAdjacent(subTab, side))
            {
                sideAdjacentSubTabs.Add(subTab);
            }
        }

        return sideAdjacentSubTabs;
    }

    /*
     * Returns a list containing all of those SubTabs whose passed side is collinear with this SubTab's passed side
     * @param side An int representing the side of this SubTab on which we're looking for collinear SubTabs
     * @return A list containing all of those SubTabs whose passed side is collinear with this SubTab's passed side
     */
    public List<SubTab> GetSideCollinearSubTabs(int side)
    {
        List<SubTab> sideCollinearSubTabs = new List<SubTab>();

        foreach (SubTab subTab in superTab.subTabs)
        {
            if (subTab != this && SubTabIsSideCollinear(subTab, side))
            {
                sideCollinearSubTabs.Add(subTab);
            }
        }

        return sideCollinearSubTabs;
    }

    /*
     * Returns a list containing all of those SubTabs whose side-parameter-opposite sides are collinear with this SubTab's passed side
     * @param side An int representing the side of this SubTab on which we're looking for collinear SubTabs
     * @return A list containing all of those SubTabs whose side-parameter-opposite side is collinear with this SubTab's passed side
     */
    public List<SubTab> GetSideOppositeCollinearSubTabs(int side)
    {
        List<SubTab> sideCollinearSubTabs = new List<SubTab>();

        foreach(SubTab subTab in superTab.subTabs)
        {
            if(subTab != this && SubTabIsOppositeSideCollinear(subTab, side))
            {
                sideCollinearSubTabs.Add(subTab);
            }
        }

        return sideCollinearSubTabs;
    }

    /*
     * Returns a float representing the min or max resize value determined by the passed parameters
     * @param side An int representing the side of this SubTab which is being resized
     * @param pass An int representing which resize pass we're on (first, second, third), and thus which adjacent SubTabs we're iterating through
     * @param adjacentSubTab The adjacent SubTab we're currently consulting in our resizing of this one
     * @param val The min or max resize value that has been determined up to the point this method is called
     * @return A float representing the min or max resize value determined by the passed parameters
     */
    public float GetResizeMinMaxValue(int side, int pass, SubTab adjacentSubTab, float val)
    {
        switch(side)
        {
            case Left:
                switch(pass)
                {
                    case First:
                        if (FloatUtil.GT(adjacentSubTab.wholeRect.x + minWidth, val))
                        {
                            val = adjacentSubTab.wholeRect.x + minWidth;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(val, adjacentSubTab.wholeRect.x + adjacentSubTab.wholeRect.width - minWidth))
                        {
                            val = adjacentSubTab.wholeRect.x + adjacentSubTab.wholeRect.width - minWidth;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(adjacentSubTab.wholeRect.x + minWidth, val))
                        {
                            val = adjacentSubTab.wholeRect.x + minWidth;
                        }
                        break;
                }
                break;
            case Right:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(val, adjacentSubTab.wholeRect.x + adjacentSubTab.wholeRect.width - minWidth - wholeRect.x))
                        {
                            val = adjacentSubTab.wholeRect.x + adjacentSubTab.wholeRect.width - minWidth - wholeRect.x;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(adjacentSubTab.wholeRect.x + minWidth - wholeRect.x, val))
                        {
                            val = adjacentSubTab.wholeRect.x + minWidth - wholeRect.x;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(val, adjacentSubTab.wholeRect.x + adjacentSubTab.wholeRect.width - minWidth - wholeRect.x))
                        {
                            val = adjacentSubTab.wholeRect.x + adjacentSubTab.wholeRect.width - minWidth - wholeRect.x;
                        }
                        break;
                }
                break;
            case Upper:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(adjacentSubTab.wholeRect.y + minHeight, val))
                        {
                            val = adjacentSubTab.wholeRect.y + minHeight;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(val, adjacentSubTab.wholeRect.y + adjacentSubTab.wholeRect.height - minHeight))
                        {
                            val = adjacentSubTab.wholeRect.y + adjacentSubTab.wholeRect.height - minHeight;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(adjacentSubTab.wholeRect.y + minHeight, val))
                        {
                            val = adjacentSubTab.wholeRect.y + minHeight;
                        }
                        break;
                }
                break;
            case Lower:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(val, adjacentSubTab.wholeRect.y + adjacentSubTab.wholeRect.height - minHeight - wholeRect.y))
                        {
                            val = adjacentSubTab.wholeRect.y + adjacentSubTab.wholeRect.height - minHeight - wholeRect.y;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(adjacentSubTab.wholeRect.y + minHeight - wholeRect.y, val))
                        {
                            val = adjacentSubTab.wholeRect.y + minHeight - wholeRect.y;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(val, adjacentSubTab.wholeRect.y + adjacentSubTab.wholeRect.height - minHeight - wholeRect.y))
                        {
                            val = adjacentSubTab.wholeRect.y + adjacentSubTab.wholeRect.height - minHeight - wholeRect.y;
                        }
                        break;
                }
                break;
        }

        return val;
    }


    /*
     * Returns a float representing the new x, y, width or height value to be applied to wholeRect in the Resize() method
     * @param side An int representing the side of this SubTab which is being resized
     * @param minVal The minimum value which must be held by the returned float
     * @param maxVal The maximum value which must be held by the returned float
     * @param mousePos A Vector2 representing the mouse cursor's position on the screen
     * @return A float representing the new x, y, width or height value to be applied to wholeRect in the Resize() method
     */
    public float GetResizeNewValue(int side, float minVal, float maxVal, Vector2 mousePos)
    {
        List<SubTab> relevantAdjacentSubTabs;

        float retVal = 0;

        switch (side)
        {
            case Left:
                if (FloatUtil.GTE(mousePos.x, minVal) && FloatUtil.LTE(mousePos.x, maxVal))
                {
                    retVal = mousePos.x;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.wholeRect.x), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.wholeRect.x + subTab.wholeRect.width)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x + subTab.wholeRect.width;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.wholeRect.x), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.wholeRect.x + subTab.wholeRect.width)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x + subTab.wholeRect.width;
                        }
                    }
                }
                else if (FloatUtil.LT(mousePos.x, minVal))
                {
                    retVal = minVal;
                }
                else if (FloatUtil.GT(mousePos.x, maxVal))
                {
                    retVal = maxVal;
                }
                break;
            case Right:
                if (FloatUtil.GTE(mousePos.x, wholeRect.x + minVal) && FloatUtil.LTE(mousePos.x, wholeRect.x + maxVal))
                {
                    retVal = mousePos.x - wholeRect.x;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.wholeRect.x), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x - wholeRect.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.wholeRect.x + subTab.wholeRect.width)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x + subTab.wholeRect.width - wholeRect.x;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.wholeRect.x), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x - wholeRect.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.wholeRect.x + subTab.wholeRect.width)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.x + subTab.wholeRect.width - wholeRect.x;
                        }
                    }
                }
                else if (FloatUtil.LT(mousePos.x, wholeRect.x + minVal))
                {
                    retVal = minVal;
                }
                else if (FloatUtil.GT(mousePos.x, wholeRect.x + maxVal))
                {
                    retVal = maxVal;
                }
                break;
            case Upper:
                if (FloatUtil.GTE(mousePos.y, minVal) && FloatUtil.LTE(mousePos.y, maxVal))
                {
                    retVal = mousePos.y;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Left);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.wholeRect.y), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.wholeRect.y + subTab.wholeRect.height)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y + subTab.wholeRect.height;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Right);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.wholeRect.y), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.wholeRect.y + subTab.wholeRect.height)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y + subTab.wholeRect.height;
                        }
                    }
                }
                else if (FloatUtil.LT(mousePos.y, minVal))
                {
                    retVal = minVal;
                }
                else if (FloatUtil.GT(mousePos.y, maxVal))
                {
                    retVal = maxVal;
                }
                break;
            case Lower:
                if (FloatUtil.GTE(mousePos.y, wholeRect.y + minVal) && FloatUtil.LTE(mousePos.y, wholeRect.y + maxVal))
                {
                    retVal = mousePos.y - wholeRect.y;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Left);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.wholeRect.y), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y - wholeRect.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.wholeRect.y + subTab.wholeRect.height)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y + subTab.wholeRect.height - wholeRect.y;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Right);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.wholeRect.y), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y - wholeRect.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.wholeRect.y + subTab.wholeRect.height)), ResizeOffset))
                        {
                            retVal = subTab.wholeRect.y + subTab.wholeRect.height - wholeRect.y;
                        }
                    }
                }
                else if (FloatUtil.LT(mousePos.y, wholeRect.y + minVal))
                {
                    retVal = minVal;
                }
                else if (FloatUtil.GT(mousePos.y, wholeRect.y + maxVal))
                {
                    retVal = maxVal;
                }
                break;
        }

        return retVal;
    }

    /*
     * Sets the minimum and maximum values to be held by this SubTab's x, y, width, and/or height properties as it's being resized
     * @param side An int representing the side of this SubTab which is being resized
     * @param firstAdjacentSide An int representing the side(s) of this SubTab's first group of adjacent SubTabs which are being resized in response to this SubTab's resizing
     * @param secondAdjacentSide An int representing the side(s) of this SubTab's second group of adjacent SubTabs which are being resized in response to this SubTab's resizing
     * @param minVal The minimum value to be modified
     * @param maxVal The maximum value to be modified
     * @param firstAdjacentSubTabs The first group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     * @param secondAdjacentSubTabs The second group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     * @param thirdAdjacentSubTabs The third group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     */
    public void SetResizeMinMaxValues(int side, int firstAdjacentSide, int secondAdjacentSide, float minVal, float maxVal, List<SubTab> firstAdjacentSubTabs, List<SubTab> secondAdjacentSubTabs, List<SubTab> thirdAdjacentSubTabs)
    {
        foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
        {
            secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(secondAdjacentSide);
            secondAdjacentSubTabs.Remove(this);

            if (side == Left || side == Upper)
            {
                minVal = GetResizeMinMaxValue(side, First, firstAdjacentSubTab, minVal);
            }
            else // side == Right || side == Lower
            {
                maxVal = GetResizeMinMaxValue(side, First, firstAdjacentSubTab, maxVal);
            }

            foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
            {
                thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(firstAdjacentSide);
                thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                if (side == Left || side == Upper)
                {
                    maxVal = GetResizeMinMaxValue(side, Second, secondAdjacentSubTab, maxVal);
                }
                else // side == Right || side == Lower
                {
                    minVal = GetResizeMinMaxValue(side, Second, secondAdjacentSubTab, minVal);
                }

                foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                {
                    if (side == Left || side == Upper)
                    {
                        minVal = GetResizeMinMaxValue(side, Third, thirdAdjacentSubTab, minVal);
                    }
                    else // side == Right || side == Lower
                    {
                        maxVal = GetResizeMinMaxValue(side, Third, thirdAdjacentSubTab, maxVal);
                    }
                }
            }
        }
    }

    /*
     * Sets up the wholeRects for all of those adjacent SubTabs which are being resized in response to this one's resizing
     * @param side An int representing the side of this SubTab which is being resized
     * @param firstAdjacentSide An int representing the side(s) of this SubTab's first group of adjacent SubTabs which are being resized in response to this SubTab's resizing
     * @param secondAdjacentSide An int representing the side(s) of this SubTab's second group of adjacent SubTabs which are being resized in response to this SubTab's resizing
     * @param firstAdjacentSubTabs The first group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     * @param secondAdjacentSubTabs The second group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     * @param thirdAdjacentSubTabs The third group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     */
    public void SetResizeAdjacentWholeRects(int side, int firstAdjacentSide, int secondAdjacentSide, List<SubTab> firstAdjacentSubTabs, List<SubTab> secondAdjacentSubTabs, List<SubTab> thirdAdjacentSubTabs)
    {
        foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
        {
            secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(secondAdjacentSide);
            secondAdjacentSubTabs.Remove(this);

            switch (side)
            {
                case Left:
                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, wholeRect.x - firstAdjacentSubTab.wholeRect.x, firstAdjacentSubTab.prevWhole.height);
                    break;
                case Right:
                    firstAdjacentSubTab.SetUpWholeRect(wholeRect.x + wholeRect.width, firstAdjacentSubTab.prevWhole.y, (firstAdjacentSubTab.prevWhole.x + firstAdjacentSubTab.prevWhole.width) - (wholeRect.x + wholeRect.width), firstAdjacentSubTab.prevWhole.height);
                    break;
                case Upper:
                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.prevWhole.width, wholeRect.y - firstAdjacentSubTab.wholeRect.y);
                    break;
                case Lower:
                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, wholeRect.y + wholeRect.height, firstAdjacentSubTab.prevWhole.width, (firstAdjacentSubTab.prevWhole.y + firstAdjacentSubTab.prevWhole.height) - (wholeRect.y + wholeRect.height));
                    break;
            }

            foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
            {
                thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(firstAdjacentSide);
                thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                switch (side)
                {
                    case Left:
                        secondAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width, secondAdjacentSubTab.prevWhole.y, (secondAdjacentSubTab.prevWhole.x + secondAdjacentSubTab.prevWhole.width) - (firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width), secondAdjacentSubTab.prevWhole.height);
                        break;
                    case Right:
                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.wholeRect.x - secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.height);
                        break;
                    case Upper:
                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height, secondAdjacentSubTab.prevWhole.width, (secondAdjacentSubTab.prevWhole.y + secondAdjacentSubTab.prevWhole.height) - (firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height));
                        break;
                    case Lower:
                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.prevWhole.width, firstAdjacentSubTab.wholeRect.y - secondAdjacentSubTab.prevWhole.y);
                        break;
                }

                foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                {
                    switch (side)
                    {
                        case Left:
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.wholeRect.x - thirdAdjacentSubTab.wholeRect.x, thirdAdjacentSubTab.prevWhole.height);
                            break;
                        case Right:
                            thirdAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width, thirdAdjacentSubTab.prevWhole.y, (thirdAdjacentSubTab.prevWhole.x + thirdAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), thirdAdjacentSubTab.prevWhole.height);
                            break;
                        case Upper:
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, thirdAdjacentSubTab.prevWhole.width, secondAdjacentSubTab.wholeRect.y - thirdAdjacentSubTab.wholeRect.y);
                            break;
                        case Lower:
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height, thirdAdjacentSubTab.prevWhole.width, (thirdAdjacentSubTab.prevWhole.y + thirdAdjacentSubTab.prevWhole.height) - (secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height));
                            break;
                    }
                }
            }
        }
    }

    /*
     * Resizes one particular side of this SubTab
     * @param side An int representing the side of this SubTab which is being resized
     */
    public void ResizeSide(int side)
    {
        List<SubTab> firstAdjacentSubTabs = new List<SubTab>();
        List<SubTab> secondAdjacentSubTabs = new List<SubTab>();
        List<SubTab> thirdAdjacentSubTabs = new List<SubTab>();

        Vector2 mousePos = Event.current.mousePosition;

        int firstAdjacentSide = Left;
        int secondAdjacentSide = Left;

        float minVal = 0;
        float maxVal = 0;
        
        float newX = prevWhole.x;
        float newY = prevWhole.y;
        float newWidth = prevWhole.width;
        float newHeight = prevWhole.height;

        switch (side)
        {
            case Left:
                firstAdjacentSide = Left;
                secondAdjacentSide = Right;

                minVal = superTab.bodyRect.x;
                maxVal = wholeRect.x + wholeRect.width - minWidth;
                break;
            case Right:
                firstAdjacentSide = Right;
                secondAdjacentSide = Left;

                minVal = minWidth;
                maxVal = maxWidth;
                break;
            case Upper:
                firstAdjacentSide = Upper;
                secondAdjacentSide = Lower;

                minVal = superTab.bodyRect.y;
                maxVal = wholeRect.y + wholeRect.height - minHeight;
                break;
            case Lower:
                firstAdjacentSide = Lower;
                secondAdjacentSide = Upper;

                minVal = minHeight;
                maxVal = maxHeight;
                break;
        }

        firstAdjacentSubTabs = GetSideAdjacentSubTabs(firstAdjacentSide);

        foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
        {
            secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(secondAdjacentSide);
            secondAdjacentSubTabs.Remove(this);

            if(side == Left || side == Upper)
            {
                minVal = GetResizeMinMaxValue(side, First, firstAdjacentSubTab, minVal);
            }
            else // side == Right || side == Lower
            {
                maxVal = GetResizeMinMaxValue(side, First, firstAdjacentSubTab, maxVal);
            }

            foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
            {
                thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(firstAdjacentSide);
                thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                if (side == Left || side == Upper)
                {
                    maxVal = GetResizeMinMaxValue(side, Second, secondAdjacentSubTab, maxVal);
                }
                else // side == Right || side == Lower
                {
                    minVal = GetResizeMinMaxValue(side, Second, secondAdjacentSubTab, minVal);
                }

                foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                {
                    if (side == Left || side == Upper)
                    {
                        minVal = GetResizeMinMaxValue(side, Third, thirdAdjacentSubTab, minVal);
                    }
                    else // side == Right || side == Lower
                    {
                        maxVal = GetResizeMinMaxValue(side, Third, thirdAdjacentSubTab, maxVal);
                    }
                }
            }
        }
        switch (side)
        {
            case Left:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxVal = wholeRect.x;
                }
                else if (FloatUtil.LT(wholeRect.x + wholeRect.width - minWidth, maxVal))
                {
                    maxVal = wholeRect.x + wholeRect.width - minWidth;
                }

                newX = GetResizeNewValue(side, minVal, maxVal, mousePos);
                newY = prevWhole.y;
                newWidth = prevWhole.width + (prevWhole.x - newX);
                newHeight = prevWhole.height;
                break;
            case Right:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minVal = wholeRect.width;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newWidth = GetResizeNewValue(side, minVal, maxVal, mousePos);
                newHeight = prevWhole.height;
                break;
            case Upper:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxVal = wholeRect.y;
                }
                else if (FloatUtil.LT(wholeRect.y + wholeRect.height - minHeight, maxVal))
                {
                    maxVal = wholeRect.y + wholeRect.height - minHeight;
                }

                newX = prevWhole.x;
                newY = GetResizeNewValue(side, minVal, maxVal, mousePos);
                newWidth = prevWhole.width;
                newHeight = prevWhole.height + (prevWhole.y - newY);
                break;
            case Lower:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minVal = wholeRect.height;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newWidth = prevWhole.width;
                newHeight = GetResizeNewValue(side, minVal, maxVal, mousePos);
                break;
        }

        SetUpWholeRect(newX, newY, newWidth, newHeight);

        SetResizeAdjacentWholeRects(side, firstAdjacentSide, secondAdjacentSide, firstAdjacentSubTabs, secondAdjacentSubTabs, thirdAdjacentSubTabs);
    }

    /*
     * Resizes various sides of this SubTab based on the value of its resizingWhat property
     */
    public void Resize()
    {
        switch(resizingWhat)
        {
            case Left:
                ResizeSide(Left);
                break;
            case Right:
                ResizeSide(Right);
                break;
            case Upper:
                ResizeSide(Upper);
                break;
            case Lower:
                ResizeSide(Lower);
                break;
            case LowerLeft:
                ResizeSide(Left);
                ResizeSide(Lower);
                break;
            case UpperLeft:
                ResizeSide(Left);
                ResizeSide(Upper);
                break;
            case UpperRight:
                ResizeSide(Right);
                ResizeSide(Upper);
                break;
            case LowerRight:
                ResizeSide(Right);
                ResizeSide(Lower);
                break;
        }
    }

    /*
     * Executes those behaviors associated with a MouseDown event
     */
    public override void MouseDown()
    {
        if(Event.current.type == EventType.MouseDown)
        {
            if(superTab == pi.GetSuperTabByDepth(0))
            {
                if(headerRect.Contains(Event.current.mousePosition))
                {
                    superTab.SetSubTabToDepth(this, 0);
                    beingDragged = true;
                }
                else
                {
                    foreach (Rect resizeRect in resizeRects)
                    {
                        if (resizeRect.Contains(Event.current.mousePosition))
                        {
                            superTab.SetSubTabToDepth(this, 0);
                            resizingWhat = Array.IndexOf(resizeRects, resizeRect);
                        }
                    }
                }
            }
        }
    }

    /*
     * Executes those behaviors associated with a MouseDrag event
     */
    public override void MouseDrag()
    {
        if(Event.current.type == EventType.MouseDrag)
        {
            if(this == superTab.GetSubTabByDepth(0))
            {
                if(beingDragged)
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
                else if(resizingWhat != None)
                {
                    Resize();
                }
            }
        }
    }

    /*
     * Executes those behaviors associated with a MouseUp event
     */
    public override void MouseUp()
    {
        if(Event.current.type == EventType.MouseUp)
        {
            if(beingDragged)
            {
                beingDragged = false;
                Place();
            }
            else if(resizingWhat != None)
            {
                resizingWhat = None;
            }
        }
    }
}
