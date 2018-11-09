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

    new public void OnGUI()
    {
        base.OnGUI();
    }

    //Method which returns whether or not this SubTab has space for another SubTab on a given side
    public bool HasSpace(int side)
    {
        if(side == Lateral)
        {
            float widthAfterPlacement = wholeRect.width / 2;

            if (FloatUtil.LT(widthAfterPlacement, minWidth))
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

            if (FloatUtil.LT(heightAfterPlacement, minHeight))
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

    //Method by which a tab is placed in a new location or its previously held one
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
        base.FillDeadSpace();
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

    //Returns whether or not this SubTab has dead/unoccupied/overlapped space adjacent to it (meaning the superTab has dead space which must be filled)
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

    //Returns whether or not this SubTab has dead/unoccupied space adjacent to any of its sides
    public bool HasDeadSpaceOnAnySide()
    {
        return HasDeadSpaceToSide(Left) || HasDeadSpaceToSide(Right) || HasDeadSpaceToSide(Upper) || HasDeadSpaceToSide(Lower);
    }

    //Method by which a SubTab alters its own wholeRect to fill up any dead/unoccupied space which is adjacent to it
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

    //Returns whether or not this SubTab has an adjacent one to its passed side
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

    //Returns whether or not the passed SubTab is adjacent to this one
    public bool SubTabIsAdjacent(SubTab subTab)
    {
        return SubTabIsSideAdjacent(subTab, Left) 
               || SubTabIsSideAdjacent(subTab, Right) 
               || SubTabIsSideAdjacent(subTab, Upper) 
               || SubTabIsSideAdjacent(subTab, Lower);
    }

    //Returns whether or not the passed SubTab is adjacent to this one on the passed side
    public bool SubTabIsSideAdjacent(SubTab subTab, int side)
    {
        switch(side)
        {
            case Left:
                //return FloatUtil.Equals(wholeRect.x, subTab.wholeRect.x + subTab.wholeRect.width)
                //       && (wholeRect.Contains(new Vector2(wholeRect.x, subTab.wholeRect.y))
                //           || wholeRect.Contains(new Vector2(wholeRect.x, subTab.wholeRect.y + subTab.wholeRect.height))
                //           || subTab.wholeRect.Contains(new Vector2(subTab.wholeRect.x, wholeRect.y))
                //           || subTab.wholeRect.Contains(new Vector2(subTab.wholeRect.x, wholeRect.y + wholeRect.height)));
                return FloatUtil.Equals(wholeRect.x, subTab.wholeRect.x + subTab.wholeRect.width)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x - wholeRect.width, wholeRect.y, wholeRect.width, wholeRect.height));
                break;
            case Right:
                //return FloatUtil.Equals(subTab.wholeRect.x, wholeRect.x + wholeRect.width)
                //       && (wholeRect.Contains(new Vector2(wholeRect.x, subTab.wholeRect.y))
                //           || wholeRect.Contains(new Vector2(wholeRect.x, subTab.wholeRect.y + subTab.wholeRect.height))
                //           || subTab.wholeRect.Contains(new Vector2(subTab.wholeRect.x, wholeRect.y))
                //           || subTab.wholeRect.Contains(new Vector2(subTab.wholeRect.x, wholeRect.y + wholeRect.height)));
                return FloatUtil.Equals(subTab.wholeRect.x, wholeRect.x + wholeRect.width)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x + wholeRect.width, wholeRect.y, wholeRect.width, wholeRect.height));
                break;
            case Upper:
                //return FloatUtil.Equals(wholeRect.y, subTab.wholeRect.y + subTab.wholeRect.height)
                //       && (wholeRect.Contains(new Vector2(subTab.wholeRect.x, wholeRect.y))
                //           || wholeRect.Contains(new Vector2(subTab.wholeRect.x + subTab.wholeRect.width, wholeRect.y))
                //           || subTab.wholeRect.Contains(new Vector2(wholeRect.x, subTab.wholeRect.y))
                //           || subTab.wholeRect.Contains(new Vector2(wholeRect.x + wholeRect.width, subTab.wholeRect.y)));
                return FloatUtil.Equals(wholeRect.y, subTab.wholeRect.y + subTab.wholeRect.height)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x, wholeRect.y - wholeRect.height, wholeRect.width, wholeRect.height));
                break;
            case Lower:
                //return FloatUtil.Equals(subTab.wholeRect.y, wholeRect.y + wholeRect.height)
                //       && (wholeRect.Contains(new Vector2(subTab.wholeRect.x, wholeRect.y))
                //           || wholeRect.Contains(new Vector2(subTab.wholeRect.x + subTab.wholeRect.width, wholeRect.y))
                //           || subTab.wholeRect.Contains(new Vector2(wholeRect.x, subTab.wholeRect.y))
                //           || subTab.wholeRect.Contains(new Vector2(wholeRect.x + wholeRect.width, subTab.wholeRect.y)));
                return FloatUtil.Equals(subTab.wholeRect.y, wholeRect.y + wholeRect.height)
                       && subTab.wholeRect.Overlaps(new Rect(wholeRect.x, wholeRect.y + wholeRect.height, wholeRect.width, wholeRect.height));
                break;
        }

        return false;
    }

    //Returns whether or not the passed SubTab's passed side is collinear with the passed side of this SubTab
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

    //Returns whether or not the passed SubTab's side-opposite side is collinear with the passed side of this SubTab
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

    //Returns the nearest SubTab which is on the passed side of this SubTab (but not necessarily adjacent to it), or NULL, if no SubTab is found which fits the criteria
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

    //Returns a list containing all of this SubTab's adjacent SubTabs
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

    //Returns a list containing all of those SubTabs adjacent to this one on a given side
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

    //Returns a list containing all of those SubTabs whose passed side is collinear with this SubTab's passed side
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

    //Returns a list containing all of those SubTabs whose side-parameter-opposite sides are collinear with this SubTab's passed side
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

    //TODO: Refactor/simplify this method since all of its cases have roughly the same form
    public void Resize()
    {
        List<SubTab> firstAdjacentSubTabs = new List<SubTab>();
        List<SubTab> secondAdjacentSubTabs = new List<SubTab>();
        List<SubTab> thirdAdjacentSubTabs = new List<SubTab>();
        List<SubTab> relevantAdjacentSubTabs;

        Vector2 mousePos = Event.current.mousePosition;

        float minX = superTab.bodyRect.x;
        float maxX = superTab.bodyRect.x + superTab.bodyRect.width - minWidth;
        float newX = prevWhole.x;
        float minY = superTab.bodyRect.y;
        float maxY = superTab.bodyRect.y + superTab.bodyRect.height - minHeight;
        float newY = prevWhole.y;
        float minimumWidth = minWidth;
        float maximumWidth = maxWidth;
        float newWidth = prevWhole.width;
        float minimumHeight = minHeight;
        float maximumHeight = maxHeight;
        float newHeight = prevWhole.height;

        switch(resizingWhat)
        {
            case Left:
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Left);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.GT(firstAdjacentSubTab.wholeRect.x + minWidth, minX))
                    {
                        minX = firstAdjacentSubTab.wholeRect.x + minWidth;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.LT(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width - minWidth, maxX))
                        {
                            maxX = secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width - minWidth;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.GT(thirdAdjacentSubTab.wholeRect.x + minWidth, minX))
                            {
                                minX = thirdAdjacentSubTab.wholeRect.x + minWidth;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxX = wholeRect.x;
                }
                else if (FloatUtil.LT(wholeRect.x + wholeRect.width - minWidth, maxX))
                {
                    maxX = wholeRect.x + wholeRect.width - minWidth;
                }

                if (FloatUtil.GTE(mousePos.x, minX) && FloatUtil.LTE(mousePos.x, maxX))
                {
                    newX = mousePos.x;
                }
                else if (FloatUtil.LT(mousePos.x, minX))
                {
                    newX = minX;
                }
                else if (FloatUtil.GTE(mousePos.x, maxX))
                {
                    newX = maxX;
                }

                newY = prevWhole.y;
                newWidth = prevWhole.width + (prevWhole.x - newX);
                newHeight = prevWhole.height;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, wholeRect.x - firstAdjacentSubTab.wholeRect.x, firstAdjacentSubTab.prevWhole.height);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width, secondAdjacentSubTab.prevWhole.y, (secondAdjacentSubTab.prevWhole.x + secondAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), secondAdjacentSubTab.prevWhole.height);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.wholeRect.x - thirdAdjacentSubTab.wholeRect.x, thirdAdjacentSubTab.prevWhole.height);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case Right:
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Right);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.LT(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width - minimumWidth - wholeRect.x, maximumWidth))
                    {
                        maximumWidth = firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width - minimumWidth - wholeRect.x;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.GT(secondAdjacentSubTab.wholeRect.x + minWidth, firstAdjacentSubTab.wholeRect.x + minimumWidth))
                        {
                            minimumWidth = secondAdjacentSubTab.wholeRect.x + minWidth - firstAdjacentSubTab.wholeRect.x;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.LT(thirdAdjacentSubTab.wholeRect.x + thirdAdjacentSubTab.wholeRect.width - minimumWidth - secondAdjacentSubTab.wholeRect.x, maximumWidth))
                            {
                                maximumWidth = thirdAdjacentSubTab.wholeRect.x + thirdAdjacentSubTab.wholeRect.width - minimumWidth - secondAdjacentSubTab.wholeRect.x;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minimumWidth = wholeRect.width;
                }

                if (FloatUtil.GTE(mousePos.x, wholeRect.x + minimumWidth) && FloatUtil.LTE(mousePos.x, wholeRect.x + maximumWidth))
                {
                    newWidth = mousePos.x - wholeRect.x;
                }
                else if (FloatUtil.LT(mousePos.x, wholeRect.x + minimumWidth))
                {
                    newWidth = minimumWidth;
                }
                else if (FloatUtil.GTE(mousePos.x, wholeRect.x + maximumWidth))
                {
                    newWidth = maximumWidth;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newHeight = prevWhole.height;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(wholeRect.x + wholeRect.width, firstAdjacentSubTab.prevWhole.y, (firstAdjacentSubTab.prevWhole.x + firstAdjacentSubTab.prevWhole.width) - (wholeRect.x + wholeRect.width), firstAdjacentSubTab.prevWhole.height);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.wholeRect.x - secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.height);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width, thirdAdjacentSubTab.prevWhole.y, (thirdAdjacentSubTab.prevWhole.x + thirdAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), thirdAdjacentSubTab.prevWhole.height);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case Upper:
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.GT(firstAdjacentSubTab.wholeRect.y + minHeight, minY))
                    {
                        minY = firstAdjacentSubTab.wholeRect.y + minHeight;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.LT(secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height - minHeight, maxY))
                        {
                            maxY = secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height - minHeight;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.GT(thirdAdjacentSubTab.wholeRect.y + minHeight, minY))
                            {
                                minY = thirdAdjacentSubTab.wholeRect.y + minHeight;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxY = wholeRect.y;
                }
                else if (FloatUtil.LT(wholeRect.y + wholeRect.height - minHeight, maxY))
                {
                    maxY = wholeRect.y + wholeRect.height - minHeight;
                }

                if (FloatUtil.GTE(mousePos.y, minY) && FloatUtil.LTE(mousePos.y, maxY))
                {
                    newY = mousePos.y;
                }
                else if (FloatUtil.LT(mousePos.y, minY))
                {
                    newY = minY;
                }
                else if (FloatUtil.GTE(mousePos.y, maxY))
                {
                    newY = maxY;
                }

                newX = prevWhole.x;
                newWidth = prevWhole.width;
                newHeight = prevWhole.height + (prevWhole.y - newY);

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.prevWhole.width, wholeRect.y - firstAdjacentSubTab.wholeRect.y);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height, secondAdjacentSubTab.prevWhole.width, (secondAdjacentSubTab.prevWhole.y + secondAdjacentSubTab.prevWhole.height) - (firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height));

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, thirdAdjacentSubTab.prevWhole.width, secondAdjacentSubTab.wholeRect.y - thirdAdjacentSubTab.wholeRect.y);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case Lower:
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.LT(firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height - minimumHeight - wholeRect.y, maximumHeight))
                    {
                        maximumHeight = firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height - minimumHeight - wholeRect.y;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.GT(secondAdjacentSubTab.wholeRect.y + minHeight, firstAdjacentSubTab.wholeRect.y + minimumHeight))
                        {
                            minimumHeight = secondAdjacentSubTab.wholeRect.y + minHeight - firstAdjacentSubTab.wholeRect.y;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.LT(thirdAdjacentSubTab.wholeRect.y + thirdAdjacentSubTab.wholeRect.height - minimumHeight - secondAdjacentSubTab.wholeRect.y, maximumHeight))
                            {
                                maximumHeight = thirdAdjacentSubTab.wholeRect.y + thirdAdjacentSubTab.wholeRect.height - minimumHeight - secondAdjacentSubTab.wholeRect.y;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minimumHeight = wholeRect.height;
                }

                if (FloatUtil.GTE(mousePos.y, wholeRect.y + minimumHeight) && FloatUtil.LTE(mousePos.y, wholeRect.y + maximumHeight))
                {
                    newHeight = mousePos.y - wholeRect.y;
                }
                else if (FloatUtil.LT(mousePos.y, wholeRect.y + minimumHeight))
                {
                    newHeight = minimumHeight;
                }
                else if (FloatUtil.GTE(mousePos.y, wholeRect.y + maximumHeight))
                {
                    newHeight = maximumHeight;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newWidth = prevWhole.width;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, wholeRect.y + wholeRect.height, firstAdjacentSubTab.prevWhole.width, (firstAdjacentSubTab.prevWhole.y + firstAdjacentSubTab.prevWhole.height) - (wholeRect.y + wholeRect.height));

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.prevWhole.width, firstAdjacentSubTab.wholeRect.y - secondAdjacentSubTab.prevWhole.y);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height, thirdAdjacentSubTab.prevWhole.width, (thirdAdjacentSubTab.prevWhole.y + thirdAdjacentSubTab.prevWhole.height) - (secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height));
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case LowerLeft:
                //Regular Left case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Left);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.GT(firstAdjacentSubTab.wholeRect.x + minWidth, minX))
                    {
                        minX = firstAdjacentSubTab.wholeRect.x + minWidth;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.LT(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width - minWidth, maxX))
                        {
                            maxX = secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width - minWidth;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.GT(thirdAdjacentSubTab.wholeRect.x + minWidth, minX))
                            {
                                minX = thirdAdjacentSubTab.wholeRect.x + minWidth;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxX = wholeRect.x;
                }
                else if (FloatUtil.LT(wholeRect.x + wholeRect.width - minWidth, maxX))
                {
                    maxX = wholeRect.x + wholeRect.width - minWidth;
                }

                if (FloatUtil.GTE(mousePos.x, minX) && FloatUtil.LTE(mousePos.x, maxX))
                {
                    newX = mousePos.x;
                }
                else if (FloatUtil.LT(mousePos.x, minX))
                {
                    newX = minX;
                }
                else if (FloatUtil.GTE(mousePos.x, maxX))
                {
                    newX = maxX;
                }

                newY = prevWhole.y;
                newWidth = prevWhole.width + (prevWhole.x - newX);
                newHeight = prevWhole.height;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, wholeRect.x - firstAdjacentSubTab.wholeRect.x, firstAdjacentSubTab.prevWhole.height);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width, secondAdjacentSubTab.prevWhole.y, (secondAdjacentSubTab.prevWhole.x + secondAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), secondAdjacentSubTab.prevWhole.height);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.wholeRect.x - thirdAdjacentSubTab.wholeRect.x, thirdAdjacentSubTab.prevWhole.height);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();

                //Regular Lower case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.LT(firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height - minimumHeight - wholeRect.y, maximumHeight))
                    {
                        maximumHeight = firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height - minimumHeight - wholeRect.y;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.GT(secondAdjacentSubTab.wholeRect.y + minHeight, firstAdjacentSubTab.wholeRect.y + minimumHeight))
                        {
                            minimumHeight = secondAdjacentSubTab.wholeRect.y + minHeight - firstAdjacentSubTab.wholeRect.y;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.LT(thirdAdjacentSubTab.wholeRect.y + thirdAdjacentSubTab.wholeRect.height - minimumHeight - secondAdjacentSubTab.wholeRect.y, maximumHeight))
                            {
                                maximumHeight = thirdAdjacentSubTab.wholeRect.y + thirdAdjacentSubTab.wholeRect.height - minimumHeight - secondAdjacentSubTab.wholeRect.y;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minimumHeight = wholeRect.height;
                }

                if (FloatUtil.GTE(mousePos.y, wholeRect.y + minimumHeight) && FloatUtil.LTE(mousePos.y, wholeRect.y + maximumHeight))
                {
                    newHeight = mousePos.y - wholeRect.y;
                }
                else if (FloatUtil.LT(mousePos.y, wholeRect.y + minimumHeight))
                {
                    newHeight = minimumHeight;
                }
                else if (FloatUtil.GTE(mousePos.y, wholeRect.y + maximumHeight))
                {
                    newHeight = maximumHeight;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newWidth = prevWhole.width;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, wholeRect.y + wholeRect.height, firstAdjacentSubTab.prevWhole.width, (firstAdjacentSubTab.prevWhole.y + firstAdjacentSubTab.prevWhole.height) - (wholeRect.y + wholeRect.height));

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.prevWhole.width, firstAdjacentSubTab.wholeRect.y - secondAdjacentSubTab.prevWhole.y);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height, thirdAdjacentSubTab.prevWhole.width, (thirdAdjacentSubTab.prevWhole.y + thirdAdjacentSubTab.prevWhole.height) - (secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height));
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case UpperLeft:
                //Regular Left case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Left);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.GT(firstAdjacentSubTab.wholeRect.x + minWidth, minX))
                    {
                        minX = firstAdjacentSubTab.wholeRect.x + minWidth;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.LT(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width - minWidth, maxX))
                        {
                            maxX = secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width - minWidth;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.GT(thirdAdjacentSubTab.wholeRect.x + minWidth, minX))
                            {
                                minX = thirdAdjacentSubTab.wholeRect.x + minWidth;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxX = wholeRect.x;
                }
                else if (FloatUtil.LT(wholeRect.x + wholeRect.width - minWidth, maxX))
                {
                    maxX = wholeRect.x + wholeRect.width - minWidth;
                }

                if (FloatUtil.GTE(mousePos.x, minX) && FloatUtil.LTE(mousePos.x, maxX))
                {
                    newX = mousePos.x;
                }
                else if (FloatUtil.LT(mousePos.x, minX))
                {
                    newX = minX;
                }
                else if (FloatUtil.GTE(mousePos.x, maxX))
                {
                    newX = maxX;
                }

                newY = prevWhole.y;
                newWidth = prevWhole.width + (prevWhole.x - newX);
                newHeight = prevWhole.height;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, wholeRect.x - firstAdjacentSubTab.wholeRect.x, firstAdjacentSubTab.prevWhole.height);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width, secondAdjacentSubTab.prevWhole.y, (secondAdjacentSubTab.prevWhole.x + secondAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), secondAdjacentSubTab.prevWhole.height);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.wholeRect.x - thirdAdjacentSubTab.wholeRect.x, thirdAdjacentSubTab.prevWhole.height);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();

                //Regular Upper case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.GT(firstAdjacentSubTab.wholeRect.y + minHeight, minY))
                    {
                        minY = firstAdjacentSubTab.wholeRect.y + minHeight;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.LT(secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height - minHeight, maxY))
                        {
                            maxY = secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height - minHeight;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.GT(thirdAdjacentSubTab.wholeRect.y + minHeight, minY))
                            {
                                minY = thirdAdjacentSubTab.wholeRect.y + minHeight;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxY = wholeRect.y;
                }
                else if (FloatUtil.LT(wholeRect.y + wholeRect.height - minHeight, maxY))
                {
                    maxY = wholeRect.y + wholeRect.height - minHeight;
                }

                if (FloatUtil.GTE(mousePos.y, minY) && FloatUtil.LTE(mousePos.y, maxY))
                {
                    newY = mousePos.y;
                }
                else if (FloatUtil.LT(mousePos.y, minY))
                {
                    newY = minY;
                }
                else if (FloatUtil.GTE(mousePos.y, maxY))
                {
                    newY = maxY;
                }

                newX = prevWhole.x;
                newWidth = prevWhole.width;
                newHeight = prevWhole.height + (prevWhole.y - newY);

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.prevWhole.width, wholeRect.y - firstAdjacentSubTab.wholeRect.y);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height, secondAdjacentSubTab.prevWhole.width, (secondAdjacentSubTab.prevWhole.y + secondAdjacentSubTab.prevWhole.height) - (firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height));

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, thirdAdjacentSubTab.prevWhole.width, secondAdjacentSubTab.wholeRect.y - thirdAdjacentSubTab.wholeRect.y);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case UpperRight:
                //Regular Right case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Right);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.LT(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width - minimumWidth - wholeRect.x, maximumWidth))
                    {
                        maximumWidth = firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width - minimumWidth - wholeRect.x;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.GT(secondAdjacentSubTab.wholeRect.x + minWidth, firstAdjacentSubTab.wholeRect.x + minimumWidth))
                        {
                            minimumWidth = secondAdjacentSubTab.wholeRect.x + minWidth - firstAdjacentSubTab.wholeRect.x;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.LT(thirdAdjacentSubTab.wholeRect.x + thirdAdjacentSubTab.wholeRect.width - minimumWidth - secondAdjacentSubTab.wholeRect.x, maximumWidth))
                            {
                                maximumWidth = thirdAdjacentSubTab.wholeRect.x + thirdAdjacentSubTab.wholeRect.width - minimumWidth - secondAdjacentSubTab.wholeRect.x;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minimumWidth = wholeRect.width;
                }

                if (FloatUtil.GTE(mousePos.x, wholeRect.x + minimumWidth) && FloatUtil.LTE(mousePos.x, wholeRect.x + maximumWidth))
                {
                    newWidth = mousePos.x - wholeRect.x;
                }
                else if (FloatUtil.LT(mousePos.x, wholeRect.x + minimumWidth))
                {
                    newWidth = minimumWidth;
                }
                else if (FloatUtil.GTE(mousePos.x, wholeRect.x + maximumWidth))
                {
                    newWidth = maximumWidth;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newHeight = prevWhole.height;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(wholeRect.x + wholeRect.width, firstAdjacentSubTab.prevWhole.y, (firstAdjacentSubTab.prevWhole.x + firstAdjacentSubTab.prevWhole.width) - (wholeRect.x + wholeRect.width), firstAdjacentSubTab.prevWhole.height);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.wholeRect.x - secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.height);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width, thirdAdjacentSubTab.prevWhole.y, (thirdAdjacentSubTab.prevWhole.x + thirdAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), thirdAdjacentSubTab.prevWhole.height);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();

                //Regular Upper case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.GT(firstAdjacentSubTab.wholeRect.y + minHeight, minY))
                    {
                        minY = firstAdjacentSubTab.wholeRect.y + minHeight;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.LT(secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height - minHeight, maxY))
                        {
                            maxY = secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height - minHeight;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.GT(thirdAdjacentSubTab.wholeRect.y + minHeight, minY))
                            {
                                minY = thirdAdjacentSubTab.wholeRect.y + minHeight;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxY = wholeRect.y;
                }
                else if (FloatUtil.LT(wholeRect.y + wholeRect.height - minHeight, maxY))
                {
                    maxY = wholeRect.y + wholeRect.height - minHeight;
                }

                if (FloatUtil.GTE(mousePos.y, minY) && FloatUtil.LTE(mousePos.y, maxY))
                {
                    newY = mousePos.y;
                }
                else if (FloatUtil.LT(mousePos.y, minY))
                {
                    newY = minY;
                }
                else if (FloatUtil.GTE(mousePos.y, maxY))
                {
                    newY = maxY;
                }

                newX = prevWhole.x;
                newWidth = prevWhole.width;
                newHeight = prevWhole.height + (prevWhole.y - newY);

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.prevWhole.width, wholeRect.y - firstAdjacentSubTab.wholeRect.y);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height, secondAdjacentSubTab.prevWhole.width, (secondAdjacentSubTab.prevWhole.y + secondAdjacentSubTab.prevWhole.height) - (firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height));

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, thirdAdjacentSubTab.prevWhole.y, thirdAdjacentSubTab.prevWhole.width, secondAdjacentSubTab.wholeRect.y - thirdAdjacentSubTab.wholeRect.y);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
            case LowerRight:
                //Regular Right case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Right);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.LT(firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width - minimumWidth - wholeRect.x, maximumWidth))
                    {
                        maximumWidth = firstAdjacentSubTab.wholeRect.x + firstAdjacentSubTab.wholeRect.width - minimumWidth - wholeRect.x;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.GT(secondAdjacentSubTab.wholeRect.x + minWidth, firstAdjacentSubTab.wholeRect.x + minimumWidth))
                        {
                            minimumWidth = secondAdjacentSubTab.wholeRect.x + minWidth - firstAdjacentSubTab.wholeRect.x;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.LT(thirdAdjacentSubTab.wholeRect.x + thirdAdjacentSubTab.wholeRect.width - minimumWidth - secondAdjacentSubTab.wholeRect.x, maximumWidth))
                            {
                                maximumWidth = thirdAdjacentSubTab.wholeRect.x + thirdAdjacentSubTab.wholeRect.width - minimumWidth - secondAdjacentSubTab.wholeRect.x;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minimumWidth = wholeRect.width;
                }

                if (FloatUtil.GTE(mousePos.x, wholeRect.x + minimumWidth) && FloatUtil.LTE(mousePos.x, wholeRect.x + maximumWidth))
                {
                    newWidth = mousePos.x - wholeRect.x;
                }
                else if (FloatUtil.LT(mousePos.x, wholeRect.x + minimumWidth))
                {
                    newWidth = minimumWidth;
                }
                else if (FloatUtil.GTE(mousePos.x, wholeRect.x + maximumWidth))
                {
                    newWidth = maximumWidth;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newHeight = prevWhole.height;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Left);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(wholeRect.x + wholeRect.width, firstAdjacentSubTab.prevWhole.y, (firstAdjacentSubTab.prevWhole.x + firstAdjacentSubTab.prevWhole.width) - (wholeRect.x + wholeRect.width), firstAdjacentSubTab.prevWhole.height);

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Right);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, firstAdjacentSubTab.wholeRect.x - secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.height);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width, thirdAdjacentSubTab.prevWhole.y, (thirdAdjacentSubTab.prevWhole.x + thirdAdjacentSubTab.prevWhole.width) - (secondAdjacentSubTab.wholeRect.x + secondAdjacentSubTab.wholeRect.width), thirdAdjacentSubTab.prevWhole.height);
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();

                //Regular Lower case, as above
                firstAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                    secondAdjacentSubTabs.Remove(this);

                    if (FloatUtil.LT(firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height - minimumHeight - wholeRect.y, maximumHeight))
                    {
                        maximumHeight = firstAdjacentSubTab.wholeRect.y + firstAdjacentSubTab.wholeRect.height - minimumHeight - wholeRect.y;
                    }

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        if (FloatUtil.GT(secondAdjacentSubTab.wholeRect.y + minHeight, firstAdjacentSubTab.wholeRect.y + minimumHeight))
                        {
                            minimumHeight = secondAdjacentSubTab.wholeRect.y + minHeight - firstAdjacentSubTab.wholeRect.y;
                        }

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            if (FloatUtil.LT(thirdAdjacentSubTab.wholeRect.y + thirdAdjacentSubTab.wholeRect.height - minimumHeight - secondAdjacentSubTab.wholeRect.y, maximumHeight))
                            {
                                maximumHeight = thirdAdjacentSubTab.wholeRect.y + thirdAdjacentSubTab.wholeRect.height - minimumHeight - secondAdjacentSubTab.wholeRect.y;
                            }
                        }
                    }
                }
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minimumHeight = wholeRect.height;
                }

                if (FloatUtil.GTE(mousePos.y, wholeRect.y + minimumHeight) && FloatUtil.LTE(mousePos.y, wholeRect.y + maximumHeight))
                {
                    newHeight = mousePos.y - wholeRect.y;
                }
                else if (FloatUtil.LT(mousePos.y, wholeRect.y + minimumHeight))
                {
                    newHeight = minimumHeight;
                }
                else if (FloatUtil.GTE(mousePos.y, wholeRect.y + maximumHeight))
                {
                    newHeight = maximumHeight;
                }

                newX = prevWhole.x;
                newY = prevWhole.y;
                newWidth = prevWhole.width;

                SetUpWholeRect(newX, newY, newWidth, newHeight);

                foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
                {
                    secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(Upper);
                    secondAdjacentSubTabs.Remove(this);

                    firstAdjacentSubTab.SetUpWholeRect(firstAdjacentSubTab.prevWhole.x, wholeRect.y + wholeRect.height, firstAdjacentSubTab.prevWhole.width, (firstAdjacentSubTab.prevWhole.y + firstAdjacentSubTab.prevWhole.height) - (wholeRect.y + wholeRect.height));

                    foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
                    {
                        thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(Lower);
                        thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                        secondAdjacentSubTab.SetUpWholeRect(secondAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.prevWhole.y, secondAdjacentSubTab.prevWhole.width, firstAdjacentSubTab.wholeRect.y - secondAdjacentSubTab.prevWhole.y);

                        foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                        {
                            thirdAdjacentSubTab.SetUpWholeRect(thirdAdjacentSubTab.prevWhole.x, secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height, thirdAdjacentSubTab.prevWhole.width, (thirdAdjacentSubTab.prevWhole.y + thirdAdjacentSubTab.prevWhole.height) - (secondAdjacentSubTab.wholeRect.y + secondAdjacentSubTab.wholeRect.height));
                        }
                    }
                }

                superTab.SortSubTabsByDepth(SuperTab.Descending);
                superTab.FillDeadSpace();
                break;
        }
    }

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

    public override void MouseUp()
    {
        if(Event.current.type == EventType.MouseUp)
        {
            if(beingDragged || resizingWhat != None)
            {
                beingDragged = false;
                resizingWhat = None;

                Place();
            }
        }
    }
}
