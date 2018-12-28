using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelsTab : ObjectTab
{

    public const string HeaderText = "LEVELS";

    public Vector2 mouseDownPosition = new Vector2(); //Vector2 used to determine whether or not the mouse cursor's position has changed between OnPointerDown and OnPointerUp

    public FocusTab focusTab;

    new public void Start() {
        objectCamera = GameObject.Find("LevelsCamera").GetComponent<PayloadCamera>();
        headerText.text = HeaderText;
        focusTab = (FocusTab)pi.GetSubTabByType(typeof(FocusTab));
        base.Start();
    }

    public override void OnPointerDown(PointerEventData ped)
    {
        base.OnPointerDown(ped);
        if(!beingDragged && resizingWhat == None)
        {
            mouseDownPosition = ped.position;
        }
    }

    public override void OnPointerUp(PointerEventData ped)
    {
        base.OnPointerUp(ped);
        if(mouseDownPosition.Equals(ped.position)) //The mouse has been clicked without its cursor being moved between OnPointerDown and OnPointerUp
        {
            Vector2 absoluteClickPosition;
            Vector2 relativeClickPosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(ort, ped.position, ped.pressEventCamera))
            {
                absoluteClickPosition = new Vector2(ped.position.x - (ort.position.x - (ort.sizeDelta.x / 2)), ped.position.y - (ort.position.y - (ort.sizeDelta.y / 2)));
                relativeClickPosition = new Vector2(absoluteClickPosition.x / ort.sizeDelta.x, absoluteClickPosition.y / ort.sizeDelta.y);

                GameObject clickedObject = objectCamera.GetClickedContent(relativeClickPosition);
                if(clickedObject != null && clickedObject.GetComponent<Clickable>() != null) //Only switch content if the clickedObject is not null, and has a Clickable component, thus being eligible for focus
                {
                    focusTab.objectCamera.SwitchContent(clickedObject);
                }
            }
        }
    }

    public override void OnDrag(PointerEventData ped)
    {
        base.OnDrag(ped);
        if(!beingDragged && resizingWhat == None)
        {
            objectCamera.OrbitAroundContent();
        }
    }
}
