using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour {

    public World world; //The overarching GameObject of which all others are children

    public GameObject parentTab; //If null, this tab's parent is the screen, and if not null, this tab is nested in another

    public Rect headerRect;
    public Rect bodyRect;

    public Rect[] nestedTabs = new Rect[8];

    public Stack<Vector2> prevPos = new Stack<Vector2>();

    public int depth; //Integer representing the order in which different Tabs will be drawn, thus allowing for overlay

    public float octantWidth;
    public float octantHeight;

    public float[][] nestedTabOctants = new float[8][];
    
    public bool beingDragged; //Represents whether or not this tab is currently being dragged by the player

	// Use this for initialization
	void Start () {
        world = GameObject.Find("/World").GetComponent<World>();
        parentTab = null;
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
        foreach (Rect rect in nestedTabs)
        {
            GUI.DrawTexture(rect, Texture2D.blackTexture, ScaleMode.ScaleAndCrop, true, 0);
            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, true, 0, Color.white, 1, 0);
        }

        //headerRect interactivity
        if (headerRect.Contains(Event.current.mousePosition)) //The mouse cursor is within the bounds of headerRect
        {
            print(depth);
            if (Event.current.type == EventType.MouseDown)
            {
                world.SetTabDepths(this);
                beingDragged = true;
            }
        }
        if (Event.current.type == EventType.MouseUp)
        {
            beingDragged = false;

            //Snap to previous position
            Vector2 prevPosBody = prevPos.Pop();
            Vector2 prevPosHeader = prevPos.Pop();
            headerRect.x = prevPosHeader.x;
            headerRect.y = prevPosHeader.y;
            bodyRect.x = prevPosBody.x;
            bodyRect.y = prevPosBody.y;
            prevPos.Push(new Vector2(headerRect.x, headerRect.y));
            prevPos.Push(new Vector2(bodyRect.x, bodyRect.y));
        }
        if (depth == 0 && beingDragged && Event.current.type == EventType.MouseDrag)
        {
            headerRect.x = Event.current.mousePosition.x - (headerRect.width / 2);
            headerRect.y = Event.current.mousePosition.y - (headerRect.height / 2);
        }
    }
}
