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
    new void Start () {
        base.Start();
        minWidth = Screen.width / 4;
        minHeight = (Screen.height - (Screen.height / 20)) / 4;
        maxWidth = Screen.width;
        maxHeight = Screen.height - (Screen.height / 20);
    }
	
	// Update is called once per frame
	void Update () {
		
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
}
