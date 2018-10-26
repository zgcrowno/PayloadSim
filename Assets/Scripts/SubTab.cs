using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTab : MonoBehaviour {

    public const int Lateral = 0; //Represents the left or right sides of the SubTab
    public const int Vertical = 1; //Represents the top or bottom sides of the SubTab

    public World world; //The overarching World of which everything else is a child
    public SuperTab superTab; //The SuperTab of which this SubTab is a child
    public Rect headerRect; //The Rect making up this SubTab's header
    public Rect bodyRect; //The Rect making up this SubTab's body
    public Rect[] quadrants; //The left, right, middle-top and middle-bottom quadrants by which is determined the placement of subsequent SubTabs within the overarching SuperTab

    public float x; //The x-coordinate held by the SubTab
    public float y; //The y-coordinate held by the SubTab
    public float width; //The width of the SubTab
    public float height; //The height of the SubTab
    public float minWidth; //The minimum allowable width of the SubTab
    public float minHeight; //The minimum allowable height of the SubTab
    public float maxWidth; //The maximum allowable width of the SubTab
    public float maxHeight; //The maximum allowable height of the SubTab
    public float prevX; //The previous x-coordinate held by the SubTab
    public float prevY; //The previous y-coordinate held by the SubTab
    public float prevWidth; //The previous width of the SubTab
    public float prevHeight; //The previous height of the SubTab
    public int depth; //Integer representing the order in which different SubTabs will be drawn, thus allowing for overlay

    // Use this for initialization
    void Start () {
        world = GameObject.Find("/World").GetComponent<World>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Method which returns whether or not this SubTab has space for another SubTab on a given side
    public bool HasSpace(int side)
    {
        if(side == Lateral)
        {
            float widthAfterPlacement = width / 2;

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
            float heightAfterPlacement = height / 2;

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
}
