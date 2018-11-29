using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubTab : Tab {

    public const int Lateral = 0;
    public const int Vertical = 1;
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

    public Rect[] quadrants = new Rect[4];
    public Rect[] resizeRects = new Rect[8];

    public SuperTab superTab; //The SuperTab of which this SubTab is a child
    
    public float minWidth; //The minimum allowable width of the SubTab
    public float minHeight; //The minimum allowable height of the SubTab
    public float maxWidth; //The maximum allowable width of the SubTab
    public float maxHeight; //The maximum allowable height of the SubTab
    public int resizingWhat; //The int representing which, if any, of this SubTab's sides/corners is currently being resized with the mouse by the player
    
    new public void Start () {
        base.Start();
        minWidth = Screen.width / 4;
        minHeight = (Screen.height - (Screen.height / 20)) / 4;
        maxWidth = Screen.width;
        maxHeight = Screen.height - (Screen.height / 20);
        resizingWhat = None;
        transform.SetParent(superTab.body.transform);

        RectTransform superTabBodyRect = superTab.GetComponent<SuperTab>().brt;
        SetUp(new Vector2(superTabBodyRect.anchoredPosition.x, superTabBodyRect.anchoredPosition.y), new Vector2(superTabBodyRect.sizeDelta.x, superTabBodyRect.sizeDelta.y));
    }

    public override void OnDrag(PointerEventData ped)
    {
        if (IsFrontmost())
        {
            if(beingDragged)
            {
                body.SetActive(false);
                hrt.position = new Vector2(Input.mousePosition.x - (hrt.sizeDelta.x / 2), Input.mousePosition.y - (hrt.sizeDelta.y / 2));

                foreach (SubTab subTab in superTab.GetComponent<SuperTab>().subTabs)
                {
                    if (subTab != this)
                    {
                        if (RectTransformUtility.RectangleContainsScreenPoint(subTab.rt, Input.mousePosition)) {
                            Vector4 prevSubTabRT = new Vector4
                            {
                                x = subTab.prt.anchoredPosition.x,
                                y = subTab.prt.anchoredPosition.y,
                                w = subTab.prt.sizeDelta.x,
                                z = subTab.prt.sizeDelta.y
                            };

                            subTab.SetUp(new Vector2(prt.anchoredPosition.x, prt.anchoredPosition.y),
                                new Vector2(prt.sizeDelta.x, prt.sizeDelta.y));
                            SetUp(new Vector2(prevSubTabRT.x, prevSubTabRT.y),
                                new Vector2(prevSubTabRT.w, prevSubTabRT.z));
                            subTab.transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
                        }
                    }
                }
            }
            else if(resizingWhat != None)
            {
                Resize();
            }
        }
    }

    public override void OnPointerDown(PointerEventData ped)
    {
        transform.SetAsLastSibling();
        if (RectTransformUtility.RectangleContainsScreenPoint(hrt, Input.mousePosition))
        {
            beingDragged = true;
        }
        else
        {
            foreach (Rect resizeRect in resizeRects)
            {
                if (resizeRect.Contains(Input.mousePosition))
                {
                    resizingWhat = Array.IndexOf(resizeRects, resizeRect);
                }
            }
        }
    }

    public override void OnPointerUp(PointerEventData ped)
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

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        rt.anchoredPosition = new Vector2(pos.x, pos.y);
        rt.sizeDelta = new Vector2(size.x, size.y);
        prt = rt;
        hrt.anchoredPosition = new Vector2(HeaderRectOffset, rt.sizeDelta.y - (Screen.height / 20));
        hrt.sizeDelta = new Vector2(Screen.width / PlayerInterface.MaxSuperTabs, Screen.height / 20);
        brt.anchoredPosition = Vector2.zero;
        brt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - (hrt.sizeDelta.y / 2));

        //Left quadrant
        quadrants[0].x = rt.position.x;
        quadrants[0].y = rt.position.y;
        quadrants[0].width = rt.sizeDelta.x / 3;
        quadrants[0].height = rt.sizeDelta.y;

        //Right quadrant
        quadrants[1].x = rt.position.x + rt.sizeDelta.x - (rt.sizeDelta.x / 3);
        quadrants[1].y = rt.position.y;
        quadrants[1].width = rt.sizeDelta.x / 3;
        quadrants[1].height = rt.sizeDelta.y;

        //Upper quadrant
        quadrants[2].x = rt.position.x + (rt.sizeDelta.x / 3);
        quadrants[2].y = rt.position.y + (rt.sizeDelta.y / 2);
        quadrants[2].width = rt.sizeDelta.x / 3;
        quadrants[2].height = rt.sizeDelta.y / 2;

        //Lower quadrant
        quadrants[3].x = rt.position.x + (rt.sizeDelta.x / 3);
        quadrants[3].y = rt.position.y;
        quadrants[3].width = rt.sizeDelta.x / 3;
        quadrants[3].height = rt.sizeDelta.y / 2;

        //Left resizeRect
        resizeRects[0].x = brt.position.x;
        resizeRects[0].y = brt.position.y + ResizeOffset;
        resizeRects[0].width = ResizeOffset;
        resizeRects[0].height = brt.sizeDelta.y - (ResizeOffset * 2);

        //Right resizeRect
        resizeRects[1].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[1].y = brt.position.y + ResizeOffset;
        resizeRects[1].width = ResizeOffset;
        resizeRects[1].height = brt.sizeDelta.y - (ResizeOffset * 2);

        //Top resizeRect
        resizeRects[2].x = brt.position.x + ResizeOffset;
        resizeRects[2].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[2].width = brt.sizeDelta.x - (ResizeOffset * 2);
        resizeRects[2].height = ResizeOffset;

        //Bottom resizeRect
        resizeRects[3].x = brt.position.x + ResizeOffset;
        resizeRects[3].y = brt.position.y;
        resizeRects[3].width = brt.sizeDelta.x - (ResizeOffset * 2);
        resizeRects[3].height = ResizeOffset;

        //Bottom-left resizeRect
        resizeRects[4].x = brt.position.x;
        resizeRects[4].y = brt.position.y;
        resizeRects[4].width = ResizeOffset;
        resizeRects[4].height = ResizeOffset;

        //Top-left resizeRect
        resizeRects[5].x = brt.position.x;
        resizeRects[5].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[5].width = ResizeOffset;
        resizeRects[5].height = ResizeOffset;

        //Top-right resizeRect
        resizeRects[6].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[6].y = brt.position.y + brt.sizeDelta.y - ResizeOffset;
        resizeRects[6].width = ResizeOffset;
        resizeRects[6].height = ResizeOffset;

        //Bottom-right resizeRect
        resizeRects[7].x = brt.position.x + brt.sizeDelta.x - ResizeOffset;
        resizeRects[7].y = brt.position.y;
        resizeRects[7].width = ResizeOffset;
        resizeRects[7].height = ResizeOffset;
    }

    /*
     * Method by which a tab is placed in a new location, or its previously held one if it's not being placed appropriately
     */
    public override void Place()
    {
        //This SubTab is onle placeable if it's being moved within its current superTab or it's being added to the PlayerInterface's superTabs list, and that list is not full
        if (RectTransformUtility.RectangleContainsScreenPoint(superTab.brt, Input.mousePosition))
        {
            //Setting to previous position here since the previous position's value is changed in the OnDrag() method
            SnapToPreviousPosition();
            superTab.transform.SetAsLastSibling();
        }
        else if (pi.superTabs.Count < PlayerInterface.MaxSuperTabs && superTab.subTabs.Count > 1)
        {
            //Adding as new SuperTab since the mouse cursor isn't contained within superTab's brt
            AddAsSuperTab(Instantiate(Resources.Load("Prefabs/SuperTabPrefab") as GameObject).GetComponent<SuperTab>());
        }
        else
        {
            //If not placeable, the SubTab is simply snapped back to its previous position
            SnapToPreviousPosition();
        }

        body.SetActive(true);

        //Ensure that in the event this SubTab is removed from its current superTab's subTabs collection, the dead space left by its absence will be filled by one or more other SubTabs
        base.FillDeadSpace();
    }

    /*
     * Resizes various sides of this SubTab based on the value of its resizingWhat property
     */
    public void Resize()
    {
        switch (resizingWhat)
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
     * Resizes one particular side of this SubTab
     * @param side An int representing the side of this SubTab which is being resized
     */
    public void ResizeSide(int side)
    {
        List<SubTab> firstAdjacentSubTabs = new List<SubTab>();
        List<SubTab> secondAdjacentSubTabs = new List<SubTab>();
        List<SubTab> thirdAdjacentSubTabs = new List<SubTab>();

        Vector2 mousePos = Input.mousePosition;

        int firstAdjacentSide = Left;
        int secondAdjacentSide = Left;

        float minVal = 0;
        float maxVal = 0;

        float newX = prt.anchoredPosition.x;
        float newY = prt.anchoredPosition.y;
        float newWidth = prt.sizeDelta.x;
        float newHeight = prt.sizeDelta.y;

        switch (side)
        {
            case Left:
                firstAdjacentSide = Left;
                secondAdjacentSide = Right;

                minVal = superTab.GetComponent<SuperTab>().brt.anchoredPosition.x;
                maxVal = rt.anchoredPosition.x + rt.sizeDelta.x - minWidth;
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

                minVal = minHeight;
                maxVal = maxHeight;
                break;
            case Lower:
                firstAdjacentSide = Lower;
                secondAdjacentSide = Upper;

                minVal = superTab.GetComponent<SuperTab>().brt.anchoredPosition.y;
                maxVal = rt.anchoredPosition.y + rt.sizeDelta.y - minHeight;
                break;
        }

        firstAdjacentSubTabs = GetSideAdjacentSubTabs(firstAdjacentSide);

        foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
        {
            secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(secondAdjacentSide);
            secondAdjacentSubTabs.Remove(this);

            if (side == Left || side == Lower)
            {
                minVal = GetResizeMinMaxValue(side, First, firstAdjacentSubTab, minVal);
            }
            else // side == Right || side == Upper
            {
                maxVal = GetResizeMinMaxValue(side, First, firstAdjacentSubTab, maxVal);
            }

            foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
            {
                thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(firstAdjacentSide);
                thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                if (side == Left || side == Lower)
                {
                    maxVal = GetResizeMinMaxValue(side, Second, secondAdjacentSubTab, maxVal);
                }
                else // side == Right || side == Upper
                {
                    minVal = GetResizeMinMaxValue(side, Second, secondAdjacentSubTab, minVal);
                }

                foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                {
                    if (side == Left || side == Lower)
                    {
                        minVal = GetResizeMinMaxValue(side, Third, thirdAdjacentSubTab, minVal);
                    }
                    else // side == Right || side == Upper
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
                    maxVal = rt.anchoredPosition.x;
                }
                else if (FloatUtil.LT(rt.anchoredPosition.x + rt.sizeDelta.x - minWidth, maxVal))
                {
                    maxVal = rt.anchoredPosition.x + rt.sizeDelta.x - minWidth;
                }

                newX = GetResizeNewValue(side, minVal, maxVal, mousePos);
                newY = prt.anchoredPosition.y;
                newWidth = prt.sizeDelta.x + (prt.anchoredPosition.x - newX);
                newHeight = prt.sizeDelta.y;
                break;
            case Right:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minVal = rt.sizeDelta.x;
                }

                newX = prt.anchoredPosition.x;
                newY = prt.anchoredPosition.y;
                newWidth = GetResizeNewValue(side, minVal, maxVal, mousePos);
                newHeight = prt.sizeDelta.y;
                break;
            case Upper:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    minVal = rt.sizeDelta.y;
                }

                newX = prt.anchoredPosition.x;
                newY = prt.anchoredPosition.y;
                newWidth = prt.sizeDelta.x;
                newHeight = GetResizeNewValue(side, minVal, maxVal, mousePos);
                break;
            case Lower:
                if (firstAdjacentSubTabs.Count < 1)
                {
                    maxVal = rt.anchoredPosition.y;
                }
                else if (FloatUtil.LT(rt.anchoredPosition.y + rt.sizeDelta.y - minHeight, maxVal))
                {
                    maxVal = rt.anchoredPosition.y + rt.sizeDelta.y - minHeight;
                }

                newX = prt.anchoredPosition.x;
                newY = GetResizeNewValue(side, minVal, maxVal, mousePos);
                newWidth = prt.sizeDelta.x;
                newHeight = prt.sizeDelta.y + (prt.anchoredPosition.y - newY);
                break;
        }

        SetUp(new Vector2(newX, newY), new Vector2(newWidth, newHeight));
        
        SetUpResizedAdjacentWholeRects(side, firstAdjacentSide, secondAdjacentSide, firstAdjacentSubTabs, secondAdjacentSubTabs, thirdAdjacentSubTabs);
    }

    /*
     * Returns a list containing all of those SubTabs adjacent to this one on a given side
     * @param side An int representing the side of this SubTab on which we're looking for adjacent SubTabs
     * @return A list containing all of those SubTabs adjacent to this one on the passed side
     */
    public List<SubTab> GetSideAdjacentSubTabs(int side)
    {
        List<SubTab> sideAdjacentSubTabs = new List<SubTab>();

        foreach (SubTab subTab in superTab.GetComponent<SuperTab>().subTabs)
        {
            if (subTab != this && SubTabIsSideAdjacent(subTab, side))
            {
                sideAdjacentSubTabs.Add(subTab);
            }
        }

        return sideAdjacentSubTabs;
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
        switch (side)
        {
            case Left:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(adjacentSubTab.rt.anchoredPosition.x + minWidth, val))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.x + minWidth;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(val, adjacentSubTab.rt.anchoredPosition.x + adjacentSubTab.rt.sizeDelta.x - minWidth))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.x + adjacentSubTab.rt.sizeDelta.x - minWidth;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(adjacentSubTab.rt.anchoredPosition.x + minWidth, val))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.x + minWidth;
                        }
                        break;
                }
                break;
            case Right:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(val, adjacentSubTab.rt.anchoredPosition.x + adjacentSubTab.rt.sizeDelta.x - minWidth - rt.anchoredPosition.x))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.x + adjacentSubTab.rt.sizeDelta.x - minWidth - rt.anchoredPosition.x;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(adjacentSubTab.rt.anchoredPosition.x + minWidth - rt.anchoredPosition.x, val))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.x + minWidth - rt.anchoredPosition.x;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(val, adjacentSubTab.rt.anchoredPosition.x + adjacentSubTab.rt.sizeDelta.x - minWidth - rt.anchoredPosition.x))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.x + adjacentSubTab.rt.sizeDelta.x - minWidth - rt.anchoredPosition.x;
                        }
                        break;
                }
                break;
            case Upper:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(val, adjacentSubTab.rt.anchoredPosition.y + adjacentSubTab.rt.sizeDelta.y - minHeight - rt.anchoredPosition.y))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.y + adjacentSubTab.rt.sizeDelta.y - minHeight - rt.anchoredPosition.y;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(adjacentSubTab.rt.anchoredPosition.y + minHeight - rt.anchoredPosition.y, val))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.y + minHeight - rt.anchoredPosition.y;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(val, adjacentSubTab.rt.anchoredPosition.y + adjacentSubTab.rt.sizeDelta.y - minHeight - rt.anchoredPosition.y))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.y + adjacentSubTab.rt.sizeDelta.y - minHeight - rt.anchoredPosition.y;
                        }
                        break;
                }
                break;
            case Lower:
                switch (pass)
                {
                    case First:
                        if (FloatUtil.GT(adjacentSubTab.rt.anchoredPosition.y + minHeight, val))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.y + minHeight;
                        }
                        break;
                    case Second:
                        if (FloatUtil.GT(val, adjacentSubTab.rt.anchoredPosition.y + adjacentSubTab.rt.sizeDelta.y - minHeight))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.y + adjacentSubTab.rt.sizeDelta.y - minHeight;
                        }
                        break;
                    case Third:
                        if (FloatUtil.GT(adjacentSubTab.rt.anchoredPosition.y + minHeight, val))
                        {
                            val = adjacentSubTab.rt.anchoredPosition.y + minHeight;
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
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.rt.anchoredPosition.x), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.rt.anchoredPosition.x), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x;
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
                if (FloatUtil.GTE(mousePos.x, rt.anchoredPosition.x + minVal) && FloatUtil.LTE(mousePos.x, rt.anchoredPosition.x + maxVal))
                {
                    retVal = mousePos.x - rt.anchoredPosition.x;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.rt.anchoredPosition.x), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x - rt.anchoredPosition.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x - rt.anchoredPosition.x;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.x - subTab.rt.anchoredPosition.x), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x - rt.anchoredPosition.x;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.x - (subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x - rt.anchoredPosition.x;
                        }
                    }
                }
                else if (FloatUtil.LT(mousePos.x, rt.anchoredPosition.x + minVal))
                {
                    retVal = minVal;
                }
                else if (FloatUtil.GT(mousePos.x, rt.anchoredPosition.x + maxVal))
                {
                    retVal = maxVal;
                }
                break;
            case Upper:
                if (FloatUtil.GTE(mousePos.y, rt.anchoredPosition.y + minVal) && FloatUtil.LTE(mousePos.y, rt.anchoredPosition.y + maxVal))
                {
                    retVal = mousePos.y - rt.anchoredPosition.y;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.rt.anchoredPosition.y), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y - rt.anchoredPosition.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y - rt.anchoredPosition.y;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.rt.anchoredPosition.y), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y - rt.anchoredPosition.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y - rt.anchoredPosition.y;
                        }
                    }
                }
                else if (FloatUtil.LT(mousePos.y, rt.anchoredPosition.y + minVal))
                {
                    retVal = minVal;
                }
                else if (FloatUtil.GT(mousePos.y, rt.anchoredPosition.y + maxVal))
                {
                    retVal = maxVal;
                }
                break;
            case Lower:
                if (FloatUtil.GTE(mousePos.y, minVal) && FloatUtil.LTE(mousePos.y, maxVal))
                {
                    retVal = mousePos.y;

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Upper);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.rt.anchoredPosition.y), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y;
                        }
                    }

                    relevantAdjacentSubTabs = GetSideAdjacentSubTabs(Lower);
                    foreach (SubTab subTab in relevantAdjacentSubTabs)
                    {
                        if (FloatUtil.LT(Mathf.Abs(mousePos.y - subTab.rt.anchoredPosition.y), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y;
                        }
                        else if (FloatUtil.LT(Mathf.Abs(mousePos.y - (subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y)), ResizeOffset))
                        {
                            retVal = subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y;
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
        }

        return retVal;
    }

    /*
     * Sets up the rts for all of those adjacent SubTabs which are being resized in response to this one's resizing
     * @param side An int representing the side of this SubTab which is being resized
     * @param firstAdjacentSide An int representing the side(s) of this SubTab's first group of adjacent SubTabs which are being resized in response to this SubTab's resizing
     * @param secondAdjacentSide An int representing the side(s) of this SubTab's second group of adjacent SubTabs which are being resized in response to this SubTab's resizing
     * @param firstAdjacentSubTabs The first group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     * @param secondAdjacentSubTabs The second group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     * @param thirdAdjacentSubTabs The third group of adjacentSubTabs which are being resized in response to this SubTab's resizing
     */
    public void SetUpResizedAdjacentWholeRects(int side, int firstAdjacentSide, int secondAdjacentSide, List<SubTab> firstAdjacentSubTabs, List<SubTab> secondAdjacentSubTabs, List<SubTab> thirdAdjacentSubTabs)
    {
        foreach (SubTab firstAdjacentSubTab in firstAdjacentSubTabs)
        {
            secondAdjacentSubTabs = firstAdjacentSubTab.GetSideAdjacentSubTabs(secondAdjacentSide);
            secondAdjacentSubTabs.Remove(this);

            switch (side)
            {
                case Left:
                    firstAdjacentSubTab.SetUp(new Vector2(firstAdjacentSubTab.prt.anchoredPosition.x, firstAdjacentSubTab.prt.anchoredPosition.y), new Vector2(rt.anchoredPosition.x - firstAdjacentSubTab.rt.anchoredPosition.x, firstAdjacentSubTab.prt.sizeDelta.y));
                    break;
                case Right:
                    firstAdjacentSubTab.SetUp(new Vector2(rt.anchoredPosition.x + rt.sizeDelta.x, firstAdjacentSubTab.prt.anchoredPosition.y), new Vector2((firstAdjacentSubTab.prt.anchoredPosition.x + firstAdjacentSubTab.prt.sizeDelta.x) - (rt.anchoredPosition.x + rt.sizeDelta.x), firstAdjacentSubTab.prt.sizeDelta.y));
                    break;
                case Upper:
                    firstAdjacentSubTab.SetUp(new Vector2(firstAdjacentSubTab.prt.anchoredPosition.x, rt.anchoredPosition.y + rt.sizeDelta.y), new Vector2(firstAdjacentSubTab.prt.sizeDelta.x, (firstAdjacentSubTab.prt.anchoredPosition.y + firstAdjacentSubTab.prt.sizeDelta.y) - (rt.anchoredPosition.y + rt.sizeDelta.y)));
                    break;
                case Lower:
                    firstAdjacentSubTab.SetUp(new Vector2(firstAdjacentSubTab.prt.anchoredPosition.x, firstAdjacentSubTab.prt.anchoredPosition.y), new Vector2(firstAdjacentSubTab.prt.sizeDelta.x, rt.anchoredPosition.y - firstAdjacentSubTab.rt.anchoredPosition.y));
                    break;
            }

            foreach (SubTab secondAdjacentSubTab in secondAdjacentSubTabs)
            {
                thirdAdjacentSubTabs = secondAdjacentSubTab.GetSideAdjacentSubTabs(firstAdjacentSide);
                thirdAdjacentSubTabs.Remove(firstAdjacentSubTab);

                switch (side)
                {
                    case Left:
                        secondAdjacentSubTab.SetUp(new Vector2(firstAdjacentSubTab.rt.anchoredPosition.x + firstAdjacentSubTab.rt.sizeDelta.x, secondAdjacentSubTab.prt.anchoredPosition.y), new Vector2((secondAdjacentSubTab.prt.anchoredPosition.x + secondAdjacentSubTab.prt.sizeDelta.x) - (firstAdjacentSubTab.rt.anchoredPosition.x + firstAdjacentSubTab.rt.sizeDelta.x), secondAdjacentSubTab.prt.sizeDelta.y));
                        break;
                    case Right:
                        secondAdjacentSubTab.SetUp(new Vector2(secondAdjacentSubTab.prt.anchoredPosition.x, secondAdjacentSubTab.prt.anchoredPosition.y), new Vector2(firstAdjacentSubTab.rt.anchoredPosition.x - secondAdjacentSubTab.prt.anchoredPosition.x, secondAdjacentSubTab.prt.sizeDelta.y));
                        break;
                    case Upper:
                        secondAdjacentSubTab.SetUp(new Vector2(secondAdjacentSubTab.prt.anchoredPosition.x, secondAdjacentSubTab.prt.anchoredPosition.y), new Vector2(secondAdjacentSubTab.prt.sizeDelta.x, firstAdjacentSubTab.rt.anchoredPosition.y - secondAdjacentSubTab.prt.anchoredPosition.y));
                        break;
                    case Lower:
                        secondAdjacentSubTab.SetUp(new Vector2(secondAdjacentSubTab.prt.anchoredPosition.x, firstAdjacentSubTab.rt.anchoredPosition.y + firstAdjacentSubTab.rt.sizeDelta.y), new Vector2(secondAdjacentSubTab.prt.sizeDelta.x, (secondAdjacentSubTab.prt.anchoredPosition.y + secondAdjacentSubTab.prt.sizeDelta.y) - (firstAdjacentSubTab.rt.anchoredPosition.y + firstAdjacentSubTab.rt.sizeDelta.y)));
                        break;
                }

                foreach (SubTab thirdAdjacentSubTab in thirdAdjacentSubTabs)
                {
                    switch (side)
                    {
                        case Left:
                            thirdAdjacentSubTab.SetUp(new Vector2(thirdAdjacentSubTab.prt.anchoredPosition.x, thirdAdjacentSubTab.prt.anchoredPosition.y), new Vector2(secondAdjacentSubTab.rt.anchoredPosition.x - thirdAdjacentSubTab.rt.anchoredPosition.x, thirdAdjacentSubTab.prt.sizeDelta.y));
                            break;
                        case Right:
                            thirdAdjacentSubTab.SetUp(new Vector2(secondAdjacentSubTab.rt.anchoredPosition.x + secondAdjacentSubTab.rt.sizeDelta.x, thirdAdjacentSubTab.prt.anchoredPosition.y), new Vector2((thirdAdjacentSubTab.prt.anchoredPosition.x + thirdAdjacentSubTab.prt.sizeDelta.x) - (secondAdjacentSubTab.rt.anchoredPosition.x + secondAdjacentSubTab.rt.sizeDelta.x), thirdAdjacentSubTab.prt.sizeDelta.y));
                            break;
                        case Upper:
                            thirdAdjacentSubTab.SetUp(new Vector2(thirdAdjacentSubTab.prt.anchoredPosition.x, secondAdjacentSubTab.rt.anchoredPosition.y + secondAdjacentSubTab.rt.sizeDelta.y), new Vector2(thirdAdjacentSubTab.prt.sizeDelta.x, (thirdAdjacentSubTab.prt.anchoredPosition.y + thirdAdjacentSubTab.prt.sizeDelta.y) - (secondAdjacentSubTab.rt.anchoredPosition.y + secondAdjacentSubTab.rt.sizeDelta.y)));
                            break;
                        case Lower:
                            thirdAdjacentSubTab.SetUp(new Vector2(thirdAdjacentSubTab.prt.anchoredPosition.x, thirdAdjacentSubTab.prt.anchoredPosition.y), new Vector2(thirdAdjacentSubTab.prt.sizeDelta.x, secondAdjacentSubTab.rt.anchoredPosition.y - thirdAdjacentSubTab.rt.anchoredPosition.y));
                            break;
                    }
                }
            }
        }
    }

    /*
     * Method by which this SubTab is added to the PlayerInterface object's header as a SuperTab
     * 
     * @param superTabToBecomeParent A SuperTab object which will be formatted from this SubTab's data
     */
    public void AddAsSuperTab(SuperTab superTabToBecomeParent)
    {
        transform.SetParent(superTabToBecomeParent.body.transform);
        superTab.subTabs.Remove(this);
        if (superTab.subTabs.Count < 1)
        {
            Destroy(superTab.gameObject);
        }
        pi.superTabs.Add(superTabToBecomeParent.GetComponent<SuperTab>());

        superTab = superTabToBecomeParent.GetComponent<SuperTab>();
        superTabToBecomeParent.subTabs.Add(this);

        SetUp(Vector2.zero, new Vector2(Screen.width, Screen.height - hrt.sizeDelta.y));

        pi.OrganizeSuperTabHeaders();
        superTab.transform.SetAsLastSibling();

        pi.SetHeaderTexts();
    }

    /*
     * Method which returns whether or not this SubTab has space for another SubTab on a given side
     * @param side An int representing the side (left/right or top/bottom) on which this SubTab may or may not have space for another SubTab
     * @return a bool representing whether or not this SubTab has any space on the specified side
     */
    public bool HasSpace(int side)
    {
        if (side == Lateral)
        {
            float widthAfterPlacement = rt.sizeDelta.x / 2;

            return FloatUtil.GTE(widthAfterPlacement, minWidth);
        }
        else //side == Vertical
        {
            float heightAfterPlacement = rt.sizeDelta.y / 2;

            return FloatUtil.GTE(heightAfterPlacement, minHeight);
        }
    }

    /*
     * Method which returns the side of the SubTab on which the mouse cursor currently resides
     * @return an int representing the side of the SubTab on which the mouse cursor currently resides
     */
    public int SideOfCursor()
    {
        if (quadrants[0].Contains(Input.mousePosition))
        {
            return Left;
        }
        else if (quadrants[1].Contains(Input.mousePosition))
        {
            return Right;
        }
        else if (quadrants[2].Contains(Input.mousePosition))
        {
            return Upper;
        }
        else //quadrants[3].Contains(Input.mousePosition)
        {
            return Lower;
        }
    }

    /*
     * Returns whether or not this SubTab has an adjacent one to its passed side
     * @param side The side on which we're checking for an adjacent SubTab
     * @return A bool representing whether or not any adjacent SubTab was found on the passed side
     */
    public bool HasSideAdjacentSubTab(int side)
    {
        foreach (SubTab subTab in superTab.GetComponent<SuperTab>().subTabs)
        {
            if (subTab != this)
            {
                if (SubTabIsSideAdjacent(subTab, side))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /*
     * Returns whether or not the passed SubTab is adjacent to this one on the passed side
     * @param subTab The SubTab for which we're checking for adjacency
     * @param side The side of this SubTab on which we're checking for adjacency with subTab
     * @return A bool representing whether or not the passed SubTab is adjacent to this one on the passed side
     */
    public bool SubTabIsSideAdjacent(SubTab subTab, int side)
    {
        switch (side)
        {
            case Left:
                return FloatUtil.Equals(rt.anchoredPosition.x, subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)
                       && RectTransformUtil.ScreenRect(subTab.rt).Overlaps(new Rect(rt.anchoredPosition.x - rt.sizeDelta.x, rt.anchoredPosition.y, rt.sizeDelta.x, rt.sizeDelta.y));
                break;
            case Right:
                return FloatUtil.Equals(subTab.rt.anchoredPosition.x, rt.anchoredPosition.x + rt.sizeDelta.x)
                       && RectTransformUtil.ScreenRect(subTab.rt).Overlaps(new Rect(rt.anchoredPosition.x + rt.sizeDelta.x, rt.anchoredPosition.y, rt.sizeDelta.x, rt.sizeDelta.y));
                break;
            case Upper:
                return FloatUtil.Equals(rt.anchoredPosition.y + rt.sizeDelta.y, subTab.rt.anchoredPosition.y)
                       && RectTransformUtil.ScreenRect(subTab.rt).Overlaps(new Rect(rt.anchoredPosition.x, rt.anchoredPosition.y + rt.sizeDelta.y, rt.sizeDelta.x, rt.sizeDelta.y));
                break;
            case Lower:
                return FloatUtil.Equals(subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y, rt.anchoredPosition.y)
                       && RectTransformUtil.ScreenRect(subTab.rt).Overlaps(new Rect(rt.anchoredPosition.x, rt.anchoredPosition.y - rt.sizeDelta.y, rt.sizeDelta.x, rt.sizeDelta.y));
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

        foreach (SubTab subTab in superTab.GetComponent<SuperTab>().subTabs)
        {
            if (subTab != this)
            {
                bool subTabIsToSide = false;

                switch (side)
                {
                    case Left:
                        subTabIsToSide = FloatUtil.LT(subTab.rt.anchoredPosition.x, rt.anchoredPosition.x)
                                         && ((FloatUtil.GTE(rt.anchoredPosition.y, subTab.rt.anchoredPosition.y)
                                              && FloatUtil.LT(rt.anchoredPosition.y, subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y))
                                             || (FloatUtil.GT(rt.anchoredPosition.y + rt.sizeDelta.y, subTab.rt.anchoredPosition.y)
                                                 && FloatUtil.LTE(rt.anchoredPosition.y + rt.sizeDelta.y, subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.GT(subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x, subTabToReturn.rt.anchoredPosition.x + subTabToReturn.rt.sizeDelta.x))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                    case Right:
                        subTabIsToSide = FloatUtil.GT(subTab.rt.anchoredPosition.x, rt.anchoredPosition.x)
                                         && ((FloatUtil.GTE(rt.anchoredPosition.y, subTab.rt.anchoredPosition.y)
                                              && FloatUtil.LT(rt.anchoredPosition.y, subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y))
                                             || (FloatUtil.GT(rt.anchoredPosition.y + rt.sizeDelta.y, subTab.rt.anchoredPosition.y)
                                                 && FloatUtil.LTE(rt.anchoredPosition.y + rt.sizeDelta.y, subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.LT(subTab.rt.anchoredPosition.x, subTabToReturn.rt.anchoredPosition.x))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                    case Upper:
                        subTabIsToSide = FloatUtil.GT(subTab.rt.anchoredPosition.y, rt.anchoredPosition.y)
                                         && ((FloatUtil.GTE(rt.anchoredPosition.x, subTab.rt.anchoredPosition.x)
                                              && FloatUtil.LT(rt.anchoredPosition.x, subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x))
                                             || (FloatUtil.GT(rt.anchoredPosition.x + rt.sizeDelta.x, subTab.rt.anchoredPosition.x)
                                                 && FloatUtil.LTE(rt.anchoredPosition.x + rt.sizeDelta.x, subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.LT(subTab.rt.anchoredPosition.y, subTabToReturn.rt.anchoredPosition.y))
                            {
                                subTabToReturn = subTab;
                            }
                        }
                        break;
                    case Lower:
                        subTabIsToSide = FloatUtil.LT(subTab.rt.anchoredPosition.y, rt.anchoredPosition.y)
                                         && ((FloatUtil.GTE(rt.anchoredPosition.x, subTab.rt.anchoredPosition.x)
                                              && FloatUtil.LT(rt.anchoredPosition.x, subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x))
                                             || (FloatUtil.GT(rt.anchoredPosition.x + rt.sizeDelta.x, subTab.rt.anchoredPosition.x)
                                                 && FloatUtil.LTE(rt.anchoredPosition.x + rt.sizeDelta.x, subTab.rt.anchoredPosition.x + subTab.rt.sizeDelta.x)));

                        if (subTabIsToSide)
                        {
                            if (subTabToReturn == null || FloatUtil.GT(subTab.rt.anchoredPosition.y + subTab.rt.sizeDelta.y, subTabToReturn.rt.anchoredPosition.y + subTabToReturn.rt.sizeDelta.y))
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
     * Returns whether or not this SubTab has dead/unoccupied/overlapped space adjacent to it (meaning the superTab has dead space which must be filled)
     * @param side The side of this SubTab on which we're checking for dead/unoccupied/overlapped space
     * @return A bool representing whether or not any dead/unoccupied/overlapped space was found
     */
    public bool HasDeadSpaceToSide(int side)
    {
        if (HasSideAdjacentSubTab(side))
        {
            return false;
        }
        else
        {
            switch (side)
            {
                case Left:
                    return !(FloatUtil.Equals(rt.anchoredPosition.x, 0));
                    break;
                case Right:
                    return !(FloatUtil.Equals(rt.anchoredPosition.x + rt.sizeDelta.x, Screen.width));
                    break;
                case Upper:
                    return !(FloatUtil.Equals(rt.anchoredPosition.y + rt.sizeDelta.y, Screen.height - hrt.sizeDelta.y));
                    break;
                case Lower:
                    return !(FloatUtil.Equals(rt.anchoredPosition.y, 0));
                    break;
            }

            return false;
        }
    }

    /*
     * Method by which a SubTab alters its own rt to fill up any dead/unoccupied/overlapped space which is adjacent to it
     */
    public new void FillDeadSpace()
    {
        if (HasDeadSpaceToSide(Left))
        {
            SubTab subTabToLeft = GetNearestSubTabToSide(Left);

            if (subTabToLeft != null)
            {
                SetUp(new Vector2(subTabToLeft.rt.anchoredPosition.x + subTabToLeft.rt.sizeDelta.x,
                               rt.anchoredPosition.y),
                               new Vector2(rt.sizeDelta.x + (rt.anchoredPosition.x - (subTabToLeft.rt.anchoredPosition.x + subTabToLeft.rt.sizeDelta.x)),
                               rt.sizeDelta.y));
            }
            else
            {
                SetUp(new Vector2(0, rt.anchoredPosition.y),
                               new Vector2(rt.anchoredPosition.x + rt.sizeDelta.x, rt.sizeDelta.y));
            }
        }
        if (HasDeadSpaceToSide(Right))
        {
            SubTab subTabToRight = GetNearestSubTabToSide(Right);

            if (subTabToRight != null)
            {
                SetUp(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y),
                               new Vector2(rt.sizeDelta.x + (subTabToRight.rt.anchoredPosition.x - (rt.anchoredPosition.x + rt.sizeDelta.x)), rt.sizeDelta.y));
            }
            else
            {
                SetUp(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y),
                               new Vector2(Screen.width - rt.anchoredPosition.x, rt.sizeDelta.y));
            }
        }
        if (HasDeadSpaceToSide(Upper))
        {
            SubTab subTabAbove = GetNearestSubTabToSide(Upper);

            if (subTabAbove != null)
            {
                SetUp(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y),
                               new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + (subTabAbove.rt.anchoredPosition.y - (rt.anchoredPosition.y + rt.sizeDelta.y))));
            }
            else
            {
                SetUp(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y),
                               new Vector2(rt.sizeDelta.x, Screen.height - hrt.sizeDelta.y - rt.anchoredPosition.y));
            }
        }
        if (HasDeadSpaceToSide(Lower))
        {
            SubTab subTabBelow = GetNearestSubTabToSide(Lower);

            if (subTabBelow != null)
            {
                SetUp(new Vector2(rt.anchoredPosition.x,
                               subTabBelow.rt.anchoredPosition.y + subTabBelow.rt.sizeDelta.y),
                               new Vector2(rt.sizeDelta.x,
                               rt.sizeDelta.y + (rt.anchoredPosition.y - (subTabBelow.rt.anchoredPosition.y + subTabBelow.rt.sizeDelta.y))));
            }
            else
            {
                SetUp(new Vector2(rt.anchoredPosition.x, 0),
                               new Vector2(rt.sizeDelta.x, rt.anchoredPosition.y + rt.sizeDelta.y));
            }
        }
    }
}
