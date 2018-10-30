using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTab : Tab {
    
    public List<SubTab> subTabs; //The SubTabs of which this SuperTab is a parent

    public int placement; //The SuperTab's placement along the top of the screen in relation to other SuperTabs, starting at left = 0

    // Use this for initialization
    new void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        //Set the depth of this Tab in order to achieve the correct drawing order
        GUI.depth = depth;

        if(!beingDragged)
        {
            //Draw the tab's body
            GUI.DrawTexture(bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);

            //Draw the subTabs
            if (depth <= 1)
            {
                foreach (SubTab subTab in subTabs)
                {
                    GUI.DrawTexture(subTab.bodyRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
                    GUI.DrawTexture(subTab.bodyRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);
                    GUI.DrawTexture(subTab.headerRect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, false, 0);
                    GUI.DrawTexture(subTab.headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);
                }
            }
        }

        //Draw the tab's header
        GUI.DrawTexture(headerRect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, false, 0, Color.white, 1, 0);

        //headerRect interactivity
        if (headerRect.Contains(Event.current.mousePosition)) //The mouse cursor is within the bounds of headerRect
        {
            if (Event.current.type == EventType.MouseDown)
            {
                world.SetSuperTabToShallow(this);
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

    //Method that returns whether or not the SuperTab may be placed where it's currently dragged by the player
    public bool IsPlaceable()
    {
        SuperTab superTabToBecomeParent = world.GetSuperTabByDepth(depth + 1);

        if(superTabToBecomeParent.bodyRect.Contains(Event.current.mousePosition))
        {
            //This SuperTab's contents are being placed inside of another SuperTab
            if (superTabToBecomeParent.subTabs.Count < 8 && subTabs.Count == 1)
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
            //This SuperTab is simply being moved rather than its contents being nested within another
            return true;
        }
    }

    //Method by which a tab is placed in a new location or its previously held one
    public void Place()
    {
        if (IsPlaceable())
        {
            SuperTab superTabToBecomeParent = world.GetSuperTabByDepth(depth + 1);

            if(superTabToBecomeParent.bodyRect.Contains(Event.current.mousePosition))
            {
                //Placing this SuperTab's contents within another SuperTab
                foreach(SubTab subTab in superTabToBecomeParent.subTabs)
                {
                    if(subTab.wholeRect.Contains(Event.current.mousePosition))
                    {
                        if(subTab.SideOfCursor() == SubTab.Left && subTab.HasSpace(SubTab.Lateral))
                        {
                            subTabs[0].SetUpWholeRect(subTab.wholeRect.x, 
                                subTab.wholeRect.y, 
                                subTab.wholeRect.width / 2, 
                                subTab.wholeRect.height);
                            subTab.SetUpWholeRect(subTab.wholeRect.x + (subTab.wholeRect.width / 2),
                                subTab.wholeRect.y,
                                subTab.wholeRect.width / 2,
                                subTab.wholeRect.height);
                        }
                        else if(subTab.SideOfCursor() == SubTab.Right && subTab.HasSpace(SubTab.Lateral))
                        {
                            subTabs[0].SetUpWholeRect(subTab.wholeRect.x + (subTab.wholeRect.width / 2),
                                subTab.wholeRect.y,
                                subTab.wholeRect.width / 2,
                                subTab.wholeRect.height);
                            subTab.SetUpWholeRect(subTab.wholeRect.x,
                                subTab.wholeRect.y,
                                subTab.wholeRect.width / 2,
                                subTab.wholeRect.height);
                        }
                        else if(subTab.SideOfCursor() == SubTab.UpperMiddle && subTab.HasSpace(SubTab.Vertical))
                        {
                            subTabs[0].SetUpWholeRect(subTab.wholeRect.x,
                                subTab.wholeRect.y,
                                subTab.wholeRect.width,
                                subTab.wholeRect.height / 2);
                            subTab.SetUpWholeRect(subTab.wholeRect.x,
                                subTab.wholeRect.y + (subTab.wholeRect.height / 2),
                                subTab.wholeRect.width,
                                subTab.wholeRect.height / 2);
                        }
                        else if(subTab.SideOfCursor() == SubTab.LowerMiddle && subTab.HasSpace(SubTab.Vertical))
                        {
                            subTabs[0].SetUpWholeRect(subTab.wholeRect.x,
                                subTab.wholeRect.y + (subTab.wholeRect.height / 2),
                                subTab.wholeRect.width,
                                subTab.wholeRect.height / 2);
                            subTab.SetUpWholeRect(subTab.wholeRect.x,
                                subTab.wholeRect.y,
                                subTab.wholeRect.width,
                                subTab.wholeRect.height / 2);
                        }
                        subTabs[0].superTab = subTab.superTab;
                        subTab.superTab.subTabs.Add(subTabs[0]);
                        world.superTabs.Remove(this);
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                //This SuperTab is simply being moved rather than its contents being nested within another
                //SetUpWholeRect(prevWhole.x, prevWhole.y, prevWhole.width, prevWhole.height);
            }
        }
        else
        {
            //Snap to previous position
            SetUpWholeRect(prevWhole.x, prevWhole.y, prevWhole.width, prevWhole.height);
        }
    }
}
