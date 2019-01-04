using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public abstract class Tab : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    public PlayerInterface pi; //The overarching PlayerInterface of which all Tabs are children (though not in terms of the hierarchy)
    public GameObject body;
    public GameObject header;
    public RectTransform rt; //The RectTransform of the whole Tab
    public RectTransform prt; //The RectTransform representing rt's previously held values (for snapping back to previous position)
    public RectTransform brt; //The RectTransform of this tab's body
    public RectTransform hrt; //The RectTransform of this tab's header
    public TextMeshProUGUI headerText;
    public Image headerBodyImage;

    public bool beingDragged;
    public bool seen; //Represents whether or not this Tab has been seen since its data has been updated in some way which is meaningful to the player

    public void Awake()
    {
        pi = GameObject.Find("/PlayerInterfacePrefab").GetComponent<PlayerInterface>();
        body = Instantiate(Resources.Load("Prefabs/PlayerInterface/BodyPrefab") as GameObject);
        header = Instantiate(Resources.Load("Prefabs/PlayerInterface/HeaderPrefab") as GameObject);
        body.transform.SetParent(transform);
        header.transform.SetParent(transform);
        rt = GetComponent<RectTransform>();
        prt = rt;
        brt = body.GetComponent<RectTransform>();
        hrt = header.GetComponent<RectTransform>();
        headerText = header.transform.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>();
        headerBodyImage = header.transform.Find("Body").GetComponent<Image>();

        beingDragged = false;
        seen = true;
    }

    public void Start () {
        
    }

    /*
     * Method by which a Tab's and its associated body's and header's RectTransforms are assigned the values they previously held
     */
    public void SnapToPreviousPosition()
    {
        SetUp(new Vector2(prt.anchoredPosition.x, prt.anchoredPosition.y),
            new Vector2(prt.sizeDelta.x, prt.sizeDelta.y));
    }

    /*
     * Returns whether or not this Tab object is in front of all of its siblings
     * @return A bool representing whether or not this Tab object is in front of all of its siblings
     */
    public bool IsFrontmost()
    {
        if(transform.parent != null)
        {
            return transform.GetSiblingIndex() == transform.parent.childCount - 1;
        }
        else
        {
            return false;
        }
    }

    /*
     * Method by which all of pi.superTabs is iterated through in order for every superTab to fill any dead space within its body
     */
    public void FillDeadSpace()
    {
        foreach (SuperTab superTab in pi.superTabs)
        {
            superTab.FillDeadSpace();
        }
    }

    public abstract void OnPointerClick(PointerEventData ped);

    public abstract void OnPointerDown(PointerEventData ped);

    public abstract void OnDrag(PointerEventData ped);

    public abstract void OnPointerUp(PointerEventData ped);

    public abstract void SetUp(Vector2 pos, Vector2 size);

    public abstract void HeaderBehavior();

    public abstract void Place();
}
