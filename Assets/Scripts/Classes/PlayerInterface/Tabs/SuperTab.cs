using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SuperTab : Tab {

    public const int MaxSubTabs = 8;

    public List<GameObject> subTabs = new List<GameObject>(MaxSubTabs); //The SubTabs of which this SuperTab is a parent

    public Image headerBodyImage;
    
    new public void Start () {
        base.Start();
        transform.SetParent(pi.canvas.transform);
        SetUp(new Vector2(0, 0), new Vector2(Screen.width, Screen.height));
        headerBodyImage = header.transform.Find("Body").GetComponent<Image>();
    }

    void Update()
    {
        if (IsFrontmost())
        {
            headerBodyImage.color = Color.white;
            headerText.color = Color.black;
        }
        else
        {
            headerBodyImage.color = Color.black;
            headerText.color = Color.white;
        }
    }

    public override void OnDrag(PointerEventData ped)
    {
        if(IsFrontmost() && beingDragged)
        {
            body.SetActive(false);
            hrt.anchoredPosition = new Vector2(Input.mousePosition.x - (hrt.sizeDelta.x / 2), Input.mousePosition.y - (hrt.sizeDelta.y / 2));

            foreach(GameObject superTabObject in pi.superTabs)
            {
                SuperTab superTab = superTabObject.GetComponent<SuperTab>();
                if(superTab != this && RectTransformUtility.RectangleContainsScreenPoint(superTab.hrt, Input.mousePosition))
                {
                    CollectionsUtil.Swap(pi.superTabs, pi.GetSuperTabIndex(gameObject), pi.GetSuperTabIndex(superTab.gameObject));
                    superTab.hrt.anchoredPosition = new Vector2(superTab.rt.anchoredPosition.x + (pi.GetSuperTabIndex(superTab.gameObject) * superTab.hrt.sizeDelta.x), superTab.hrt.anchoredPosition.y);
                    superTab.gameObject.transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
                }
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
    }

    public override void OnPointerUp(PointerEventData ped)
    {
        beingDragged = false;
        Place();
    }

    public override void SetUp(Vector2 pos, Vector2 size)
    {
        rt.anchoredPosition = new Vector2(pos.x, pos.y);
        rt.sizeDelta = new Vector2(size.x, size.y);
        prt = rt;
        hrt.anchoredPosition = new Vector2(pi.GetSuperTabIndex(gameObject) * (Screen.width / PlayerInterface.MaxSuperTabs), rt.sizeDelta.y - (Screen.height / 20));
        hrt.sizeDelta = new Vector2(Screen.width / PlayerInterface.MaxSuperTabs, Screen.height / 20);
        brt.anchoredPosition = Vector2.zero;
        brt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y - hrt.sizeDelta.y);
    }

    /*
     * Method by which a tab is placed in a new location, or its previously held one if it's not being placed appropriately
     */ 
    public override void Place()
    {
        //This SuperTab is only placeable if it isn't the only SuperTab currently in the PlayerInterface
        if(pi.superTabs.Count > PlayerInterface.MinSuperTabs)
        {
            //The superTab one index behind this one in the hierarchy
            SuperTab superTabToBecomeParent = transform.parent.GetChild(transform.GetSiblingIndex() - 1).gameObject.GetComponent<SuperTab>();

            if(RectTransformUtility.RectangleContainsScreenPoint(superTabToBecomeParent.brt, Input.mousePosition))
            {
                //This SuperTab's contents are being placed inside of another SuperTab
                if(superTabToBecomeParent.subTabs.Count < MaxSubTabs && subTabs.Count == 1)
                {
                    //This SuperTab may only be placed within another if this one only contains one SubTab, and the other has fewer than the max

                    //Using bool to avoid mutating collection while iterating through it
                    bool addingAsSubTab = false;

                    //Place this SuperTab's contents within another SuperTab if placeable criteria are met
                    foreach(GameObject subTabObject in superTabToBecomeParent.subTabs)
                    {
                        SubTab subTab = subTabObject.GetComponent<SubTab>();
                        if(RectTransformUtility.RectangleContainsScreenPoint(subTab.rt, Input.mousePosition))
                        {
                            SubTab firstSubTab = subTabs[0].GetComponent<SubTab>();
                            if(subTab.SideOfCursor() == SubTab.Left && subTab.HasSpace(SubTab.Lateral))
                            {
                                firstSubTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x, subTab.rt.anchoredPosition.y),
                                    new Vector2(subTab.rt.sizeDelta.x / 2, subTab.rt.sizeDelta.y));
                                subTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x + (subTab.rt.sizeDelta.x / 2), subTab.rt.anchoredPosition.y),
                                    new Vector2(subTab.rt.sizeDelta.x / 2, subTab.rt.sizeDelta.y));

                                addingAsSubTab = true;
                            }
                            else if(subTab.SideOfCursor() == SubTab.Right && subTab.HasSpace(SubTab.Lateral))
                            {
                                firstSubTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x + (subTab.rt.sizeDelta.x / 2), subTab.rt.anchoredPosition.y),
                                    new Vector2(subTab.rt.sizeDelta.x / 2, subTab.rt.sizeDelta.y));
                                subTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x, subTab.rt.anchoredPosition.y),
                                    new Vector2(subTab.rt.sizeDelta.x / 2, subTab.rt.sizeDelta.y));

                                addingAsSubTab = true;
                            }
                            else if(subTab.SideOfCursor() == SubTab.Upper && subTab.HasSpace(SubTab.Vertical))
                            {
                                firstSubTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x, subTab.rt.anchoredPosition.y),
                                    new Vector2(subTab.rt.sizeDelta.x, subTab.rt.sizeDelta.y / 2));
                                subTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x, subTab.rt.anchoredPosition.y + (subTab.rt.sizeDelta.y / 2)),
                                    new Vector2(subTab.rt.sizeDelta.x, subTab.rt.sizeDelta.y / 2));

                                addingAsSubTab = true;
                            }
                            else if(subTab.SideOfCursor() == SubTab.Lower && subTab.HasSpace(SubTab.Vertical))
                            {
                                firstSubTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x, subTab.rt.anchoredPosition.y + (subTab.rt.sizeDelta.y / 2)),
                                    new Vector2(subTab.rt.sizeDelta.x, subTab.rt.sizeDelta.y / 2));
                                subTab.SetUp(new Vector2(subTab.rt.anchoredPosition.x, subTab.rt.anchoredPosition.y),
                                    new Vector2(subTab.rt.sizeDelta.x, subTab.rt.sizeDelta.y / 2));

                                addingAsSubTab = true;
                            }
                        }
                    }
                    if(addingAsSubTab)
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
                SnapToPreviousPosition();
            }
        }
        else
        {
            SnapToPreviousPosition();
        }
        body.SetActive(true);
    }

    /*
     * Method by which this SuperTab is added to another SuperTab object's body as a SubTab
     * @param superTabToBecomeParent A SuperTab object which will become the parent of the SubTab formulated from this SuperTab
     */
    public void AddAsSubTab(SuperTab superTabToBecomeParent)
    {
        GameObject firstSubTabObject = subTabs[0];
        SubTab firstSubTab = firstSubTabObject.GetComponent<SubTab>();
        firstSubTabObject.transform.parent = superTabToBecomeParent.body.transform;
        firstSubTab.superTab = superTabToBecomeParent.gameObject;
        superTabToBecomeParent.subTabs.Add(firstSubTab.gameObject);
        pi.superTabs.Remove(gameObject);
        pi.OrganizeSuperTabHeaders();
        superTabToBecomeParent.gameObject.transform.SetAsLastSibling();
        Destroy(gameObject);
    }

    /*
     * Method by which a SuperTab alters its own bodyRect to fill up any dead/unoccupied/overlapped space which is adjacent to it
     */
    public new void FillDeadSpace()
    {
        foreach (GameObject subTab in subTabs)
        {
            subTab.GetComponent<SubTab>().FillDeadSpace();
        }
    }
}
