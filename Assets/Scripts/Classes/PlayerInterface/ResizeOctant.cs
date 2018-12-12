using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizeOctant : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{

    public RectTransform rt;
    public Image image;
    public SubTab subTab; //The SubTab of which this ResizeOctant is an ancestor
    public Texture2D resizeHorizontalCursor;
    public Texture2D resizeVerticalCursor;
    public Texture2D resizeUpLeftCursor;
    public Texture2D resizeUpRightCursor;

    public int type; //This value is set upon instantiation within the SubTab class

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        image = gameObject.AddComponent<Image>(); //Adding Image component to enable raycasting
        image.color =  new Color(0, 0, 0, 0);

        //Set up various cursor textures
        resizeHorizontalCursor = (Texture2D)Resources.Load("Textures/resizeHorizontal");
        resizeVerticalCursor = (Texture2D)Resources.Load("Textures/resizeVertical");
        resizeUpLeftCursor = (Texture2D)Resources.Load("Textures/resizeUpLeft");
        resizeUpRightCursor = (Texture2D)Resources.Load("Textures/resizeUpRight");
    }

    // Use this for initialization
    void Start () {
        subTab = transform.parent.parent.GetComponent<SubTab>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData ped)
    {
        Texture2D cursorToUse = null;
        Vector2 hotSpot = Vector2.zero;

        switch (type)
        {
            case SubTab.Left:
                cursorToUse = resizeHorizontalCursor;
                hotSpot = new Vector2(resizeHorizontalCursor.width / 2, 5);
                break;
            case SubTab.Right:
                cursorToUse = resizeHorizontalCursor;
                hotSpot = new Vector2(resizeHorizontalCursor.width / 2, 5);
                break;
            case SubTab.Upper:
                cursorToUse = resizeVerticalCursor;
                hotSpot = new Vector2(5, resizeVerticalCursor.height / 2);
                break;
            case SubTab.Lower:
                cursorToUse = resizeVerticalCursor;
                hotSpot = new Vector2(5, resizeVerticalCursor.height / 2);
                break;
            case SubTab.LowerLeft:
                cursorToUse = resizeUpRightCursor;
                hotSpot = new Vector2(11.5f, 11.5f);
                break;
            case SubTab.UpperLeft:
                cursorToUse = resizeUpLeftCursor;
                hotSpot = new Vector2(11.5f, 11.5f);
                break;
            case SubTab.UpperRight:
                cursorToUse = resizeUpRightCursor;
                hotSpot = new Vector2(11.5f, 11.5f);
                break;
            case SubTab.LowerRight:
                cursorToUse = resizeUpLeftCursor;
                hotSpot = new Vector2(11.5f, 11.5f);
                break;
        }

        Cursor.SetCursor(cursorToUse, hotSpot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData ped)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        subTab.OnPointerDown(ped);
        if (!RectTransformUtility.RectangleContainsScreenPoint(subTab.hrt, Input.mousePosition))
        {
            subTab.resizingWhat = Array.IndexOf(subTab.resizeOctants, this);
        }
    }

    public void OnPointerUp(PointerEventData ped)
    {
        subTab.OnPointerUp(ped);
    }

    public void OnDrag(PointerEventData ped)
    {
        subTab.OnDrag(ped);
    }
}
