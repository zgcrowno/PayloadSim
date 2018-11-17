using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTab : Tab {

    public const int MaxSubTabs = 8;
    public const int Ascending = 0;
    public const int Descending = 1;

    public List<SubTab> subTabs; //The SubTabs of which this SuperTab is a parent

    new public void Awake()
    {
        base.Awake();
        subTabs = new List<SubTab>();
    }
    
    new public void Start () {
        base.Start();
        SetUpWholeRect(0, 0, Screen.width, Screen.height);
    }
	
	new public void Update () {
        base.Update();
	}
    
    /*
     * Method by which a tab is placed in a new location or its previously held one
     */
    public override void Place()
    {
        //This SuperTab is onle placeable if it isn't the only SuperTab currently in the PlayerInterface
        if (pi.superTabs.Count > 1)
        {
            //The SuperTab one unit deeper than this one
            SuperTab superTabToBecomeParent = pi.GetSuperTabByDepth(pi.GetSuperTabsBelow(this).Count + 1);

            if (superTabToBecomeParent.bodyRect.Contains(Event.current.mousePosition))
            {
                //This SuperTab's contents are being placed inside of another SuperTab
                if (superTabToBecomeParent.subTabs.Count < MaxSubTabs && subTabs.Count == 1)
                {
                    //This SuperTab may only be placed within another if this one only contains one SubTab, and the other has fewer than the max

                    //Using bool to avoid mutating collection while iterating through it
                    bool addingAsSubTab = false;

                    //Place this SuperTab's contents within another SuperTab if placeable criteria are met
                    foreach (SubTab subTab in superTabToBecomeParent.subTabs)
                    {
                        if (subTab.wholeRect.Contains(Event.current.mousePosition))
                        {
                            if (subTab.SideOfCursor() == SubTab.Left && subTab.HasSpace(SubTab.Lateral))
                            {
                                subTabs[0].SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width / 2,
                                    subTab.wholeRect.height);
                                subTab.SetUpWholeRect(subTab.wholeRect.x + (subTab.wholeRect.width / 2),
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width / 2,
                                    subTab.wholeRect.height);

                                addingAsSubTab = true;
                            }
                            else if (subTab.SideOfCursor() == SubTab.Right && subTab.HasSpace(SubTab.Lateral))
                            {
                                subTabs[0].SetUpWholeRect(subTab.wholeRect.x + (subTab.wholeRect.width / 2),
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width / 2,
                                    subTab.wholeRect.height);
                                subTab.SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width / 2,
                                    subTab.wholeRect.height);

                                addingAsSubTab = true;
                            }
                            else if (subTab.SideOfCursor() == SubTab.Upper && subTab.HasSpace(SubTab.Vertical))
                            {
                                subTabs[0].SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width,
                                    subTab.wholeRect.height / 2);
                                subTab.SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y + (subTab.wholeRect.height / 2),
                                    subTab.wholeRect.width,
                                    subTab.wholeRect.height / 2);

                                addingAsSubTab = true;
                            }
                            else if (subTab.SideOfCursor() == SubTab.Lower && subTab.HasSpace(SubTab.Vertical))
                            {
                                subTabs[0].SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y + (subTab.wholeRect.height / 2),
                                    subTab.wholeRect.width,
                                    subTab.wholeRect.height / 2);
                                subTab.SetUpWholeRect(subTab.wholeRect.x,
                                    subTab.wholeRect.y,
                                    subTab.wholeRect.width,
                                    subTab.wholeRect.height / 2);

                                addingAsSubTab = true;
                            }
                        }
                    }
                    if (addingAsSubTab)
                    {
                        AddAsSubTab(superTabToBecomeParent);
                    }
                    else
                    {
                        SnapToPreviousPosition();
                    }
                }
                else
                {
                    SnapToPreviousPosition();
                }
            }
            else
            {
                //This SuperTab is simply being moved within the PlayerInterface's superTabs order rather than its contents being nested within another
                SnapToPreviousPosition();
            }
        }
        else
        {
            SnapToPreviousPosition();
        }
    }

    /*
     * Method by which this SuperTab is added to another SuperTab object's body as a SubTab
     * @param superTabToBecomeParent A SuperTab object which will become the parent of the SubTab formulated from this SuperTab
     */
    public void AddAsSubTab(SuperTab superTabToBecomeParent)
    {
        subTabs[0].superTab = superTabToBecomeParent;
        superTabToBecomeParent.subTabs.Add(subTabs[0]);
        pi.superTabs.Remove(this);
        pi.OrganizeSuperTabHeaders();
        pi.SetSuperTabToDepth(superTabToBecomeParent, 0);
        Destroy(gameObject);
    }

    /*
     * Method by which this SuperTab's header's coordinates are modified based upon this SuperTab's placement within pi.superTabs
     */
    public void SnapHeaderToIndexPosition()
    {
        //This SuperTab is simply being moved rather than its contents being nested within another
        headerRect.x = wholeRect.x + (pi.GetSuperTabIndex(this) * headerRect.width);
        headerRect.y = wholeRect.y;
    }

    /*
     * Sets up all of this SuperTab's rects based on values passed for the wholeRect datum
     * @param x The x-coordinate to be applied to this SuperTab's wholeRect
     * @param y The y-coordinate to be applied to this SuperTab's wholeRect
     * @param width The width to be applied to this SuperTab's wholeRect
     * @param height The height to be applied to this SuperTab's wholeRect
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

        headerRect.width = Screen.width / PlayerInterface.MaxSuperTabs;
        headerRect.height = Screen.height / 20;
        headerRect.x = wholeRect.x + (pi.GetSuperTabIndex(this) * headerRect.width);
        headerRect.y = wholeRect.y;

        bodyRect.x = wholeRect.x;
        bodyRect.y = wholeRect.y + headerRect.height;
        bodyRect.width = wholeRect.width;
        bodyRect.height = wholeRect.height - headerRect.height;
    }

    /*
     * Method by which this SuperTab's GUI elements are drawn on the screen
     */
    public override void Draw()
    {
        //Set the depth of this Tab in order to achieve the correct drawing order
        GUI.depth = depth;

        if (!beingDragged)
        {
            //Draw the tab's body
            GUI.DrawTexture(bodyRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false);
            GUI.DrawTexture(bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);
        }

        //Draw the tab's header
        if (this == pi.GetSuperTabByDepth(0))
        {
            GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false);
            pi.headerFontStyle.normal.textColor = Color.black;
        }
        else
        {
            GUI.DrawTexture(headerRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false);
            pi.headerFontStyle.normal.textColor = Color.white;
        }
        GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);

        GUI.Label(headerRect, headerText, pi.headerFontStyle);
    }
    
    /*
     * Method by which a SubTab's depth is set to anywhere from 0-7, these ints corresponding to the depth of the passed SubTab relative to all others, and all SubTabs possessing a depth >= the passed value are incremented incidentally
     * @param subTab The SubTab whose depth we're setting
     * @param depth The depth to which we're setting the passed SubTab
     */
    public void SetSubTabToDepth(SubTab subTab, int depth)
    {
        SubTab SubTabCurrentlyAtPassedDepth = GetSubTabByDepth(depth);
        List<SubTab> SubTabsBelowThatCurrentlyAtPassedDepth = GetSubTabsBelow(SubTabCurrentlyAtPassedDepth);

        int startingDepth = 0;

        foreach (SubTab subt in SubTabsBelowThatCurrentlyAtPassedDepth)
        {
            startingDepth++;
        }

        subTab.depth = startingDepth;
        startingDepth++;

        foreach (SubTab subt in subTabs)
        {
            if (subt != subTab && !SubTabsBelowThatCurrentlyAtPassedDepth.Contains(subt))
            {
                subt.depth = startingDepth;
                startingDepth++;
            }
        }
    }
    
    /*
     * Returns the SubTab whose depth is anywhere from 0-7, these ints corresponding to the depth of the passed SubTab relative to all others
     * @param depth The depth by which we're searching for a SubTab
     * @return The SubTab whose depth matches the passed depth
     */
    public SubTab GetSubTabByDepth(int depth)
    {
        SubTab subTabToReturn = null;

        foreach (SubTab subTab in subTabs)
        {
            List<SubTab> subTabsBelow = GetSubTabsBelow(subTab);
            if (subTabsBelow.Count == depth)
            {
                subTabToReturn = subTab;
            }
        }

        return subTabToReturn;
    }

    /*
     * Returns a list containing all of those SubTabs which have a depth less than the SubTab passed as a parameter
     * @param subTab The SubTab below whose depth we're grabbing all of the other SubTabs
     * @return A list of SubTabs whose depths are less than the passed SubTab
     */
    public List<SubTab> GetSubTabsBelow(SubTab subTab)
    {
        List<SubTab> subTabsBelow = new List<SubTab>();
        foreach (SubTab st in subTabs)
        {
            if (st.depth < subTab.depth)
            {
                subTabsBelow.Add(st);
            }
        }
        return subTabsBelow;
    }

    /*
     * Returns whether or not this SuperTab has any space within its bodyRect not taken up by a SubTab
     * @return A bool representing whether or not this SuperTab has any space within its bodyRect not taken up by a SubTab
     */
    public bool HasDeadSpace()
    {
        foreach(SubTab subTab in subTabs)
        {
            if(subTab.HasDeadSpaceOnAnySide())
            {
                return true;
            }
        }

        return false;
    }

    /*
     * Executes those behaviors associated with a MouseDown event
     */
    public override void MouseDown()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            if (headerRect.Contains(Event.current.mousePosition))
            {
                pi.SetSuperTabToDepth(this, 0);
                beingDragged = true;
            }
        }
    }

    /*
     * Executes those behaviors associated with a MouseDrag event
     */
    public override void MouseDrag()
    {
        if (Event.current.type == EventType.MouseDrag)
        {
            if(this == pi.GetSuperTabByDepth(0) && beingDragged)
            {
                headerRect.x = Event.current.mousePosition.x - (headerRect.width / 2);
                headerRect.y = Event.current.mousePosition.y - (headerRect.height / 2);

                foreach (SuperTab superTab in pi.superTabs)
                {
                    if (superTab != this && superTab.headerRect.Contains(Event.current.mousePosition))
                    {
                        CollectionsUtil.Swap(pi.superTabs, pi.GetSuperTabIndex(this), pi.GetSuperTabIndex(superTab));
                        superTab.headerRect.x = wholeRect.x + (pi.GetSuperTabIndex(superTab) * headerRect.width);
                        pi.SetSuperTabToDepth(superTab, 1);
                    }
                }
            }
        }
    }

    /*
     * Executes those behaviors associated with a MouseUp event
     */
    public override void MouseUp()
    {
        if (Event.current.type == EventType.MouseUp)
        {
            if(beingDragged)
            {
                beingDragged = false;

                Place();
            }
        }
    }

    /*
     * Method by which a SuperTab alters its own bodyRect to fill up any dead/unoccupied/overlapped space which is adjacent to it
     */
    public new void FillDeadSpace()
    {
        foreach(SubTab subTab in subTabs)
        {
            subTab.FillDeadSpace();
        }
    }

    /*
     * Method by which a SuperTab sorts its subTabs list by depth, either ascending or descending
     * @param ascOrDesc An int representing whether this SuperTab's subTabs list is to be sorted in ascending or descending order
     */
    public void SortSubTabsByDepth(int ascOrDesc)
    {
        SubTab temp;
        int j;

        switch(ascOrDesc)
        {
            case Ascending:
                for (int i = 1; i <= subTabs.Count - 1; i++)
                {
                    temp = subTabs[i];
                    j = i - 1;
                    while (j >= 0 && subTabs[j].depth > temp.depth)
                    {
                        subTabs[j + 1] = subTabs[j];
                        j--;
                    }
                    subTabs[j + 1] = temp;
                }
                break;
            case Descending:
                for (int i = 1; i <= subTabs.Count - 1; i++)
                {
                    temp = subTabs[i];
                    j = i - 1;
                    while (j >= 0 && subTabs[j].depth < temp.depth)
                    {
                        subTabs[j + 1] = subTabs[j];
                        j--;
                    }
                    subTabs[j + 1] = temp;
                }
                break;
        }
    }
}
