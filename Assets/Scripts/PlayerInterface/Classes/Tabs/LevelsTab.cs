using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelsTab : ObjectTab
{

    public const string HeaderText = "LEVELS";

    public FocusTab focusTab;

    new public void Start() {
        objectCamera = GameObject.Find("LevelsCamera").GetComponent<PayloadCamera>();
        headerText.text = HeaderText;
        focusTab = (FocusTab)pi.GetSubTabByType(typeof(FocusTab));
        base.Start();
    }

    public override void OnPointerClick(PointerEventData ped)
    {
        Vector2 absoluteClickPosition;
        Vector2 relativeClickPosition;
        if (RectTransformUtility.RectangleContainsScreenPoint(ort, ped.position, ped.pressEventCamera))
        {
            absoluteClickPosition = new Vector2(ped.position.x - (ort.position.x - (ort.sizeDelta.x / 2)), ped.position.y - (ort.position.y - (ort.sizeDelta.y / 2)));
            relativeClickPosition = new Vector2(absoluteClickPosition.x / ort.sizeDelta.x, absoluteClickPosition.y / ort.sizeDelta.y);

            focusTab.objectCamera.SwitchContent(objectCamera.GetClickedContent(relativeClickPosition));
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
